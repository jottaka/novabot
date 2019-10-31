using Newtonsoft.Json;
using NovaBot.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NovaBot.Helpers
{
    public class SlackApiHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        public string clientId = "";
        private readonly string scope = "";
        private readonly string redirectUri = "www";
        private readonly string clientSecret = "";
        public SlackApiHelper(
            IHttpClientFactory clientFactory
            ) {
            _clientFactory = clientFactory;
        }


        public async Task SendUserAuth()
        {
            UriBuilder builder = new UriBuilder("https://slack.com/oauth/authorize");
            builder.Query = $"client_id={clientId}&scope={scope}&redirect_uri={redirectUri}";
            var request = new HttpRequestMessage(HttpMethod.Get, 
                builder.Uri);
           
            var client = _clientFactory.CreateClient();
            var result = await client.SendAsync(request);

        }

        public async Task ProcessVerificationCode(string code) {
            try
            {
                HttpRequestMessage request = prepareProcessVerificaztionCodeRequest(code);
                ExchangeVerificationCodeResponseModel response = await getPrepareProcessVerificationCodeResponse(request);
                var tst = response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public async Task<List<UserSlackModel>> GetUserList(string token) {
            try
            {
                HttpResponseMessage sent = await prepareGetUserListRequest(token);
                return await validateResponse(sent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region PRIVATE

        private async Task<HttpResponseMessage> prepareGetUserListRequest(string token)
        {
            UriBuilder builder = new UriBuilder("https://slack.com/api/users.list");
            builder.Query = $"token={token}";

            var request = new HttpRequestMessage(HttpMethod.Get,
                builder.Uri);
            request.Headers.Clear();
            request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var client = _clientFactory.CreateClient();
            var sent = await client.SendAsync(request);
            return sent;
        }

        private static async Task<List<UserSlackModel>> validateResponse(HttpResponseMessage sent)
        {
            if (sent.IsSuccessStatusCode)
            {
                var responseStr = await sent.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<UserListApiResponseMode>(
                    responseStr
                    );
                if (response.ok)
                {
                    return response.members;
                }
                else
                {
                    throw new Exception();
                }

            }
            else
            {
                throw new Exception();
            }
        }

        private async Task<ExchangeVerificationCodeResponseModel> getPrepareProcessVerificationCodeResponse(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            var sent = await client.SendAsync(request);
            return await processGetPrepareProcessVerificationCodeResponse(sent);
        }

        private static async Task<ExchangeVerificationCodeResponseModel> processGetPrepareProcessVerificationCodeResponse(HttpResponseMessage sent)
        {
            if (sent.IsSuccessStatusCode)
            {
                var responseStr = await sent.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ExchangeVerificationCodeResponseModel>(
                    responseStr
                    );
                return response;
            }
            else
            {
                throw new Exception();
            }
        }

        private HttpRequestMessage prepareProcessVerificaztionCodeRequest(string code)
        {
            UriBuilder builder = new UriBuilder("https://slack.com/api/oauth.access");

            var request = new HttpRequestMessage(HttpMethod.Post,
                builder.Uri);
            StringContent content = prepareContent(code);

            request.Headers.Clear();
            request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            request.Content = content;
            return request;
        }

        private StringContent prepareContent(string code)
        {
            var payload = new ExchangeVerificationCodeRequestModel()
            {
                code = code,
                client_id = clientId,
                client_secret = clientSecret,
                redirect_uri = redirectUri
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload),
                Encoding.UTF8, "application/json");
            return content;
        } 
        #endregion

    }





}
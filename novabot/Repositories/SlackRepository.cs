using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using NovaBot.Repositories.interfaces;
using NovaBot.Data;
using Microsoft.AspNetCore.Mvc;
using NovaBot.Helpers;

namespace NovaBot.Repositories
{
    public class SlackRepository : ISlackRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SlackApiHelper _slackApiHelper;
        public SlackRepository(
            ApplicationDbContext context,
            SlackApiHelper slackApiHelper
            )
        {
            _slackApiHelper = slackApiHelper;
            _context = context;
        }

        public async Task<string> ProcessRequest(SlackEventRequestModel request)
        {
            try
            {
                string response = null;
                switch (request.type)
                {
                    case "url_verification":
                        response = request.challenge;
                        break;
                    case "message":
                        break;
                    default:
                        break;
                }
                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        public async Task SendMessage(string message, string channel)
        {
            try
            {
                MessageToChannelMode messageModel = prepareMessageToChannelModel(message, channel);
                await _slackApiHelper.SendMessageToChannel(messageModel);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private MessageToChannelMode prepareMessageToChannelModel(string message, string channel)
        {
            var token = _context.Configurations.FirstOrDefault().BotAccessToken;
            var messageModel = new MessageToChannelMode()
            {
                token = token,
                channel = channel,
                text = message
            };
            return messageModel;
        }

        public async Task GetUserList()
        {
            try
            {
                var token = (await _context.Configurations.FirstOrDefaultAsync()).BotAccessToken;
                var users = await _slackApiHelper.GetUserListAsync(token);
                if (!users.Any())
                {
                    return;
                }
                var usersList = users.ConvertAll(u => new UserModel(u));
                await _context.AddRangeAsync(usersList);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public async Task GetVerificationCode(string code)
        {
            try
            {
                await saveAuthCode(code);
                await _context.SaveChangesAsync();
                var requestResponse = await _slackApiHelper.ProcessVerificationCodeAsync(code);
                await saveBotAccessTokenData(requestResponse);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private async Task saveAuthCode(string code)
        {
            if (!await _context.Configurations.AnyAsync())
            {
                await _context.AddAsync(new ConfigurationModel() { LastAuthToken = code });
            }
            else
            {
                var config = await _context.Configurations.FirstOrDefaultAsync();
            }
        }

        private async Task saveBotAccessTokenData(ExchangeVerificationCodeResponseModel response)
        {
            var configurations = await _context.Configurations.FirstOrDefaultAsync();
            configurations.BotAccessToken = response.bot.bot_access_token;
            configurations.BotUserId = response.bot.bot_user_id;
            await _context.SaveChangesAsync();
        }

        public async Task<string> AddQuoteAsync(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
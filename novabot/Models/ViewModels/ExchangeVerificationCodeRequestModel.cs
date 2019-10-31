using System.Collections.Generic;


namespace NovaBot.Models.ViewModels
{

    public class ExchangeVerificationCodeRequestModel
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string code { get; set; }
        public string redirect_uri { get; set; }

    }

}

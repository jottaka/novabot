using System.Collections.Generic;


namespace NovaBot.Models.ViewModels
{

    public class ExchangeVerificationCodeResponseModel
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public BotVerificationModel bot { get; set; }
    }


    public class BotVerificationModel{
        public string bot_user_id { get; set; }
        public string stringbot_access_token { get; set; }
    }

}

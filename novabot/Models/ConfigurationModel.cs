using System;

namespace NovaBot.Models
{

    public class ConfigurationModel
    {
        //FK
        public string ConfigurationId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BotUserId { get; set; }
        public string BotAccessToken { get; set; }
        public string LastAuthToken { get; set; }

    }

}

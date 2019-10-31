using System.Collections.Generic;


namespace NovaBot.Models.ViewModels
{

    public class SlackEventRequestModel
    {
        public string token { get; set; }
        public string challenge { get; set; }
        public string type { get; set; }
        public string user { get; set; }
        public string  text { get; set; }
        public string channel { get; set; }
    }

}

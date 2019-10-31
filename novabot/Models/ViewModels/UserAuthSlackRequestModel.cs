using System.Collections.Generic;


namespace NovaBot.Models.ViewModels
{

    public class UserAuthSlackRequestModel
    {
        public string client_id { get; set; }
        public string scope { get; set; }
        public string redirect_uri { get; set; }

    }

}

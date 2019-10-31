using System.Collections.Generic;


namespace NovaBot.Models.ViewModels
{


    public class UserListApiResponseMode
    {
        public bool ok { get; set; }
        public List<UserSlackModel> members { get; set; }
    }

    public class UserSlackModel
    {
        public string id { get; set; }
        public  string name { get; set; }
        public bool deleted { get; set; }
        public string real_name { get; set; }
        public UserProfileModel profile { get; set; }
    }

    public class UserProfileModel
    {
        public string status_text { get; set; }
        public string image_72 { get; set; }
        public string image_192 { get; set; }
        public string image_512 { get; set; }
    }

}

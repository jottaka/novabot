using NovaBot.Models.ViewModels;
using System.Collections.Generic;


namespace NovaBot.Models
{

    public class UserModel
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public uint Ranking { get; set; }
        public string SlackId { get; set; }
        public string RealName { get; set; }
        public bool Deleted { get; set; }
        public string StatusText { get; set; }
        public string ProfilePicutre_72 { get; set; }
        public string ProfilePicture_192 { get; set; }
        public string ProfilePicutre_512 { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }


        public List<QuoteModel> Quotes { get; set; }

        public UserModel() { }
        public UserModel(UserViewModel user) {
            FromViewModel(user);
        }

        public UserModel(UserSlackModel model)
        {
            Name = model.name;
            RealName = model.real_name;
            SlackId = model.id;
            StatusText = model.profile.status_text;
            Deleted = model.deleted;
            ProfilePicture_192 = model.profile.image_192;
            ProfilePicutre_512 = model.profile.image_512;
            ProfilePicutre_72 = model.profile.image_72;
        }

        public UserViewModel ToViewModel()
        {
            return new UserViewModel()
            {
                Name = this.Name,
                Ranking = this.Ranking,
                UserId = this.UserId
            };
        }

        public void FromViewModel(UserViewModel user)
        {
            Name = user.Name;
            UserId = user.UserId;
            Ranking = user.Ranking;
        }

        

    }

}

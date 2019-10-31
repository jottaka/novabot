using NovaBot.Models.ViewModels;
using System.Collections.Generic;


namespace NovaBot.Models
{

    public class UserModel
    {
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string UserId { get; set; }
        public uint Ranking { get; set; }
        public List<QuoteModel> Quotes { get; set; }

        public UserModel() { }
        public UserModel(UserViewModel user) {
            FromViewModel(user);
        }

        public UserViewModel ToViewModel()
        {
            return new UserViewModel()
            {
                Name = this.Name,
                ProfilePicture = this.ProfilePicture,
                Ranking = this.Ranking,
                UserId = this.UserId
            };
        }

        public void FromViewModel(UserViewModel user)
        {
            Name = user.Name;
            ProfilePicture = user.ProfilePicture;
            UserId = user.UserId;
            Ranking = user.Ranking;
        }

    }

}

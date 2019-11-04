using Microsoft.EntityFrameworkCore;
using NovaBot.Data;
using NovaBot.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public List<VoteModel> MyVotes { get; set; }


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

        public async Task<UserViewModel> ToViewModel(ApplicationDbContext ctx)
        {
            var numberQuotes = await ctx.Quote.CountAsync(c => c.UserId == UserId);
            var numberOfDownVotes = await ctx.VoteModels.CountAsync(
                c=> c.UserSlackId == SlackId && c.Vote==-1
                );
            var numberOfUpvotes = await ctx.VoteModels.CountAsync(
                c => c.UserSlackId == SlackId && c.Vote == 1
                );
            var snitchScore = await ctx.Quote.CountAsync(
                q=> q.SnitchId==UserId
                );

            return new UserViewModel()
            {
                Name = this.Name,
                Ranking = this.Ranking,
                UserId = this.UserId,
                ProfilePicture = this.ProfilePicture_192,
                UpVotes = (uint)numberOfUpvotes,
                DownVotes=(uint)numberOfDownVotes,
                NumberOfQuotes = (uint)numberQuotes,
                SnitchScore = (uint)snitchScore,
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

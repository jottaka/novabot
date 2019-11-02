using NovaBot.Models.ViewModels;
using System;

namespace NovaBot.Models
{

    public class QuoteModel
    {
        //FK
        public string UserId { get; set; }
        public string SnitchId { get; set; }
        public string Content { get; set; }
        public string QuoteId { get; set; }
        public DateTimeOffset Date { get; set; }
        public uint Upvotes { get; set; }
        public uint Downvotes { get; set; }
        public UserModel User { get; set; }
        public UserModel Snitch { get; set; }



        public QuoteFullViewModel ToFullViewMode()
        {

            var toReturn = new QuoteFullViewModel()
            {
                Content = Content,
                Date = Date,
                Downvotes = Downvotes,
                SnitchId = SnitchId,
                SnitchName=Snitch.Name,
                QuoteId = QuoteId,
                Upvotes= Upvotes,
                UserId=UserId,
                UserName = User.Name,
                UserPicture=User.ProfilePicture_192
            };
            return toReturn;
        }
    }


    

}

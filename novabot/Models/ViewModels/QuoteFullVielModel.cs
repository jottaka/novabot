using System;

namespace NovaBot.Models.ViewModels
{

    public class QuoteFullViewModel
    {
        //FK
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public string SnitchId { get; set; }
        public string SnitchName { get; set; }
        public string Content { get; set; }
        public string QuoteId { get; set; }
        public DateTimeOffset Date { get; set; }
        public uint Upvotes { get; set; }
        public uint Downvotes { get; set; }
    }

}

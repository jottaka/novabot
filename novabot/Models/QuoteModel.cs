using System;

namespace NovaBot.Models
{

    public class QuoteModel
    {
        //FK
        public string UserId { get; set; }

        public string Content { get; set; }
        public string QuoteId { get; set; }
        public DateTimeOffset Date { get; set; }
        public uint Upvotes { get; set; }
        public uint Downvotes { get; set; }
        public UserModel User { get; set; }
    }

}

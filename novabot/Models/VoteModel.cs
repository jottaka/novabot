using System;

namespace NovaBot.Models
{

    public class VoteModel
    {
        //FK
        public string UserSlackId { get; set; }
        public string QuoteVoteUid { get; set; }
        public short Vote { get; set; }
        public string QuoteId { get; set; }
        public QuoteModel Quote { get; set; }
        public UserModel User { get; set; }

    }

}

using System;
using System.Collections.Generic;
using static NovaBot.Helpers.EnumerablesHelper;

namespace NovaBot.Models.ViewModels
{

    public class ListQuoteResponseModel
    {
        public int TotalQuotes { get; set; }
        public int NumberOfPages { get; set; }
        public List<QuoteModel> quotes { get; set; }
        public int PageNumber { get; set; }


    }

}

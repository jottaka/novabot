using System.Collections.Generic;
using System.Threading.Tasks;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using static NovaBot.Helpers.EnumerablesHelper;

namespace NovaBot.Repositories.interfaces
{

    public interface IQuoteRepository
    {

        Task<string> AddQuoteAsync(QuoteModel quote);
        Task UpdateQuoteAsync(QuoteModel quote);
        Task DeleteQuoteAsync(string id);
        Task<int> CountQuotesAsync();
        Task<ListQuoteResponseModel> GetListAsync(ListQuoteRequestModel request);
        Task UpvoteAsync(string quoteId);
        Task DownvoteAsync(string quoteId);
    }

}

using System.Collections.Generic;
using System.Threading.Tasks;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using static NovaBot.Helpers.EnumerablesHelper;

namespace NovaBot.Repositories.interfaces
{

    public interface ISlackRepository
    {
        Task<string> AddQuoteAsync(string message);
        Task<string> ProcessRequest(SlackEventRequestModel request);
    }

}

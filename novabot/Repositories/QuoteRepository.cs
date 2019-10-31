using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NovaBot.Helpers;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
using NovaBot.Repositories.interfaces;
using NovaBot.Data;
using static NovaBot.Helpers.EnumerablesHelper;

namespace NovaBot.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _context;
        public QuoteRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
        }
        public async Task<string> AddQuoteAsync(QuoteModel quote)
        {
            try
            {
                await _context.Quote.AddAsync(quote);
                await _context.SaveChangesAsync();
                return quote.QuoteId;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task DeleteQuoteAsync(string id)
        {
            try
            {
                var toRemove = _context.Quote.FirstOrDefault(
                    q => q.QuoteId == id
                    );
                _context.Remove(toRemove);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<ListQuoteResponseModel> GetList(ListQuoteRequestModel request)
        {
            try
            {
                ListQuoteResponseModel response = await getListResponse(request);

                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<int> CountQuotes()
        {
            try
            {
                return await countQuotes();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public Task DownvoteAsync(string quoteId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateQuoteAsync(QuoteModel quote)
        {
            throw new System.NotImplementedException();
        }

        public Task UpvoteAsync(string quoteId)
        {
            throw new System.NotImplementedException();
        }

        #region PRIVATE

        private async Task<ListQuoteResponseModel> getListResponse(ListQuoteRequestModel request)
        {
            IQueryable<QuoteModel> orderedQuery = null;
            var quotesCount = await countQuotes();
            var numberOfPages = quotesCount / request.N;

            var queryUnordered = _context.Quote.AsNoTracking();

            switch (request.OrderBy)
            {
                case OrderByEnum.ByDate:
                    orderedQuery = queryUnordered.OrderByDescending(q => q.Date);
                    break;
                case OrderByEnum.ByName:
                    orderedQuery = queryUnordered.OrderByDescending(q => q.User.Name);
                    break;
                case OrderByEnum.ByPositiveVotes:
                    orderedQuery = queryUnordered.OrderByDescending(q => q.Upvotes);
                    break;
                case OrderByEnum.ByNegativeVotes:
                    orderedQuery = queryUnordered.OrderByDescending(q => q.Downvotes);
                    break;
            }

            var toReturnList = await orderedQuery?.Skip(request.N * request.Page)
                .Take(request.N)?.ToListAsync();

            var response = new ListQuoteResponseModel()
            {
                NumberOfPages = numberOfPages,
                PageNumber = request.Page,
                quotes = toReturnList,
                TotalQuotes = quotesCount
            };
            return response;
        }
        private async Task<int> countQuotes()
        {
            return await _context.Quote.AsNoTracking().CountAsync();
        }

        public Task<int> CountQuotesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ListQuoteResponseModel> GetListAsync(ListQuoteRequestModel request)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
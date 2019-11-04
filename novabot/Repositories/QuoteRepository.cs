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
using System;

namespace NovaBot.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _context;
        private static Random random = new Random();

        public QuoteRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        /// <summary>
        /// Retunr the QuoteVoteUid To make it possible to vote using it
        /// </summary>
        /// <param name="quoteEvent"></param>
        /// <returns></returns>
        public async Task<String> ReceiveNewQuoteEvent(SlackEventRequestModel quoteEvent)
        {
            try
            {
                return await processNewQuoteEvent(quoteEvent);
            }
            catch (global::System.Exception)
            {

                throw;
            }
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

        public async Task<ListQuoteResponseModel> GetListAsync(ListQuoteRequestModel request)
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

        public async Task<ListQuoteResponseModel> GetListByUserAsync(ListQuoteRequestModel request, string userId)
        {
            try
            {
                ListQuoteResponseModel response = await getListResponseByUser(request, userId);

                return response;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<ListQuoteResponseModel> GetListBySnitchAsync(ListQuoteRequestModel request, string snitchId)
        {
            try
            {
                ListQuoteResponseModel response = await getListResponseBySnitch(request, snitchId);

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

        public async Task UpvoteAsync(SlackEventRequestModel request)
        {
            try
            {
                var quote = await validadeVoteRequest(request);
                //To Do
                //Verify if user exists, if not request its information
                short voteValue = 1;
                await saveVote(request, voteValue, quote);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DownvoteAsync(SlackEventRequestModel request)
        {
            try
            {
                var quote = await validadeVoteRequest(request);
                //To Do
                //Verify if user exists, if not request its information
                short voteValue = -1;
                await saveVote(request, voteValue,quote);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region NotImplemented

        private async Task processEvent(SlackEventRequestModel quoteEvent)
        {
            throw new System.NotImplementedException();
        }


        public Task DownvoteAsync(string quoteId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateQuoteAsync(QuoteModel quote)
        {
            throw new System.NotImplementedException();
        }


        public Task<int> CountQuotesAsync()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region PRIVATE

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> getAuthorId(string author)
        {
            string authorId = null;
            if (author != null)
            {
                var authorDb = await _context.User.FirstOrDefaultAsync(u =>
                string.Compare(u.Name.ToLower(), author.ToLower()) == 0 ||
                string.Compare(u.RealName.ToLower(), author.ToLower()) == 0);
                if (authorDb is null)
                {
                    authorId = null;
                }
                else
                {
                    authorId = authorDb.UserId;
                }

            }

            return authorId;
        }

        private async Task<ListQuoteResponseModel> getListResponse(ListQuoteRequestModel request)
        {
            IQueryable<QuoteModel> orderedQuery = null;
            var quotesCount = await countQuotes();
            var numberOfPages = quotesCount / request.N;

            orderedQuery = generateListQueriable(request, orderedQuery);

            ListQuoteResponseModel response = await getListResponse(request, orderedQuery, quotesCount, numberOfPages);
            return response;
        }

        private async Task<ListQuoteResponseModel> getListResponseByUser(ListQuoteRequestModel request, string userId)
        {
            IQueryable<QuoteModel> orderedQuery = null;
            var quotesCount = await countQuotesByUser(userId);
            var numberOfPages = quotesCount / request.N;

            orderedQuery = generateListQueriableByUser(request, orderedQuery, userId);

            ListQuoteResponseModel response = await getListResponse(request, orderedQuery, quotesCount, numberOfPages);
            return response;
        }

        private async Task<ListQuoteResponseModel> getListResponseBySnitch(ListQuoteRequestModel request, string snitchId)
        {
            IQueryable<QuoteModel> orderedQuery = null;
            var quotesCount = await countQuotesbySnitch(snitchId);
            var numberOfPages = quotesCount / request.N;

            orderedQuery = generateListQueriableBySnitch(request, orderedQuery, snitchId);

            ListQuoteResponseModel response = await getListResponse(request, orderedQuery, quotesCount, numberOfPages);
            return response;
        }



        private static async Task<ListQuoteResponseModel> getListResponse(ListQuoteRequestModel request, IQueryable<QuoteModel> orderedQuery, int quotesCount, int numberOfPages)
        {
            var toReturnList = (await orderedQuery?.Skip(request.N * request.Page)
                            .Take(request.N)?.ToListAsync()).ConvertAll(
                            q => q.ToFullViewMode()
                            );

            var response = new ListQuoteResponseModel()
            {
                NumberOfPages = numberOfPages,
                PageNumber = request.Page,
                Quotes = toReturnList,
                TotalQuotes = quotesCount
            };
            return response;
        }


        private IQueryable<QuoteModel> generateListQueriableBySnitch(ListQuoteRequestModel request, IQueryable<QuoteModel> orderedQuery, string snitchId)
        {
            var queryUnordered = _context.Quote.Include(q => q.User).Include(q => q.Snitch)
                            .AsNoTracking().Where(u => u.SnitchId == snitchId);

            orderedQuery = orderQuotes(request, orderedQuery, queryUnordered);

            return orderedQuery;
        }

        private IQueryable<QuoteModel> generateListQueriableByUser(ListQuoteRequestModel request, IQueryable<QuoteModel> orderedQuery, string userId)
        {
            var queryUnordered = _context.Quote.Include(q => q.User).Include(q => q.Snitch)
                            .AsNoTracking().Where(u => u.UserId == userId);

            orderedQuery = orderQuotes(request, orderedQuery, queryUnordered);

            return orderedQuery;
        }

        private IQueryable<QuoteModel> generateListQueriable(ListQuoteRequestModel request, IQueryable<QuoteModel> orderedQuery)
        {
            var queryUnordered = _context.Quote.Include(q => q.User).Include(q => q.Snitch)
                            .AsNoTracking();

            orderedQuery = orderQuotes(request, orderedQuery, queryUnordered);

            return orderedQuery;
        }

        private static IQueryable<QuoteModel> orderQuotes(ListQuoteRequestModel request, IQueryable<QuoteModel> orderedQuery, IQueryable<QuoteModel> queryUnordered)
        {
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

            return orderedQuery;
        }

        private async Task<int> countQuotes()
        {
            return await _context.Quote.AsNoTracking().CountAsync();
        }

        private async Task<int> countQuotesByUser(string userId)
        {
            return await _context.Quote.AsNoTracking().CountAsync(q => q.UserId == userId);
        }

        private async Task<int> countQuotesbySnitch(string snitchId)
        {
            return await _context.Quote.AsNoTracking().CountAsync(q => q.SnitchId == snitchId);
        }


        private async Task saveQuote(SlackEventRequestModel quoteEvent, string quote, string authorId)
        {
            var snitchId = await _context.User
                                .FirstOrDefaultAsync(u => u.SlackId == quoteEvent.user_id);
            var quoteModel = new QuoteModel()
            {
                Content = quote,
                Date = DateTimeOffset.UtcNow,
                Upvotes = 0,
                Downvotes = 0,
                SnitchId = authorId,
                UserId = authorId
            };
            await _context.Quote.AddAsync(quoteModel);
            await _context.SaveChangesAsync();
        }


        private async Task saveVote(SlackEventRequestModel request, short voteValue,QuoteModel quote)
        {
            var vote = new VoteModel()
            {
                QuoteVoteUid = request.text,
                UserSlackId = request.user_id,
                Vote = voteValue,
                QuoteId = quote.QuoteId
            };
            if (vote.Vote > 0)
            {
                quote.Upvotes++;
            }
            else
            {
                quote.Downvotes++;
            }
            await _context.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        private async Task<QuoteModel> validadeVoteRequest(SlackEventRequestModel request)
        {
            if (request.text.Split(' ').Count() > 1)
            {
                throw new Exception($"Requeste invalido: {request.text}");
            }

            var quote = await _context.Quote.
                FirstOrDefaultAsync(v => v.QuoteVoteUid == request.text);
            if (quote is null)
            {
                throw new Exception($"Quote não existe id:{request.text}");
            }
            if (await _context.VoteModels.AnyAsync(v => v.QuoteVoteUid == request.text
            && v.UserSlackId == request.user_id))
            {
                throw new Exception(
                    $"Usuario ja votou para essa quote Uid:{request.user_id} {request.text}");
            }
            return quote;
        }


        private async Task<string> processNewQuoteEvent(SlackEventRequestModel quoteEvent)
        {
            string authorName = null;
            string authorId = null;
            var content = quoteEvent.text;
            if (string.IsNullOrWhiteSpace(quoteEvent.text))
            {
                throw new Exception("Quote vazio");
            }
            var byPosition = quoteEvent.text.LastIndexOf("by:");
            if (byPosition > 0)
            {
                var authorString = quoteEvent.text.Split("by:");
                if (string.IsNullOrWhiteSpace(authorString[0]))
                {
                    throw new Exception("Quote vazio");
                }

                content = authorString[0];

                if (authorString.Count() > 2)
                {
                    authorName = null;
                }
                else
                {

                    authorName = authorString[1].Replace("@", "").Replace(" ", ""); ;

                    var author = await _context.User.FirstOrDefaultAsync(
                        u => u.Name == authorName || u.RealName == authorName
                        );
                    if (author != null)
                    {
                        authorId = author.UserId;
                    }
                }
            }
           return await saveQuoteInDb(quoteEvent, authorId, content);
        }

        private async Task<string> saveQuoteInDb(SlackEventRequestModel quoteEvent, string authorId, string content)
        {
            string quoteVoteUid = await getUniqueQuoteVoteId();
            
            var snitchId = _context.User.FirstOrDefault(u=>u.SlackId==quoteEvent.user_id).UserId;
            var quote = new QuoteModel()
            {
                Content = content,
                SnitchId = snitchId,
                UserId = authorId,
                Date = DateTimeOffset.Now,
                QuoteVoteUid = quoteVoteUid
            };
            await _context.AddAsync(quote);
            await _context.SaveChangesAsync();
            return quoteVoteUid;
        }

        private async Task<string> getUniqueQuoteVoteId()
        {
            string quoteVoteUid;
            do
            {
                quoteVoteUid = RandomString(8);

            } while (await _context.Quote.AnyAsync(q => q.QuoteVoteUid == quoteVoteUid));
            return quoteVoteUid;
        }

        #endregion
    }
}
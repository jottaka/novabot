using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NovaBot;
using NovaBot.Data;
using NovaBot.Models.ViewModels;
using NovaBot.Repositories;
using NovaBot.Tests.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NovaBotTest.Tests
{
    public class QuoteRepositoryTest : BaseTest
    {
        public QuoteRepositoryTest(TestFixture<Startup> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddQuoteTst()
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                await addQuoteTst(ctx);
            }
        }

        [Fact]
        public async Task UpvoteQuoteTestSuccess()
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                await upVoteTestSuccess(ctx);
            }
        }

        [Fact]
        public async Task UpvoteQuoteTwiceTest_throwException()
        {
            await upvoteQuoteTwiceTest();
        }

        [Theory]
        [InlineData("EU DISSE ISSO", "EU DISSE ISSO", "")]
        [InlineData("EU DISSE AQUILO by:", "EU DISSE AQUILO ", "")]
        [InlineData("EU DISSE ISSO by: @USUARIO", "EU DISSE ISSO ", "USUARIO")]
        [InlineData("EU DISSE ISSO by: USUARIO", "EU DISSE ISSO ", "USUARIO")]
        public async Task AddNewQuoteEventTest(string input, string text, string author)
        {
            await addNewQuoteEventTest(input,text, author);
        }

        #region PRIVATE


        private async Task upvoteQuoteTwiceTest()
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                var repository = new QuoteRepository(ctx);
                SlackEventRequestModel upvoteRequest = await getSlackEventRequestModel();

                await repository.UpvoteAsync(upvoteRequest);
                await Assert.ThrowsAnyAsync<Exception>(
                  async () =>
                await repository.UpvoteAsync(upvoteRequest)
                    );
            }
        }
        private async Task upVoteTestSuccess(ApplicationDbContext ctx)
        {
            var repository = new QuoteRepository(ctx);
            SlackEventRequestModel upvoteRequest = await getSlackEventRequestModel();

            await repository.UpvoteAsync(upvoteRequest);

            var quoteInDb = await ctx.Quote.Include(q => q.Votes)
                .FirstOrDefaultAsync();
            var vote = quoteInDb.Votes[0];
            Object.Equals(1, vote.Vote);
            Object.Equals(vote.UserSlackId, "SLACK");
        }

        private async Task<SlackEventRequestModel> getSlackEventRequestModel()
        {
            var userId = await AddUser(false);
            var upvoter = await AddUser(true);
            var quoteVoteUid = await AddQuote(userId, upvoter);

            var upvoteRequest = new SlackEventRequestModel()
            {
                user_id = upvoter,
                text = "1111",
            };
            return upvoteRequest;
        }

        private async Task addNewQuoteEventTest(string input, string text,string  author)
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                var repository = new QuoteRepository(ctx);
                var addedUserId = await AddUser(false);
                var snitchId = await AddUser(true);

                var userId = String.IsNullOrEmpty(author) ? null : addedUserId;

                var slackEvent = new SlackEventRequestModel()
                {
                    user_id = snitchId,
                    text = input,
                };

                await assertAddNewQuoteEventTst(text, ctx, repository, userId, snitchId, slackEvent);
            }
        }

        private static async Task assertAddNewQuoteEventTst(string text, ApplicationDbContext ctx, QuoteRepository repository, string userId, string snitchId, SlackEventRequestModel slackEvent)
        {
            await repository.ReceiveNewQuoteEvent(slackEvent);

            var quoteInDb = await ctx.Quote.FirstOrDefaultAsync();


            Assert.Equal(userId, quoteInDb.UserId);
            Assert.Equal(snitchId, quoteInDb.SnitchId);
            Assert.Equal(text, quoteInDb.Content);
        }


        private async Task addQuoteTst(ApplicationDbContext ctx)
        {
            var repository = new QuoteRepository(ctx);
            var userId = await AddUser(false);
            var snitchId = await AddUser(true);

            var quote = GetFilledQuoteModel(userId,snitchId);
            await repository.AddQuoteAsync(quote);
            var quoteDb = ctx.Quote.FirstOrDefault();

            Assert.NotNull(quoteDb);
            Assert.Equal(quote.Date, quoteDb.Date);
        }
        #endregion
    }


}


using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NovaBot;
using NovaBot.Data;
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
    public class QuoteRepositoryTest:BaseTest 
    {
        public QuoteRepositoryTest(TestFixture<Startup> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddQuoteTst()
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                var repository = new QuoteRepository(ctx);
                var userId = await AddUser();
                var quote = GetFilledQuoteModel(userId);
                await repository.AddQuoteAsync(quote);


                var quoteDb = ctx.Quote.FirstOrDefault();

                Assert.NotNull(quoteDb);
                Assert.Equal(quote.Date, quoteDb.Date);
            }
        }


    }
}

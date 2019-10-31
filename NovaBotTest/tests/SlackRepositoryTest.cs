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
    public class SlackRepositoryTest:BaseTest 
    {
        
        public SlackRepositoryTest(TestFixture<Startup> fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData("/quote 'Minha Frase' @franco", "MinhaFrase","franco")]
        [InlineData("/quote 'Minha Frase'", "Minha Frase", "anônimo")]
        public async Task AddQuoteSlackEventTest(string input, string expectedQuote, string expectedUser)
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                var repository = new SlackRepository(ctx);
                await repository.AddQuoteAsync(input);


                var quoteDb = ctx.Quote.Include(q=> q.User).FirstOrDefault();
               

                Assert.NotNull(quoteDb);
                Assert.Equal(expectedQuote, quoteDb.Content);
                Assert.Equal(expectedUser, quoteDb.User.Name);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NovaBot;
using NovaBot.Data;
using NovaBot.Models;
using NovaBot.Models.ViewModels;
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
    public abstract class BaseTest : IClassFixture<TestFixture<Startup>>
    {
        public DbContextOptions<ApplicationDbContext> options;
        public SqliteConnection connection;
        public BaseTest(TestFixture<Startup> fixture)
        {
            setUpDataBaseConnection();
        }

        private void setUpDataBaseConnection()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            EnsureDatabaseIsCreated(options);
        }

        public static void EnsureDatabaseIsCreated(DbContextOptions<ApplicationDbContext> options)
        {
            // Create the schema in the database
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
            }
        }


        #region helpers

        protected async Task<string> AddQuote(string userId,string snitchId)
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                var quoteModel = GetFilledQuoteModel(userId, snitchId);
                await ctx.Quote.AddAsync(quoteModel);
                await ctx.SaveChangesAsync();
                return quoteModel.QuoteId;
            }
        }

        protected async Task<string> AddUser(bool isSnitch)
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                UserModel user = addUser(isSnitch);

                ctx.Add(user);
                await ctx.SaveChangesAsync();
                return user.UserId;

            }
        }

        private static UserModel addUser(bool isSnitch)
        {
            var name = isSnitch ? "SNITCH" : "USUARIO";
            return new UserModel()
            {
                Name = name,
                Ranking = 0,
                SlackId = "SLACK",
            };
        }

        protected UserViewModel GeFilledtUserViewModel()
        {
            return new UserViewModel()
            {
                Name = "usuario",
                Ranking = 1
            };
        }

        protected QuoteModel GetFilledQuoteModel(string userId, string snitchId)
        {
            return new QuoteModel()
            {
                Content = "frase minha",
                Date = DateTimeOffset.UtcNow,
                Downvotes = 1,
                Upvotes = 1,
                UserId = userId,
                QuoteVoteUid = "1111",
                SnitchId = snitchId,
            };
        }

        #endregion

    }
}

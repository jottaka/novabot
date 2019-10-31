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

        protected async Task<string> AddUser()
        {
            using (var ctx = new ApplicationDbContext(options))
            {
                var user = new UserModel()
                {
                    Name = "usuario",
                    Ranking = 0
                };

                ctx.Add(user);
                await ctx.SaveChangesAsync();
                return user.UserId;

            }
        }

        protected UserViewModel GeFilledtUserViewModel()
        {
            return new UserViewModel()
            {
                Name = "usuario",
                Ranking = 1
            };
        }

        protected QuoteModel GetFilledQuoteModel(string userId)
        {
            return new QuoteModel()
            {
                Content = "frase minha",
                Date = DateTimeOffset.UtcNow,
                Downvotes = 1,
                Upvotes = 1,
                UserId = userId
            };
        }

        #endregion

    }
}

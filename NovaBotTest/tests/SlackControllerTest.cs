using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NovaBot;
using NovaBot.Controllers;
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
    public class SlackControllerTest : BaseTest 
    {
        
        public SlackControllerTest(TestFixture<Startup> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task TestVerification()
        {
            using (var ctx = new ApplicationDbContext(options))
            {

                var repository = new SlackRepository(ctx);
                var request = new SlackEventRequestModel() {
                    challenge = "MAH_XALENDI",
                    token = "MAH_TOQUIN",
                    type = "url_verification"
                };

                var response = await repository.ProcessRequest(request);
                Assert.Equal(request.challenge, response);
            }
        }
    }
}

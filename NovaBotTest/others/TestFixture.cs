using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;


namespace NovaBot.Tests.Others
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {

        private readonly HttpClient Client;
        public readonly TestServer Server;

        public TestFixture()
        {
            var projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            var builder = new WebHostBuilder()
                .UseContentRoot(@"")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("settings.tst.json")
                    .Build()
                )
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            Client = new HttpClient();
        }


        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}

using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Logstore.Menu;

namespace Logstore.Test.Context.Menu
{
    public class MenuProvider : IDisposable
    {
        private readonly TestServer _server;
        public readonly HttpClient _client;
        public MenuProvider()
        {
            _server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(
                    config =>
                    {
                        var integrationConfig = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json")
                          .Build();

                        config.AddConfiguration(integrationConfig);
                    })

                );
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }
    }
}

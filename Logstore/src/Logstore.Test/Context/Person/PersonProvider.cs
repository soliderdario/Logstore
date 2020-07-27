using System;
using System.Net.Http;
using Logstore.Person;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Logstore.Test.Context.Person
{
    public class PersonProvider : IDisposable
    {
        private readonly TestServer _server;
        public readonly HttpClient _client;
        public PersonProvider()
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

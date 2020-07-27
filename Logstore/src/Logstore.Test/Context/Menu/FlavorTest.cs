using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Logdtore.Domain.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logstore.Test.Context.Menu
{
    [TestClass]
    public class FlavorTest
    {
        
        [TestMethod]
        public async Task Create3Queijos()
        {
            var entry = new FlavorView
            {
                Name = "3 Queijos",
                Price = 50.00
            };           
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateFrangoComRequeijao()
        {
            var entry = new FlavorView
            {
                Name = "Frango com requeijão",
                Price = 59.99
            };
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateMussarela()
        {
            var entry = new FlavorView
            {
                Name = "Mussarela",
                Price = 42.50
            };
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateCalabresa()
        {
            var entry = new FlavorView
            {
                Name = "Calabresa",
                Price = 42.50
            };
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CreatePepperoni()
        {
            var entry = new FlavorView
            {
                Name = "Pepperoni",
                Price = 55.00
            };
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CreatePortuguesa()
        {
            var entry = new FlavorView
            {
                Name = "Portuguesa",
                Price = 45.00
            };
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateVeggie()
        {
            var entry = new FlavorView
            {
                Name = "Veggie",
                Price = 59.99
            };
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new MenuProvider()._client;

            var response = await client.PostAsync("/api/v1/flavor/save", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetFlavors()
        {
            var client = new MenuProvider()._client;
            var response = await client.GetAsync("/api/v1/flavor/query");
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }


}

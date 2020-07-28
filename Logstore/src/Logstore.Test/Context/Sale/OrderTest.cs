using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logdtore.Domain.View;

namespace Logstore.Test.Context.Sale
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public async Task CreateOrderYesCustomer()
        {
            var entry = new OrderYesCustomerView
            {
                DateCreate = DateTime.Now,                
                Email ="soliderdario@hotmail.com",                
                Items = new List<OrderItemView>()
            };
            entry.Items.Add( new OrderItemView {
                Flavors = new List<long> {3,4}            
            });
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new SaleProvider()._client;            

            var response = await client.PostAsync("/api/v1/Order/new/yes/customer", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        public async Task CreateOrderNoCustomer()
        {
            var entry = new OrderNoCustomerView
            {
                DateCreate = DateTime.Now,
                Name ="Ingrid Guimarães Martins Leandro",
                Email = "solideringrid@hotmail.com",
                Street = "Rua Jurupira",
                Number = "724",
                Complement = "",
                Neighborhood = "Barra Funda",
                City = "São Paulo",
                PostalCode = "02714-000",
                UF = "SP",
                Items = new List<OrderItemView>()
            };
            entry.Items.Add(new OrderItemView
            {
                Flavors = new List<long> { 3 },                
            });

            entry.Items.Add(new OrderItemView
            {
                Flavors = new List<long> { 1,2 },
            });
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new SaleProvider()._client;

            var response = await client.PostAsync("/api/v1/Order/new/no/customer", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

using Logdtore.Domain.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Logstore.Test.Context.Sale
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public async System.Threading.Tasks.Task CreateOrderYesCustomer()
        {
            var entry = new OrderYesCustomerView
            {
                DateCreate = DateTime.Now,
                Email ="soliderdario@hotmail.com",                
                Items = new List<OrderItemView>()
            };
            entry.Items.Add( new OrderItemView {
                Flavors = new List<long> {8,3}            
            });
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new SaleProvider()._client;
            

            var response = await client.PostAsync("/api/v1/Order/new/yes/customer", new StringContent(payload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


    }
}

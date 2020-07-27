using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logdtore.Domain.View;
using Logstore.Domain.View;
using Logstore.Test.Context.Person;

namespace Logstore.Test.Context.Sale
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public async Task CreateCustomer()
        {
            var entry = new CustomerView
            {
                Name ="Dario Martins Bueno Leandro",              
                Email ="soliderdario@hotmail.com",
                Street = "Rua Baião Parente",
                Number = "396",
                Complement = "AP151 BL02",
                Neighborhood = "Jardim Primavera",
                City = "São Paulo",
                PostalCode = "02735-000",
                UF = "SP",
            };
           
            var payload = System.Text.Json.JsonSerializer.Serialize(entry);
            var client = new PersonProvider()._client;            

            var response = await client.PostAsync("/api/v1/Customer/save", new StringContent(payload, Encoding.UTF8, "application/json"));            
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }       
    }
}

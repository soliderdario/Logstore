using Logdtore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logdtore.Domain.View
{
    public class OrderView
    {       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string UF { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItemView
    {
        public long FlavorId01 { get; set; }
        public decimal Value01 { get; set; }
        public long FlavorId02 { get; set; }
        public decimal Value02 { get; set; }
    }
}

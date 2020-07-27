using Logdtore.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logstore.Domain.Model
{
    [Table("[Order]")]
    public class Order : ModelBase
    {
        public long CustomerId { get; set; }
        public DateTime DateCreate { get; set; }
        public double Total{ get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public List<OrderItem> Items { get; set; }
    }

    [Table("OrderAddressDelivery")]
    public class OrderAddressDelivery:ModelBase
    {
        public long OrderId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string UF { get; set; }
    }

    [Table("OrderItem")]
    public class OrderItem : ModelBase
    {
        public long OrderId { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public List<OrderItemFlavor> Flavors { get; set; }
    }
    [Table("OrderItemFlavor")]
    public class OrderItemFlavor : ModelBase
    {
        public long OrderItemId { get; set; }
        public long FlavorId { get; set; }
        public double Value { get; set; }
    }
}

using Logdtore.Domain.Model;
using System;
using System.Collections.Generic;

namespace Logstore.Domain.Model
{
    public class Order : ModelBase
    {
        public long CustomerId { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem : ModelBase
    {
        public long OrderId { get; set; }
        public List<OrderItemFlavor> Flavors { get; set; }
    }
    public class OrderItemFlavor : ModelBase
    {
        public long OrderItemId { get; set; }
        public long FlavorId { get; set; }
        public decimal Value { get; set; }
    }
}

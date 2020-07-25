using Logdtore.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logstore.Domain.Model
{
    [Table("Order")]
    public class Order : ModelBase
    {
        public long CustomerId { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    [Table("OrderItem")]
    public class OrderItem : ModelBase
    {
        public long OrderId { get; set; }
        public List<OrderItemFlavor> Flavors { get; set; }
    }
    [Table("OrderItemFlavor")]
    public class OrderItemFlavor : ModelBase
    {
        public long OrderItemId { get; set; }
        public long FlavorId { get; set; }
        public decimal Value { get; set; }
    }
}

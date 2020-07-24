using System;
using System.Collections.Generic;

namespace Logdtore.Domain.Model
{
    public class Order:Base
    {        
        public long CustomerId { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> Items { get; set; }       
    }

    public class OrderItem: Base
    {        
        public long OrderId { get; set; }
        public List<OrderItemFlavor> Flavors { get; set; }
    }
    public class OrderItemFlavor:Base
    {       
        public long OrderItemId { get; set; }
        public long FlavorId { get; set; }
        public decimal Value { get; set; }        
    }
}

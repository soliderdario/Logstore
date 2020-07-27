using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logdtore.Domain.View;

using Logstore.Bootstrap;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;

namespace Logstore.Sale.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HistoryController : MainController
    {
        private readonly IOrderRepository _orderRepository;
        public HistoryController(
            IOrderRepository orderRepository,
            INotifier notifier, 
            IMapper mapper) : base(notifier, mapper)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("query/email")]
        public async Task<IEnumerable<OrderHistoryView>> Query(string email)
        {
            try
            {
                var sql ="SELECT O.Id as OrderId, c.Name as CustomerName, O.DateCreate, O.Total " +
                         " FROM Customer C " +
                         " INNER JOIN[Order] O on O.CustomerId = C.Id " +
                         " WHERE c.email = @email" +
                         " ORDER BY O.DateCreate desc";
                var orders = await _orderRepository.Query<OrderHistoryView>(sql, new { email });

                foreach(var order in orders)
                {
                    sql = "SELECT Id as OrderitemId " +
                          " FROM OrderItem " +
                          " WHERE OrderId =@OrderId";
                    var items = await _orderRepository.Query<OrderHistoryItemView>(sql, new { order.OrderId });
                    foreach (var item in items)
                    {
                        sql = "SELECT F.Name as Flavor, OIF.Value FROM OrderItemFlavor OIF " +                             
                              " INNER JOIN Flavor F on F.Id = OIF.FlavorId " +
                              " WHERE OIF.OrderItemId = @OrderItemId";
                        var flavors = await _orderRepository.Query<OrderHistoryItemFlavorView>(sql, new { item.OrderItemId });
                        items.Where(src => src.OrderItemId == item.OrderItemId).FirstOrDefault().Flavors.AddRange(flavors);
                    }
                    orders.Where(src => src.OrderId == order.OrderId).FirstOrDefault().Pizzas.AddRange(items);
                }                
                return orders;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<OrderHistoryView>)BadRequest(ex.Message);
            }
        }
    }
}

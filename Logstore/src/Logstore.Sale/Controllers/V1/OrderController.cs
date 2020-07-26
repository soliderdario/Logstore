using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logstore.Bootstrap;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Logstore.Domain.Model;
using Logdtore.Domain.View;

namespace Logstore.Sale.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : MainController
    {
        private readonly IOrderRepository _orderRepository;        
        public OrderController(
            INotifier notifier,
            IMapper mapper,
            IOrderRepository orderRepository           
            ) : base(notifier, mapper)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("query")]
        public async Task<IEnumerable<OrderView>> Query()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<OrderView>>(await _orderRepository.Query<OrderView>("Select * from Order"));
                return result;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<OrderView>)BadRequest(ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] OrderView orderView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var customer = _mapper.Map<Customer>(orderView);
                await _orderRepository.Save(orderView, customer);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse();
        }

        [HttpDelete("delete/{orderId}")]
        public async Task<IActionResult> Delete(long orderId)
        {
            try
            {
                await _orderRepository.Delete(orderId);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse(orderId);
        }
    }
}

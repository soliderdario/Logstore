using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logstore.Bootstrap;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
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
        public async Task<IEnumerable<OrderNoCustomerView>> Query()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<OrderNoCustomerView>>(await _orderRepository.Query<OrderNoCustomerView>("Select * from Order"));
                return result;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<OrderNoCustomerView>)BadRequest(ex.Message);
            }
        }

        [HttpPost("new/no/customer")]
        public async Task<IActionResult> NewOrderNoCustomer([FromBody] OrderNoCustomerView orderView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);
                
                await _orderRepository.Save(orderView);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse();
        }

        [HttpPost("new/yes/customer")]
        public async Task<IActionResult> NewOrderYesCustomer([FromBody] OrderYesCustomerView orderView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                await _orderRepository.Save(orderView);
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

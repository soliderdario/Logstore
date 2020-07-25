using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logdtore.Domain.Model;
using Logdtore.Domain.View;
using Logstore.Bootstrap;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Logstore.Domain.View;
using Logstore.Domain.Model;

namespace Logstore.Menu.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : MainController
    {

        private readonly ICustomerRepository _customerRepository;
        public CustomerController(
            INotifier notifier,
            IMapper mapper,
            ICustomerRepository customerRepository
            ) : base(notifier, mapper)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("Query")]
        public async Task<IEnumerable<FlavorView>> Query()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<FlavorView>>(await _customerRepository.Query<Flavor>("Select * from Customer order by Name"));
                return result;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<FlavorView>)BadRequest(ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] CustomerView customerView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var customer = _mapper.Map<Customer>(customerView);
                if (customer.Id == 0)
                {
                    await _customerRepository.Insert(customer);
                }
                else
                {
                    await _customerRepository.Update(customer);
                }
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse();
        }

        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> Delete(long customerId)
        {
            try
            {                
                await _customerRepository.Delete(customerId);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse(customerId);
        }
    }
}

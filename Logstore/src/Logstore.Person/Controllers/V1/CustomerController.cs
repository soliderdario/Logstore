using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Logstore.Bootstrap;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Logstore.Domain.View;
using Logstore.Domain.Model;

namespace Logstore.Person.Controllers.V1
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

        [HttpGet("query")]
        public async Task<IEnumerable<CustomerView>> Query()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<CustomerView>>(await _customerRepository.Query<CustomerView>("Select * from Customer order by Name"));
                return result;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<CustomerView>)BadRequest(ex.Message);
            }
        }

        [HttpGet("query/email/{email}")]
        public async Task<IEnumerable<CustomerView>> Query(string email)
        {
            try
            {
                var result = _mapper.Map<IEnumerable<CustomerView>>(await _customerRepository.Query<CustomerView>("Select * from Customer Where email = @email", new { email }));
                return result;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<CustomerView>)BadRequest(ex.Message);
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

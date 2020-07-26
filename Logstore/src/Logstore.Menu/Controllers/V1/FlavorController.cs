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

namespace Logstore.Menu.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FlavorController : MainController
    {

        private readonly IFlavorRepository _flavorRepository;
        public FlavorController(
            INotifier notifier,
            IMapper mapper,
            IFlavorRepository flavorRepository
            ) : base(notifier, mapper)
        {
            _flavorRepository = flavorRepository;
        }

        [HttpGet("query")]
        public async Task<IEnumerable<FlavorView>> Query()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<FlavorView>>(await _flavorRepository.Query<Flavor>("Select Id, Name, Price from Flavor order by Name"));
                return result;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return (IEnumerable<FlavorView>)BadRequest(ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] FlavorView flavorView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var flavor = _mapper.Map<Flavor>(flavorView);
                if (flavor.Id == 0)
                {
                    await _flavorRepository.Insert(flavor);
                }
                else
                {
                    await _flavorRepository.Update(flavor);
                }
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse();
        }

        [HttpDelete("delete/{flavorId}")]
        public async Task<IActionResult> Delete(long flavorId)
        {
            try
            {                
                await _flavorRepository.Delete(flavorId);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse(flavorId);
        }
    }
}

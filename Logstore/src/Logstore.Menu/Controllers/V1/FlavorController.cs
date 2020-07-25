using AutoMapper;
using Logdtore.Domain.Model;
using Logdtore.Domain.View;
using Logstore.Bootstrap;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            try
            {                
                var flavor = new Flavor();                
                flavor.Name = "Queijo";
                flavor.Price = 20.00;
                await _flavorRepository.Insert(flavor);

                flavor.Name += $" update ";
                await _flavorRepository.Update(flavor);
            }
            catch
            {                

            }
            return CustomResponse();
        }

        [HttpGet("automapper")]
        public async Task<IActionResult> Automapper()
        {
            try
            {
                var Id = 11;
                var flavors = await _flavorRepository.Query<Flavor>("Select Id, Name, Price from Flavor where Id = @Id", new {Id});
                var flavorView = _mapper.Map<FlavorView>(flavors.FirstOrDefault());

                flavorView.Name = "Transaction";
                var flavor = _mapper.Map<Flavor>(flavorView);
                await _flavorRepository.Update(flavor);

                
            }
            catch
            {

            }
            return CustomResponse();
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Save([FromBody] bool companyViewModel)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return CustomResponse(ModelState);

                //var company = _mapper.Map<CompanyModel>(companyViewModel);
                //await _companyService.Save(company);
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
            }
            return CustomResponse();
        }
    }
}

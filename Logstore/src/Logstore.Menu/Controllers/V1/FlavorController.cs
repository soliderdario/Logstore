using AutoMapper;
using Logdtore.Domain;
using Logdtore.Domain.Interfaces;
using Logdtore.Domain.Model;
using Logstore.Bootstrap;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlavorRepository _flavorRepository;
        public FlavorController(
            INotifier notifier,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFlavorRepository flavorRepository
            ) : base(notifier, mapper)
        {
            _unitOfWork = unitOfWork;
            _flavorRepository = flavorRepository;
        }
        [HttpGet]
        public IActionResult Test()
        {
            try
            {
                _unitOfWork.Begin();
                var flavor = new Flavor();
                _flavorRepository.Insert(flavor);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();

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

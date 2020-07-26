using AutoMapper;
using Logdtore.Domain.View;
using Logstore.Domain.Model;

namespace Logstore.Bootstrap.AutoMapper
{
    public class SaleAutomapper : Profile
    {
        public SaleAutomapper()
        {            
            CreateMap<OrderView, Customer>();               
        }
    }
}

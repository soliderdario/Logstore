using AutoMapper;
using Logdtore.Domain.View;
using Logstore.Domain.Model;
using Logstore.Domain.View;

namespace Logstore.Bootstrap.AutoMapper
{
    public class SaleAutomapper : Profile
    {
        public SaleAutomapper()
        {
            CreateMap<Order, OrderView>().ReverseMap();
        }
    }
}

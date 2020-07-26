using AutoMapper;
using Logdtore.Domain.View;
using Logstore.Domain.Model;

namespace Logstore.Bootstrap.AutoMapper
{
    public class SaleAutomapper : Profile
    {
        public SaleAutomapper()
        {
            CreateMap<OrderNoCustomerView, Customer>();
            CreateMap<Customer, OrderAddressDelivery>()
                .ForMember(dest => dest.Id, act => act.Ignore());
        }
    }
}

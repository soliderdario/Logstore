using AutoMapper;
using Logdtore.Domain.Model;
using Logdtore.Domain.View;

namespace Logstore.Bootstrap.AutoMapper
{
    public class MenuAutomapper : Profile
    {
        public MenuAutomapper()
        {
            CreateMap<Flavor, FlavorView>().ReverseMap();
        }
    }
}

using AutoMapper;
using Logstore.Domain.Model;
using Logstore.Domain.View;

namespace Logstore.Bootstrap.AutoMapper
{
    public class PersonAutomapper : Profile
    {
        public PersonAutomapper()
        {
            CreateMap<Customer, CustomerView>().ReverseMap();
        }
    }
}

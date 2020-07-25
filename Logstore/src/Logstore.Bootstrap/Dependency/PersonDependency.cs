using Logstore.Data;
using Logstore.Data.Repository;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logstore.Bootstrap.Dependency
{
    public static class PersonDependency
    {
        public static IServiceCollection PersonResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<RepositoryBase>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}


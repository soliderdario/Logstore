using Logstore.Data;
using Logstore.Data.Repository;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logstore.Bootstrap.Dependency
{
    public static class SaleDependency
    {
        public static IServiceCollection SaleResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<RepositoryBase>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}


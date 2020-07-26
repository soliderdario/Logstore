using Logstore.Bootstrap.Swagger;
using Logstore.Data;
using Logstore.Data.Repository;
using Logstore.Domain.Interfaces;
using Logstore.Infrastructure.Notifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Logstore.Bootstrap.Dependency
{
    public static class SaleDependency
    {
        public static IServiceCollection SaleResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SaleSwaggerConfigureSwaggerOptions>();
            services.AddScoped<RepositoryBase>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFlavorRepository, FlavorRepository>();
            return services;
        }
    }
}


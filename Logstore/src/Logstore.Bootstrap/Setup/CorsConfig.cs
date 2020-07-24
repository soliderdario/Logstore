using Microsoft.Extensions.DependencyInjection;

namespace Logstore.Bootstrap.Setup
{
    public static class CorsConfig
    {
        public static IServiceCollection CustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );


                options.AddPolicy("Production",
                    builder =>
                        builder                            
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins("http://test.logstore.com.br", "http://www.logstore.com.br")
                            .SetIsOriginAllowedToAllowWildcardSubdomains());
            });
            return services;
        }
    }
}

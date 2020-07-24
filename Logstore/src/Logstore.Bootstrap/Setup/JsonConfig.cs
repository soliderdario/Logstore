using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Logstore.Bootstrap.Setup
{
    public static class JsonConfig
    {
        public static IServiceCollection CustomJson(this IServiceCollection services)
        {
            //Configure o result to conform class definition
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt =>
                {
                    //Enable this options to serialize json conform class definition
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.Formatting = Formatting.None;
                    if (opt.SerializerSettings.ContractResolver != null)
                    {
                        //Configure o result to conform class definition
                        var resolver = opt.SerializerSettings.ContractResolver as DefaultContractResolver;
                        resolver.NamingStrategy = null;
                    }
                }
                );
            return services;
        }
    }
}

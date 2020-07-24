using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Logstore.Bootstrap.Swagger
{
    public static class PersonSwagger
    {
        public static IServiceCollection CustomPersonSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<DefaultValues>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insert the token JWT this way: Bearer {your token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }
        public static IApplicationBuilder CustomUsePersonSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
               options =>
               {
                   foreach (var description in provider.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
               });
            return app;
        }
    }
    public class PersonSwaggerConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        
        readonly IApiVersionDescriptionProvider provider;
        public PersonSwaggerConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }
        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Logstore Person API - Martins Solutions",
                Version = description.ApiVersion.ToString(),
                Description = "This API contains all the features to be used in person module",
                Contact = new OpenApiContact() { Name = "Dario Martins Bueno Leandro", Email = "dario@martinssolutions.com.br" },
                TermsOfService = new Uri("http://wwww.martinssolutions.com.br"),
                License = new OpenApiLicense() { Name = "Notorious", Url = new Uri("http://www.martinssolutions.com.br") }
            };

            if (description.IsDeprecated)
            {
                info.Description += "This version is Deprecated!";
            }
            return info;
        }
    }
}

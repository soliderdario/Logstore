using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Logstore.Bootstrap.AutoMapper;
using Logstore.Bootstrap.Dependency;
using Logstore.Bootstrap.Setup;
using Logstore.Bootstrap.Swagger;
using Logstore.Data;

namespace Logstore.Menu
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            

            // Json Configuration
            services.CustomJson();

            //Auto Mapper Configuration 
            services.AddAutoMapper(typeof(MenuAutomapper));
            
            //Repository Configuration
            services.Configure<RepositoryBase>(this.Configuration);

            // Dependency Injection Configuration
            services.MenuResolveDependencies(this.Configuration);

            //Api Versioning  Configuration
            services.CustomApiVersioning();

            //Swagger Configuration          
            services.CustomMenuSwagger();

            //Cors Configuration
            services.CustomCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            // Use Cors 
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Production");
                app.UseHsts();
            }

            //Use Culture
            app.UseCulture();

            app.UseHttpsRedirection();

            //Use Swagger
            app.CustomUseMenuSwagger(provider);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

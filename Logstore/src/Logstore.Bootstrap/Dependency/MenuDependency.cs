﻿using Logdtore.Domain;
using Logdtore.Domain.Interfaces;
using Logstore.Data;
using Logstore.Data.Repository;
using Logstore.Infrastructure.Notifiers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore.Bootstrap.Dependency
{
    public static class MenuDependency
    {
        public static IServiceCollection MenuResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFlavorRepository, FlavorRepository>();
            return services;
        }
    }
}

﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineShope.Core.IRepositories;
using OnlineShope.Core.Utilitiy;
using OnlineShope.Infrastructure.Interfaces;
using OnlineShope.Infrastructure.Model;
using OnlineShope.Infrastructure.Repository;
using OnlineShope.Infrastructure.UnitOfWorks;
using OnlineShope.Infrastructure.Utilitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Infrastructure
{
    public static class DIRegister
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void AddUnitOfWork(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddInfraUtility(this IServiceCollection service)
        {
            service.AddSingleton<EncryptionUtility>();
            service.AddSingleton<MyFileUtility>();
        }

   }
}

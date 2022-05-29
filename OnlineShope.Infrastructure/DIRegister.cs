using Microsoft.Extensions.DependencyInjection;
using OnlineShope.Core.Utilitiy;
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
            //service.addscope
        }

        public static void AddInfraUtility(this IServiceCollection service)
        {
            service.AddSingleton<EncryptionUtility>();
        }
    }
}

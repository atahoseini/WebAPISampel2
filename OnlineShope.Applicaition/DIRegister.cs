using Microsoft.Extensions.DependencyInjection;
using OnlineShope.Applicaition.Interfaces;
using OnlineShope.Applicaition.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShope.Applicaition;

public static class DIRegister
{
    public static IServiceCollection AddApplicaitionServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IPermissionServices, PermissionServices>();


        return services;
    }
}

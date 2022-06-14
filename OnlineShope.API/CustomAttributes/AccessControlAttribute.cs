
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShope.Applicaition.Interfaces;

namespace OnlineShope.API.CustomAttributes;

public class AccessControlAttribute : ActionFilterAttribute
{
    public string Permission { get; set; }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //1- Permission
        //2- user
        //3- applicaiton Id
        //gRPC
        var userId = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(q => q.Type == "userId").Value);
        //var roles = GetRoles(userId);
        //var persmissions = GetPersmissions(roles);

        //nokte aval khondan az kash ast ---
        // ya khondan az yek service ke sar akhar bege true ya false ast va kash ham dar on bekhone

        //1- inject IPermissionServices
        //2- Call checkPermission from IPermimissionServices

        // get di instance from HttpContext.RequestServices
        var permissionServices = context.HttpContext.RequestServices.GetService<IPermissionServices>();
        if (!await permissionServices.CheckPermission(userId, Permission))
        {
            context.Result=new BadRequestObjectResult("No access in this action...");
        }
        else
        {
            base.OnActionExecutionAsync(context, next);
        }
    }
}
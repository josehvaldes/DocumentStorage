namespace DocuStorage.Helpers;

using DocuStorate.Common.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{

    public Roles Roles { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];

        if (user == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else 
        {
            var roleUser = (Roles)user.Role;
            
            if (Roles.Root != roleUser &&
                Roles.Root != Roles &&
                !Roles.HasFlag(roleUser))
            {
                context.Result = new JsonResult(new { message = "Method Not Allowed" }) { StatusCode = StatusCodes.Status405MethodNotAllowed };
            }
        }
    }
}
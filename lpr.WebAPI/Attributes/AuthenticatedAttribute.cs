using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace lpr.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticatedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            //UserModel user = (UserModel)filterContext.HttpContext.Items["User"];

            Console.WriteLine("test");

            //if (user == null)
             //   filterContext.Result = new UnauthorizedObjectResult("user is not authorized") { StatusCode = 403 };
        }
    }
}

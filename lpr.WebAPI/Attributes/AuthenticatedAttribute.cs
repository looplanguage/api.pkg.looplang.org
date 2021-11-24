using lpr.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace lpr.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticatedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            string user = (string)filterContext.HttpContext.Items["AccountId"];

            Console.WriteLine(user);

            if (user == null)
                filterContext.Result = new UnauthorizedObjectResult("user is not authorized") { StatusCode = 403 };
        }
    }
}

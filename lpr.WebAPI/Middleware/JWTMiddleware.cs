using System.Collections.Generic;
using System.Linq;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using lpr.Common.Models;

namespace lpr.WebAPI.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILprDbContext dbContext, IJWTService jwtService)
        {
            string token = context.Request.Headers["X-JWT-Token"];
            try
            {
                if (jwtService.IsTokenValid(token))
                {
                    IEnumerable<Claim> claims = jwtService.GetTokenClaims(token);
                    string ?accountId = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Authentication))?.Value;

                    context.Items["AccountId"] = accountId;
                    //context.Items["GitHubAccessToken"] = dbContext.Account.FirstOrDefault(x => x.GithubId == githubId);
                }
            
            }catch(Exception)
            {
                //throw e;
            }
            await _next(context);
        }
    }
}

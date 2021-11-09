using lpr.Common.Interfaces.Contexts;
using lpr.WebAPI.Services;
using System.Security.Claims;

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
                    //string userId = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value;
                    string githubId = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Authentication)).Value;

                    context.Items["GitHubAccessToken"] = dbContext.Account.FirstOrDefault(x => x.GithubId == githubId);
                    //context.Items["GitHubAccessToken"] = dbContext.Account.FirstOrDefault(x => x.Id.ToString() == userId);
                }
            }
            catch
            {
                //ignore catch
            }
            await _next(context);
        }
    }
}

using System.Security.Claims;

namespace lpr.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> ValidateGitHubAccessToken(string token);
        string GenerateJWT(Claim claim);
    }
}

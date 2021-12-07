using System.Security.Claims;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> ValidateGitHubAccessToken(string token);
        string GenerateJWT(Claim claim);
    }
}

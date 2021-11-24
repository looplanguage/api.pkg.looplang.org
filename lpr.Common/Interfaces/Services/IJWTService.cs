using lpr.Common.Interfaces.Data;
using System.Security.Claims;

namespace lpr.Common.Interfaces.Services
{
    public interface IJWTService
    {
        bool IsTokenValid(string token);
        string GenerateToken(IJWTContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}

using lpr.Common.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lpr.WebAPI.Services
{
    public interface IJWTService
    {
        bool IsTokenValid(string token);
        string GenerateToken(IJWTContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}

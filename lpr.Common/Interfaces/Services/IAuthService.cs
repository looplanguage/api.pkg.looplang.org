using System.Security.Claims;
using lpr.Common.Dtos.In;
using lpr.Common.Models;

namespace lpr.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Account> GetAccountById(Guid accountId);
        Task<Account> UpdateAccount(UpdateAccountDtoIn newAccountData, Guid accountId);
        Task<string> ValidateGitHubAccessToken(string token);
        string GenerateJWT(Claim claim);
    }
}

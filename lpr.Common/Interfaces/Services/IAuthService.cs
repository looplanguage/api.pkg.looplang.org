namespace lpr.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> ValidateGitHubAccessToken(string token);
    }
}

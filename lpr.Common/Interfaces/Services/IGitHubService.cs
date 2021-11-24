using lpr.Common.Models;

namespace lpr.Common.Interfaces.Services
{
    public interface IGitHubService
    {
        Task<GithubUser> GetGithubUser(string token);
        Task<Account> GetRegisteredUser(int githubId);
        Task<Account> Register(GithubUser githubUser);
    }
}

using System.Threading.Tasks;
using lpr.Common.Models;

namespace lpr.Common.Interfaces.Services
{
    public interface IGitHubService
    {
        Task<GithubUser> GetGithubUser(string token);
        Task<Account> GetRegisteredUser(int githubId);
        Account Register(GithubUser githubUser);
    }
}

using System.Threading.Tasks;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using Octokit;
using Account = lpr.Common.Models.Account;

namespace lpr.Logic.Services
{
    public class GitHubService: IGitHubService
    {
        private readonly string? _clientId;//Unused
        private readonly string? _clientSecret;//Unused
        private readonly IAccountData _accountData;

        public GitHubService(string? clientId, string? clientSecret, IAccountData accountData)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _accountData = accountData;
        }

        public async Task<GithubUser> GetGithubUser(string authKey)
        {
            var github = new GitHubClient(new ProductHeaderValue("LPR"));
            github.Credentials = new Credentials(authKey);
            var user = await github.User.Current();

            return new GithubUser() {
              Id= user.Id,
              Name= user.Name,
              Email= user.Email,
              AvatarUrl= user.AvatarUrl,
            };
        }

        public async Task<Account> GetRegisteredUser(int githubId)
        {
            return await _accountData.GetAccountLinkedToGithub(githubId);
        }

        public Account Register(GithubUser githubUser)
        {
            return _accountData.RegisterGithubAccount(githubUser);
        }
    }
}

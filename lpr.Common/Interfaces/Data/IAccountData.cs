using lpr.Common.Models;

namespace lpr.Common.Interfaces.Data {
  public interface IAccountData {
    public Task<Account> GetAccountById(Guid accountId);
    public Task<bool> UpdateAccount(Account account);
    public Task<Account> GetAccountLinkedToGithub(int githubId);
    public Task<Account> RegisterGithubAccount(GithubUser githubUser);
  }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lpr.Common.Models;

namespace lpr.Common.Interfaces.Data {
  public interface IAccountData {
    public Task<Account> GetAccountLinkedToGithub(int githubId);
    public Task<Account> RegisterGithubAccount(GithubUser githubUser);
  }
}

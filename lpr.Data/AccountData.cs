using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace lpr.Data {
  public class AccountData : IAccountData {
    private readonly ILprDbContext _ctx;

    public AccountData(ILprDbContext ctx) { _ctx = ctx; }

    public async Task<Account> GetAccountById(Guid accountId) {
      return await _ctx.Account.Where(a => a.Id == accountId).FirstOrDefaultAsync();
    }

    public async Task<Account> GetAccountLinkedToGithub(int githubId) {
      return await _ctx.Account.Where(a => a.GithubId == githubId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAccount(Account account) {
      Account accountToUpdate = await _ctx.Account.Where(a => a.Id == account.Id).FirstOrDefaultAsync();

      accountToUpdate = account;
      _ctx.SaveChanges();
      return true;
    }

    public async Task<Account> RegisterGithubAccount(GithubUser githubUser) {
        Account account = new Account{
            GithubId = githubUser.Id,
            Id = new Guid(),
            Name = githubUser.Name,
            Logo = githubUser.AvatarUrl,
            Created = DateTime.Now,
        };

        _ctx.Add(account);
        _ctx.SaveChanges();
      
        return account;
    }
  }
}

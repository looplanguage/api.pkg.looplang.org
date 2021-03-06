using System;
using System.Linq;
using System.Threading.Tasks;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace lpr.Data {
  public class AccountData : IAccountData {
    private readonly ILprDbContext _ctx;

    public AccountData(ILprDbContext ctx) { _ctx = ctx; }

    public async Task<Account> GetAccountById(Guid accountId) {
      return await _ctx.Account.Include(a => a.AccountIdentifiers).Where(a => a.Id == accountId).FirstOrDefaultAsync();
    }

    public async Task<Account> GetAccountLinkedToGithub(int githubId) {
      return await _ctx.Account.Include(a => a.AccountIdentifiers).Where(a => a.AccountIdentifiers!.GithubId == githubId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAccount(Account account) {
      Account accountToUpdate = await _ctx.Account.Where(a => a.Id == account.Id).FirstOrDefaultAsync();

      accountToUpdate = account;
      _ctx.SaveChanges();
      return true;
    }

    public Account RegisterGithubAccount(GithubUser githubUser) {
        AccountIdentifiers identifiers = new AccountIdentifiers(){GithubId = githubUser.Id};
        Account account = new Account{
            AccountIdentifiers = identifiers,
            Id = new Guid(),
            Name = githubUser.Name,
            Logo = githubUser.AvatarUrl,
            Created = DateTime.Now,
        };

        _ctx.Add(identifiers);
        _ctx.Add(account);
        _ctx.SaveChanges();
      
        return account;
    }

    public Account Register(Account account)
    {
        _ctx.Add(account);
        _ctx.SaveChanges();
        return account;
    }
  }
}

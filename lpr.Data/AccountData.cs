using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace lpr.Data {
  public class AccountData : IAccountData {
    private readonly ILprDbContext _ctx;

    public AccountData(ILprDbContext ctx) { _ctx = ctx; }

    public async Task<Account> GetAccountLinkedToGithub(int githubId) {
      return await _ctx.Account.Where(a => a.GithubId == githubId).FirstOrDefaultAsync();
    }

    public Account RegisterGithubAccount(GithubUser githubUser) {
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

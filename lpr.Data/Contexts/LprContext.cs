using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version = lpr.Common.Models.Version;

namespace lpr.Data.Contexts {
  public class LprContext : DbContext, ILprDbContext {
    public DbSet<Account> Account { get; set; }
    public DbSet<Organisation> Organisation { get; set; }
    public DbSet<Package> Package { get; set; }
    public DbSet<Version> Version { get; set; }

    public LprContext(DbContextOptions<LprContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder model) {
      model.Entity<Account>().HasKey(c => c.Id);
      model.Entity<Account>().HasAlternateKey(c => c.GithubId);
    }
  }
}

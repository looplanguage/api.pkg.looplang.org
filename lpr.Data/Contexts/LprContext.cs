using lpr.Common.Interfaces.Contexts;
using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Version = lpr.Common.Models.Version;

namespace lpr.Data.Contexts {
  public class LprContext : DbContext, ILprDbContext
  {
    public DbSet<Account> Account { get; set; } = null!;
    public DbSet<AccountIdentifiers> AccountIdentifiers { get; set; } = null!;
    public DbSet<Organisation> Organisation { get; set; } = null!;
    public DbSet<Package> Package { get; set; } = null!;
    public DbSet<Version> Version { get; set; } = null!;
    public DbSet<OrganisationMember> OrganisationMember  { get; set; } = null!;

    public LprContext(DbContextOptions<LprContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder model) {
      model.Entity<Account>().HasKey(c => c.Id);
    }
  }
}

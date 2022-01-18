using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version = lpr.Common.Models.Version;

namespace lpr.Common.Interfaces.Contexts {
  public interface ILprDbContext : IDbContext, IDisposable {
    public DbSet<Account> Account { get; set; }
    public DbSet<Organisation> Organisation { get; set; }
    public DbSet<Package> Package { get; set; }
    public DbSet<Version> Version { get; set; }
    public DbSet<PackageMember> PackageMember  { get; set; }
    public DbSet<OrganisationMember> OrganisationMember  { get; set; }
  }
}

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
  public class PackageData : IPackageData {
    private readonly ILprDbContext _ctx;

    public PackageData(ILprDbContext ctx) { _ctx = ctx; }

    public async Task<List<Package>> GetPackagesPaginatedAsync(int page,
                                                               int amount) {
      return await _ctx.Package.Skip(page * amount).Take(amount).ToListAsync();
    }

    public async Task<List<Package>> GetTopPackagesAsync(int amount) {
      throw new NotImplementedException();
      // TODO: Because Downloads are based on the collective downloads of the
      // versions, this method might need to move to the VersionService. return
      // await _ctx.Package.Include(v => v.Versions).OrderByDescending(p =>
      // p.Downloads).ToListAsync();
    }

    public async Task<List<Package>>
    GetPackagesFromOrganisationAsync(Guid organisationId) {
      Organisation organisation;
      try {
        organisation = await _ctx.Organisation.Include(p => p.Packages)
                           .Where(o => o.Id == organisationId)
                           .FirstAsync();
      } catch {
        throw new ArgumentException("Organisation does not exist.");
      }

      return organisation.Packages;
    }

    public async Task<Package> CreatePackageAsync(Package newPackage) {
      // Do we also need to create the first 'fallback' version?
      newPackage.Id = Guid.NewGuid();
      newPackage.Created = DateTime.Now;

      await _ctx.Package.AddAsync(newPackage);
      _ctx.SaveChanges(); // TODO: Change to SaveChangesAsync (not available for
      // some reason??)

      return newPackage;
    }

    public async Task<Package> GetFullPackageAsync(Guid packageId) {
      return await _ctx.Package.Include(v => v.Versions)
          .FirstAsync(p => p.Id == packageId);
    }

    public async Task<Package> ArchivePackageAsync(Guid packageId) {
      throw new NotImplementedException();
      // return await _ctx.Package.FindAsync(p => p.Id == packageId)
    }
  }
}

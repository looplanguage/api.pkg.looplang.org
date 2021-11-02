using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services {
  public class PackageService : IPackageService {
    private readonly IPackageData _data;
    public PackageService(IPackageData data) { _data = data; }

    public async Task<List<Package>> GetPackagesPaginatedAsync(int page,
                                                               int amount) {
      return await _data.GetPackagesPaginatedAsync(page, amount);
    }

    public async Task<List<Package>> GetTopPackagesAsync(int amount) {
      return await _data.GetTopPackagesAsync(amount);
    }

    public async Task<List<Package>>
    GetPackagesFromOrganisationAsync(Guid organisationId) {
      return await _data.GetPackagesFromOrganisationAsync(organisationId);
    }

    public async Task<Package> CreatePackageAsync(Package newPackage) {
      return await _data.CreatePackageAsync(newPackage);
    }

    public async Task<Package> GetFullPackageAsync(Guid packageId) {
      return await _data.GetFullPackageAsync(packageId);
    }

    public async Task<Package> ArchivePackageAsync(Guid packageId) {
      return await _data.ArchivePackageAsync(packageId);
    }
  }
}

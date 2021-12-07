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
    private readonly IPackageData _packageData;
    public PackageService(IPackageData data) { _packageData = data; }

    public async Task<List<Package>> GetPackagesPaginatedAsync(int page, int amount) {
      return await _packageData.GetPackagesPaginatedAsync(page, amount);
    }

    public async Task<List<Package>> GetTopPackagesAsync(int amount) {
      return await _packageData.GetTopPackagesAsync(amount);
    }

    public async Task<List<Package>> GetPackagesFromOrganisationAsync(Guid organisationId) {
      return await _packageData.GetPackagesFromOrganisationAsync(organisationId);
    }

    public async Task<Package> CreatePackageAsync(Package newPackage) {
      return await _packageData.CreatePackageAsync(newPackage);
    }

    public async Task<Package> GetFullPackageAsync(Guid packageId) {
      return await _packageData.GetFullPackageAsync(packageId);
    }

    public async Task<Package> ArchivePackageAsync(Guid packageId) {
      return await _packageData.ArchivePackageAsync(packageId);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lpr.Common.Models;

namespace lpr.Common.Interfaces.Services {
  public interface IPackageService {
    public Task<List<Package>> GetPackagesPaginatedAsync(int page, int amount);
    public Task<List<Package>> GetTopPackagesAsync(int amount);
    public Task<List<Package>> GetPackagesFromOrganisationAsync(Guid organisationId);
    public Task<List<Package>> GetPackagesFromAccountAsync(Guid accountId);
    public Task<Package> CreatePackageAsync(Guid? organisationId, Guid accountId, Package newPackage);
    public Task<Package> GetFullPackageAsync(Guid packageId); // TODO: Add auth
    public Task<Package> ArchivePackageAsync(Guid packageId); // TODO: Add auth
  }
}

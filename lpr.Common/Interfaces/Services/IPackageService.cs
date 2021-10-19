﻿using lpr.Common.Models;

namespace lpr.Common.Interfaces.Services
{
public interface IPackageService
{
    public Task<List<Package>> GetPackagesPaginatedAsync(int page, int amount);
    public Task<List<Package>> GetTopPackagesAsync(int amount);
    public Task<Package> CreatePackageAsync(Package newPackage);//TODO: Add auth
    public Task<Package> GetFullPackageAsync(Guid packageId);//TODO: Add auth
    public Task<Package> ArchivePackageAsync(Guid packageId);//TODO: Add auth
}
}

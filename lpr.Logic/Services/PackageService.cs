using lpr.Common.Interfaces;
using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
    public class PackageService : IPackageService
    {
        public Task<List<Package>> GetPackagesPaginatedAsync(int page, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<List<Package>> GetTopPackagesAsync(int amount)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreatePackage(Package newPackage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ArchivePackage(Guid packageId)
        {
            throw new NotImplementedException();
        }

    }
}

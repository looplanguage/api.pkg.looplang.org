using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces
{
    public interface IPackageData
    {
        public Task<List<Package>> GetPackagesPaginatedAsync(int page, int amount);
        public Task<List<Package>> GetTopPackagesAsync(int amount);
        public Task<bool> CreatePackage(Package newPackage);
        public Task<bool> ArchivePackage(Guid packageId);
    }
}

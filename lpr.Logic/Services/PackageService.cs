using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<Package> CreatePackageAsync(Package newPackage) {
            if (newPackage == null)
                throw new ApiException(500, new ErrorMessage("Package null", "The given package was null"));

            if (String.IsNullOrWhiteSpace(newPackage.Name) || newPackage.Name.Length < 5)
                throw new ApiException(400, new ErrorMessage("Package name too Short", "The package name was null, whitespace or too short"));

            if (!Utilities.ValidateName(newPackage.Name))
                throw new ApiException(400, new ErrorMessage("Package name not allowed", "The package name cannot contain numbers, or special characters"));
                
            if (newPackage.Id == Guid.Empty || newPackage.Created == DateTime.MinValue)
                throw new ApiException(500, new ErrorMessage("Package Created Incorrecly", "the package was not created in the correct way"));

            //TODO Check if account or user already have a package with this name

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

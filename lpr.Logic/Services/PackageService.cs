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
        private readonly IPackageData _packageData;
        private readonly IAccountData _accountData;
        public PackageService(IPackageData packageContext, IAccountData accountContext)
        { 
            _packageData = packageContext;
            _accountData = accountContext;
        }

        public async Task<List<Package>> GetPackagesPaginatedAsync(int page, int amount) {
            return await _packageData.GetPackagesPaginatedAsync(page, amount);
        }

        public async Task<List<Package>> GetTopPackagesAsync(int amount) {
            return await _packageData.GetTopPackagesAsync(amount);
        }

        public async Task<List<Package>> GetPackagesFromOrganisationAsync(Guid organisationId)
        {
            return await _packageData.GetPackagesFromOrganisationAsync(organisationId);
        }

        public async Task<List<Package>> GetPackagesFromAccountAsync(Guid accountId)
        {
            return await _packageData.GetPackagesFromAccountAsync(accountId);
        }

        public async Task<Package> CreatePackageAsync(Guid? orgId, Guid accId, Package newPackage) {
            if (newPackage == null)
                throw new ApiException(500, new ErrorMessage("Package null", "The given package was null"));

            if (String.IsNullOrWhiteSpace(newPackage.Name) || newPackage.Name.Length < 5)
                throw new ApiException(400, new ErrorMessage("Package name too Short", "The package name was null, whitespace or too short"));

            if (!Utilities.ValidateName(newPackage.Name))
                throw new ApiException(400, new ErrorMessage("Package name not allowed", "The package name cannot contain numbers, or special characters"));

            if (newPackage.Id == Guid.Empty || newPackage.Created == DateTime.MinValue)
                throw new ApiException(500, new ErrorMessage("Package Created Incorrecly", "The package was not created in the correct way"));

            Account account = await _accountData.GetAccountById(accId);
            if(account == null)
                throw new ApiException(400, new ErrorMessage("Account doesn't exist", "The provided Account doesn't exist."));

            if(orgId == null)//Create package under Account
            {
                //TODO: Check if Account already has a package with this name
                return await _packageData.CreatePackageAsync(null, account.Id, newPackage);
            }
            else//Create package under Organisation
            {
                //TODO: Check if Account has rights to this Organisation.
                //TODO: Check if Organisation already has a package with this name
                return await _packageData.CreatePackageAsync(orgId, account.Id, newPackage);
            }
        }

        public async Task<Package> GetFullPackageAsync(Guid packageId) {
            return await _packageData.GetFullPackageAsync(packageId);
        }

        public async Task<Package> ArchivePackageAsync(Guid packageId) {
            Package package = await _packageData.GetFullPackageAsync(packageId);

            if (package == null)
                throw new ApiException(404, 
                    new ErrorMessage("Package not found", "The package you were trying to archive was not found"));

            return await _packageData.ArchivePackageAsync(package);
        }

        public async Task<Package> GetFullPackageByNameAsync(string packageName)
        {
            return await _packageData.GetFullPackageByNameAsync(packageName);
        }
    }
}

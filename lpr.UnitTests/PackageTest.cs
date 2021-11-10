using System;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using lpr.Logic.Services;
using Moq;
using Xunit;

namespace lpr.Tests
{
    public class PackageTest
    {
        public Mock<PackageData> packageData;
        public IPackageService packageService;

        public PackageTest()
        {
            this.packageData =
                new Mock<PackageData>(DatabaseMoq.GetDatabaseContext());
            this.packageService =
                new PackageService(this.packageData.Object);
        }

        [Theory]
        public void Get_Package_By_Id(Guid packId)
        {
            var pack = this.packageService.GetFullPackageAsync(packId).Result;

            Assert.IsType<Package>(pack);
        }
    }
}

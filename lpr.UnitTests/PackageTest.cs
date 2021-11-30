using System.Threading.Tasks;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Logic.Services;
using lpr.Tests.Faker;
using Moq;
using Xunit;

namespace lpr.Tests {
    public class PackageTest {
        public Mock<IPackageData> PackageDataMock;

        public PackageTest() { this.PackageDataMock = new Mock<IPackageData>(); }

        [Fact]
        public async void Get_Package_By_Id_True() {
            Package TestPackage = PackageFaker.Faker();
            this.PackageDataMock.Setup(d => d.GetFullPackageAsync(TestPackage.Id))
                .Returns(Task.FromResult(TestPackage));

            IPackageService packageService =
                new PackageService(this.PackageDataMock.Object);
            var pack = await packageService.GetFullPackageAsync(TestPackage.Id);

            Assert.Equal(pack, TestPackage);
        }

        [Fact]
        public async void Create_Package_True()
        {
            Package testPackage = PackageFaker.Faker();

            PackageDataMock.Setup(d => d.CreatePackageAsync(testPackage))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);

            Package package = await packageService.CreatePackageAsync(testPackage);

            Assert.Equal(package, testPackage);
        }

        [Fact]
        public async void Get_Archive_Package_True()
        {
            Package testPackage = PackageFaker.Faker();

            PackageDataMock.Setup(d => d.ArchivePackageAsync(testPackage.Id))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);
            Package package = new Package();
            try
            {
                 package = await packageService.ArchivePackageAsync(testPackage.Id);
            }catch(ApiException e)
            {
                Assert.True(false, e.Message);
            }

            Assert.Equal(testPackage, package);
        }

        [Fact]
        public async void Create_Package_False()
        {
            Package testPackage = PackageFaker.Faker();

            testPackage.Name = "";

            PackageDataMock.Setup(d => d.ArchivePackageAsync(testPackage.Id))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);
            Package package = new Package();
            try
            {
                package = await packageService.ArchivePackageAsync(testPackage.Id);
            }
            catch (ApiException e)
            {
                Assert.True(false, e.Message);
            }

            Assert.Equal(testPackage, package);
        }
    }
}

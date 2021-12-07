using System.Threading.Tasks;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Logic;
using lpr.Logic.Services;
using lpr.Tests.Faker;
using Moq;
using Xunit;

namespace lpr.Tests {
    public class PackageTest {
        public Mock<IPackageData> PackageDataMock;

        public PackageTest() { this.PackageDataMock = new Mock<IPackageData>(); }

        [Fact]
        public async void GetFullPackageAsync_True() {
            Package TestPackage = PackageFaker.Faker();
            this.PackageDataMock.Setup(d => d.GetFullPackageAsync(TestPackage.Id))
                .Returns(Task.FromResult(TestPackage));

            IPackageService packageService =
                new PackageService(this.PackageDataMock.Object);
            var pack = await packageService.GetFullPackageAsync(TestPackage.Id);

            Assert.Equal(pack, TestPackage);
        }

        [Fact]
        public async void GetFullPackageAsync_False()
        {
            Package TestPackage = PackageFaker.Faker();
            this.PackageDataMock.Setup(d => d.GetFullPackageAsync(TestPackage.Id))
                .Returns(Task.FromResult(TestPackage));

            IPackageService packageService =
                new PackageService(this.PackageDataMock.Object);
            Package pack = await packageService.GetFullPackageAsync(System.Guid.Empty);

            Assert.Null(pack);
        }

        [Fact]
        public async void CreatePackageAsync_True()
        {
            Package testPackage = PackageFaker.Faker();

            PackageDataMock.Setup(d => d.CreatePackageAsync(testPackage))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);

            Package package = await packageService.CreatePackageAsync(testPackage);

            Assert.Equal(package, testPackage);
        }

        [Fact]
        public async void ArchivePackageAsync_True()
        {
            Package testPackage = PackageFaker.Faker();

            PackageDataMock.Setup(d => d.ArchivePackageAsync(testPackage))
                .ReturnsAsync(testPackage);

            PackageDataMock.Setup(d => d.GetFullPackageAsync(testPackage.Id))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);

            Package package = await packageService.ArchivePackageAsync(testPackage.Id);

            Assert.Equal(testPackage, package);
        }

        [Fact]
        public async void ArchivePackageAsync_Throws_ApiExecption_No_Pacakage()
        {
            Package testPackage = PackageFaker.Faker();

            PackageDataMock.Setup(d => d.ArchivePackageAsync(testPackage))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => packageService.ArchivePackageAsync(System.Guid.Empty));
        }

        [Fact]
        public async void CreatePackageAsync_Throws_ApiException_String_Empty()
        {
            Package testPackage = PackageFaker.Faker();

            testPackage.Name = "";

            PackageDataMock.Setup(d => d.CreatePackageAsync(testPackage))
                .ReturnsAsync(testPackage);
            
            IPackageService packageService = new PackageService(PackageDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => packageService.CreatePackageAsync(testPackage));
        }

        [Fact]
        public async void CreatePackageAsync_Throws_ApiException_Invalid_Character()
        {
            Package testPackage = PackageFaker.Faker();

            testPackage.Name = "TestPackage#";

            PackageDataMock.Setup(d => d.CreatePackageAsync(testPackage))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => packageService.CreatePackageAsync(testPackage));
        }


        [Fact]
        public async void CreatePackageAsync_Throws_ApiException_String_TooShort()
        {
            Package testPackage = PackageFaker.Faker();

            testPackage.Name = "test";

            PackageDataMock.Setup(d => d.CreatePackageAsync(testPackage))
                .ReturnsAsync(testPackage);

            IPackageService packageService = new PackageService(PackageDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => packageService.CreatePackageAsync(testPackage));
        }
    }
}

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

    public PackageTest() {
        this.PackageDataMock = new Mock<IPackageData>();
    }

    [Fact]
    public async void Get_Package_By_Id()
    {
        Package TestPackage = PackageFaker.Faker();
        this.PackageDataMock.Setup(d => d.GetFullPackageAsync(TestPackage.Id)).Returns(Task.FromResult(TestPackage));

        IPackageService packageService = new PackageService(this.PackageDataMock.Object);
        var pack = await packageService.GetFullPackageAsync(TestPackage.Id);

        Assert.IsType<Package>(pack);
    }
}
}

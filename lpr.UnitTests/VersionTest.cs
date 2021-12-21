using Amazon.S3;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Logic;
using lpr.Logic.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace lpr.Tests
{
    public class VersionTest
    {
        public Mock<IVersionData> versionDataMock;
        public Mock<IAmazonS3> s3ClientMock;
        public Mock<IPackageService> packageServiceMock;

        public VersionTest()
        {
            versionDataMock = new Mock<IVersionData>();
            s3ClientMock =  new Mock<IAmazonS3>();
            packageServiceMock = new Mock<IPackageService>();
        }

        [Fact]
        public void CreateVersion_True()
        {
            Package package = new Package( new Common.Dtos.In.PackageDtoIn() { Name = "TestPackage"});
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);

            versionDataMock.Setup(x => x.AddVersion(version, package));
            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);


            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);
            //service.CreatVersion();
        }

        
    }
}

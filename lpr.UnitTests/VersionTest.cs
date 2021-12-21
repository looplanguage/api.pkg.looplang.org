using Amazon.S3;
using Amazon.S3.Model;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Logic;
using lpr.Logic.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace lpr.Tests
{
    public class VersionTest
    {
        public Mock<IVersionData> versionDataMock;
        public Mock<IAmazonS3> s3ClientMock;
        public Mock<IPackageService> packageServiceMock;
        public Mock<IFormFile> fileMock;
        public VersionTest()
        {
            versionDataMock = new Mock<IVersionData>();
            s3ClientMock =  new Mock<IAmazonS3>();
            packageServiceMock = new Mock<IPackageService>();
            fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async void CreateVersion_True_ZipFile()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);


            MemoryStream stream = new MemoryStream();

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = package.Id.ToString(),
                UseClientRegion = true
            };

            versionDataMock.Setup(x => x.AddVersion(version, package));

            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);
            s3ClientMock.Setup(x => x.PutBucketAsync(putBucketRequest, CancellationToken.None));

            fileMock.Setup(x => x.ContentType).Returns("application/zip");
            fileMock.Setup(x => x.CopyTo(stream));

            packageServiceMock.Setup(x => x.GetFullPackageAsync(package.Id)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);
            Common.Models.Version v = await service.AddVersion(fileMock.Object, version, package.Id.ToString());
            Assert.Equal(version, v);
        }
        [Fact]
        public async void CreateVersion_True_TarFile()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);


            MemoryStream stream = new MemoryStream();

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = package.Id.ToString(),
                UseClientRegion = true
            };

            versionDataMock.Setup(x => x.AddVersion(version, package));

            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);
            s3ClientMock.Setup(x => x.PutBucketAsync(putBucketRequest, CancellationToken.None));

            fileMock.Setup(x => x.ContentType).Returns("application/x-tar");
            fileMock.Setup(x => x.CopyTo(stream));

            packageServiceMock.Setup(x => x.GetFullPackageAsync(package.Id)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);
            Common.Models.Version v = await service.AddVersion(fileMock.Object, version, package.Id.ToString());
            Assert.Equal(version, v);
        }

        [Fact]
        public async void CreateVersion_True_HigherVersion()
        {
            Package package = new Package(new Common.Dtos.In.PackageDtoIn() { Name = "TestPackage" });
            (var ver, bool _) = Utilities.GetSemVerFromString("0.1.0");
            package.Versions.Add(ver);
            string semVer = "0.2.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);


            MemoryStream stream = new MemoryStream();

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = package.Id.ToString(),
                UseClientRegion = true
            };

            versionDataMock.Setup(x => x.AddVersion(version, package));

            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);
            s3ClientMock.Setup(x => x.PutBucketAsync(putBucketRequest, CancellationToken.None));

            fileMock.Setup(x => x.ContentType).Returns("application/x-tar");
            fileMock.Setup(x => x.CopyTo(stream));

            packageServiceMock.Setup(x => x.GetFullPackageAsync(package.Id)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);
            Common.Models.Version v = await service.AddVersion(fileMock.Object, version, package.Id.ToString());
            Assert.Equal(version, v);
        }

        [Fact]
        public async void CreateVersion_False_WrongFileFormat()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);


            MemoryStream stream = new MemoryStream();

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = package.Id.ToString(),
                UseClientRegion = true
            };

            versionDataMock.Setup(x => x.AddVersion(version, package));

            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);
            s3ClientMock.Setup(x => x.PutBucketAsync(putBucketRequest, CancellationToken.None));

            fileMock.Setup(x => x.ContentType).Returns("application/pdf");
            fileMock.Setup(x => x.CopyTo(stream));

            packageServiceMock.Setup(x => x.GetFullPackageAsync(package.Id)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.AddVersion(fileMock.Object, version, package.Id.ToString()));
        }

        [Fact]
        public async void CreateVersion_False_Version_Too_Small()
        {
            Package package = LoopFaker.Package();
            (var v, bool _) = Utilities.GetSemVerFromString("0.2.0");
            package.Versions.Add(v);
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);


            MemoryStream stream = new MemoryStream();

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = package.Id.ToString(),
                UseClientRegion = true
            };

            versionDataMock.Setup(x => x.AddVersion(version, package));

            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);
            s3ClientMock.Setup(x => x.PutBucketAsync(putBucketRequest, CancellationToken.None));

            fileMock.Setup(x => x.ContentType).Returns("application/zip");
            fileMock.Setup(x => x.CopyTo(stream));

            packageServiceMock.Setup(x => x.GetFullPackageAsync(package.Id)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.AddVersion(fileMock.Object, version, package.Id.ToString()));
        }

        [Fact]
        public async void CreateVersion_False_No_Package()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);


            MemoryStream stream = new MemoryStream();

            var putBucketRequest = new PutBucketRequest
            {
                BucketName = package.Id.ToString(),
                UseClientRegion = true
            };

            versionDataMock.Setup(x => x.AddVersion(version, package));

            s3ClientMock.Setup(x => x.DoesS3BucketExistAsync(semVer)).ReturnsAsync(true);
            s3ClientMock.Setup(x => x.PutBucketAsync(putBucketRequest, CancellationToken.None));

            fileMock.Setup(x => x.ContentType).Returns("application/zip");
            fileMock.Setup(x => x.CopyTo(stream));

            packageServiceMock.Setup(x => x.GetFullPackageAsync(package.Id));

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.AddVersion(fileMock.Object, version, package.Id.ToString()));
        }

        [Fact]
        public async Task GetVersion()
        {

        }

        public async Task GetLatestVersion()
        {

        }

        public void DeprecateVersion()
        {

        }
    }
}

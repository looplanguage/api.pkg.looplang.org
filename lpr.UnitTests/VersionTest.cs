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
        public async Task GetVersion_True()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool res) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            GetObjectResponse getObjectResponse = new GetObjectResponse() {
                BucketName = package.Id.ToString(),
                Key = Utilities.GetStringFromVersion(version)
            };

            s3ClientMock.Setup(x => x.GetObjectAsync(It.Is<GetObjectRequest>(x => x.Key == getObjectResponse.Key && x.BucketName == getObjectResponse.BucketName), default)).ReturnsAsync(getObjectResponse);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            GetObjectResponse objectResponse = await service.GetVersion(package.Name, version);

            Assert.Equal(getObjectResponse, objectResponse);
        }

        [Fact]
        public async Task GetVersion_False_Wrong_version()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            (Common.Models.Version wrongVersion, bool _) = Utilities.GetSemVerFromString("0.2.0");

            await Assert.ThrowsAsync<ApiException>(() => service.GetVersion(package.Name, wrongVersion));
        }

        [Fact]
        public async Task GetVersion_False_Wrong_package()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            Package wrongPackage = LoopFaker.Package();

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(wrongPackage.Name)).ReturnsAsync(wrongPackage);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.GetVersion(wrongPackage.Name, version));
        }
        [Fact]
        public async Task GetLatestVersion_True()
        {
            Package package = LoopFaker.Package();
            package.Versions.RemoveAll(x => true);
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            string semVer2 = "0.2.0";
            (Common.Models.Version version2, bool _) = Utilities.GetSemVerFromString(semVer2);
            package.Versions.Add(version2);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            GetObjectResponse getObjectResponse = new GetObjectResponse()
            {
                BucketName = package.Id.ToString(),
                Key = Utilities.GetStringFromVersion(version)
            };

            s3ClientMock.Setup(x => x.GetObjectAsync(It.Is<GetObjectRequest>(x => x.Key == getObjectResponse.Key && x.BucketName == getObjectResponse.BucketName), default)).ReturnsAsync(getObjectResponse);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            GetObjectResponse objectResponse = await service.GetVersion(package.Name, version);

            Assert.Equal(getObjectResponse, objectResponse);
        }

        [Fact]
        public async Task GetLatestVersion_False_No_versions()
        {
            Package package = LoopFaker.Package();
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.RemoveAll(x => x.Deprecated == false && x.Deprecated == true);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.GetVersion(package.Name, version));
        }

        [Fact]
        public async void DeprecateVersion_True()
        {
            Package package = LoopFaker.Package();
            package.Versions.RemoveAll(x => true);
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            versionDataMock.Setup(x => x.UpdateVersion(It.Is<Common.Models.Version>(x => x.Id == version.Id)));

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            (Common.Models.Version versionIn, bool _) = Utilities.GetSemVerFromString(semVer);
            version= await service.DeprecateVersion(package.Name, versionIn);

            Assert.NotEqual(version.Deprecated, versionIn.Deprecated);
        }

        [Fact]
        public async void DeprecateVersion_Wrong_Version()
        {
            Package package = LoopFaker.Package();
            package.Versions.RemoveAll(x => true);
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            string wrongSemVer = "0.2.0";
            (Common.Models.Version wrongVersion, bool _) = Utilities.GetSemVerFromString(wrongSemVer);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.DeprecateVersion(package.Name, wrongVersion));
        }

        [Fact]
        public async void DeprecateVersion_Wrong_Package()
        {
            Package package = LoopFaker.Package();
            package.Versions.RemoveAll(x => true);
            string semVer = "0.1.0";
            (Common.Models.Version version, bool _) = Utilities.GetSemVerFromString(semVer);
            package.Versions.Add(version);

            Package wrongPackage = LoopFaker.Package();
            wrongPackage.Versions.RemoveAll(x => true);

            packageServiceMock.Setup(x => x.GetFullPackageByNameAsync(package.Name)).ReturnsAsync(package);

            VersionService service = new VersionService(s3ClientMock.Object, packageServiceMock.Object, versionDataMock.Object);

            await Assert.ThrowsAsync<ApiException>(() => service.DeprecateVersion(wrongPackage.Name, version));
        }
    }
}

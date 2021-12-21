using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
    public class VersionService : IVersionService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IPackageService _packageService;
        private readonly IVersionData _versionData;

        public VersionService(IAmazonS3 s3Client, IPackageService packageService, IVersionData versiondata)
        {
            _s3Client = s3Client;
            _packageService = packageService;
            _versionData = versiondata;
        }
        public async Task AddVersion(IFormFile file, Common.Models.Version version, string packageId)
        {
            if (file == null)
                throw new ApiException(400, new ErrorMessage("File null", "There was no file found"));

            //check if user has privileges but this feature is still missing from package

            if (file.ContentType != "application/zip" && file.ContentType != "application/x-tar" && file.ContentType != "application/x-zip-compressed")
                throw new ApiException(415, new ErrorMessage("Unsupported media type", "You can only use a .zip or .tar file"));

            Package package = await _packageService.GetFullPackageAsync(Guid.Parse(packageId));

            if (package == null)
                throw new ApiException(404, new ErrorMessage("package not found", "The package you were adding a version to was not found"));


            Common.Models.Version? previousVersion = package.GetLatestVersion();

            if (previousVersion != null && !Utilities.IsVersionGreater(version, previousVersion))
                throw new ApiException(400, new ErrorMessage("Version too low", "The new version must be higher than the last version"));
          
            await UploadFileToStorageAsync(file, Utilities.GetStringFromVersion(version), package);

            _versionData.AddVersion(version, package);
        }

        private async Task UploadFileToStorageAsync(IFormFile file, string SemVer, Package package)
        {
            if (!await _s3Client.DoesS3BucketExistAsync(package.Id.ToString()))
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = package.Id.ToString(),
                    UseClientRegion = true
                };
                await _s3Client.PutBucketAsync(putBucketRequest);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                TransferUtility fileTransfer = new TransferUtility(_s3Client);
                await fileTransfer.UploadAsync(stream, package.Id.ToString(), SemVer);
            }
        }
    }
}

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
        public async Task<Common.Models.Version> AddVersion(IFormFile file, Common.Models.Version version, string packageId)
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
            return version;
        }

        public async Task<GetObjectResponse> GetLatestVersion(string packageName)
        {
            Package package = await _packageService.GetFullPackageByNameAsync(packageName);

            if (package == null)
                throw new ApiException(404, new ErrorMessage("package not found", "The package you asked for doesn't exist"));
            
            
            Common.Models.Version? version = package.GetLatestVersion();

            if (version == null)
                throw new ApiException(404, new ErrorMessage("version not found", "This package does not contain this version"));

            GetObjectResponse objectResponse = await GetFileFromStorage(version, package);

            return objectResponse;
        }

        public async Task<GetObjectResponse> GetVersion(string packageName, Common.Models.Version semVer)
        {
            Package package = await _packageService.GetFullPackageByNameAsync(packageName);

            if (package == null)
                throw new ApiException(404, new ErrorMessage("package not found", "The package you asked for doesn't exist"));

            Common.Models.Version? version = package.GetVersion(semVer);
            if (version == null)
                throw new ApiException(404, new ErrorMessage("version not found", "This package does not contain this version"));

            GetObjectResponse objectResponse = await GetFileFromStorage(version, package);

            return objectResponse;
        }

        public async Task<Common.Models.Version> DeprecateVersion(string packageName, Common.Models.Version semVer)
        {
            Package package = await _packageService.GetFullPackageByNameAsync(packageName);

            if (package == null)
                throw new ApiException(404, new ErrorMessage("package not found", "The package you asked for doesn't exist"));

            Common.Models.Version? version = package.GetVersion(semVer);
            if (version == null)
                throw new ApiException(404, new ErrorMessage("version not found", "This package does not contain this version"));

            version.Deprecated = !version.Deprecated;

            _versionData.UpdateVersion(version);

            return version;
        }


        private async Task UploadFileToStorageAsync(IFormFile file, string SemVer, Package package)
        {
            try
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
                    TransferUtilityUploadRequest transferUtilityUploadRequest = new TransferUtilityUploadRequest()
                    {
                        BucketName = package.Id.ToString(),
                        Key = SemVer,
                        InputStream = stream,
                        ContentType = file.ContentType
                    };
                    await fileTransfer.UploadAsync(transferUtilityUploadRequest);
                }
            }
            catch (Exception e)
            {
                throw new ApiException(500, new ErrorMessage("Error uploading object to storage", e.Message));
            } 
        }

        private async Task<GetObjectResponse> GetFileFromStorage(Common.Models.Version version, Package package)
        {
            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = package.Id.ToString(),
                    Key = Utilities.GetStringFromVersion(version)
                };
                return await _s3Client.GetObjectAsync(getObjectRequest);
            }
            catch (Exception e)
            {
                throw new ApiException(500, new ErrorMessage("Error getting object from storage", e.Message));
            } 
        }
    }
}

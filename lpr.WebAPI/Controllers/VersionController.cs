using Amazon.S3;
using Amazon.S3.Model;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using lpr.Logic;
using lpr.Logic.Services;
using lpr.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly IVersionService _versionService;
        public VersionController(IAmazonS3 s3Client, IPackageData packagedata, IVersionData versionData, IAccountData accountdata)
        {
            _versionService = new VersionService(s3Client, new PackageService(packagedata, accountdata), versionData);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVersion([FromForm] NewVersion newVersion)
        {
            (Common.Models.Version version, bool success) = Utilities.GetSemVerFromString(newVersion.Version);
            if (!success)
                throw new ApiException(400, new ErrorMessage("", ""));
            Common.Models.Version v = await _versionService.AddVersion(newVersion.File, version, newVersion.PackageId);

            return Ok(v);
        }

        [HttpGet("/{package}/{version}")]
        public async Task<IActionResult> GetVersion(string package, string version)
        {
            if (version == null || version == "latest")
            {
                GetObjectResponse response = await _versionService.GetLatestVersion(package);
                return File(response.ResponseStream, response.Headers.ContentType, package);
            }

            (Common.Models.Version vers, bool success) = Utilities.GetSemVerFromString(version);
            if (!success)
                throw new ApiException(400, new ErrorMessage("", ""));

            GetObjectResponse res= await _versionService.GetVersion(package, vers);

            return File(res.ResponseStream, res.Headers.ContentType, package);
        }
        
        [HttpPut("/{package}/{version}")]
        public async Task<IActionResult> DeprecateVersion(string package, string version)
        {

            (Common.Models.Version ver, bool success) = Utilities.GetSemVerFromString(version);
            if (!success)
                throw new ApiException(400, new ErrorMessage("", ""));

            Common.Models.Version v = await _versionService.DeprecateVersion(package, ver);
            return Ok(v);
        }
    }
}

using Amazon.S3;
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
            await _versionService.AddVersion(newVersion.File, version, newVersion.PackageId);

            return Ok(version);
        }
    }
}

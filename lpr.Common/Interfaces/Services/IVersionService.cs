using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Services
{
    public interface IVersionService
    {
        Task<Models.Version> AddVersion(IFormFile file, Models.Version version, string packageId);
        Task<GetObjectResponse> GetLatestVersion(string packageName);
        Task<GetObjectResponse> GetVersion(string packageName, Models.Version version);
        Task<Models.Version> DeprecateVersion(string packageName, Models.Version version);
    }
}

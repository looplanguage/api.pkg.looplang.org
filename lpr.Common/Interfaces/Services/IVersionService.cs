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
        Task CreateVersion(IFormFile file, string SemVer, string packageId);
    }
}

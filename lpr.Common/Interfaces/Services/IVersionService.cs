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
        Task AddVersion(IFormFile file, Models.Version version, string packageId);
    }
}

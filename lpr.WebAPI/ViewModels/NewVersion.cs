using System.Diagnostics.CodeAnalysis;

namespace lpr.WebAPI.ViewModels
{
    public class NewVersion
    {
        [NotNull]
        public IFormFile? File { get; set; }
        public string Version { get; set; } = "";
        public string PackageId { get; set; } = "";
    }
}

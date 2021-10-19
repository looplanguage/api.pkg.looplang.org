using System.ComponentModel.DataAnnotations;

namespace lpr.Common.Dtos.In
{
    public class VersionDtoIn
    {
        [Required]
        public int Major { get; set; }

        [Required]
        public int Minor { get; set; }

        [Required]
        public int Patch { get; set; }

        public string? Documentation { get; set; }

        [Required]
        public string File { get; set; }
    }
}

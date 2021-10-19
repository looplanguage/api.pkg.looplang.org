using System.ComponentModel.DataAnnotations;

namespace lpr.Common.Dtos.In {
  public class PackageDtoIn {
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public VersionDtoIn InitialVersion { get; set; }

    public string? Documentation { get; set; }
  }
}

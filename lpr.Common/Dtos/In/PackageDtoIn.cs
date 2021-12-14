using System.ComponentModel.DataAnnotations;

namespace lpr.Common.Dtos.In {
  public class PackageDtoIn
  {
    //TODO: Do we want to parse data from the ZIP/TAR package file itself?
    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;
    public string? Documentation { get; set; }
  }
}

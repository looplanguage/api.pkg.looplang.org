using lpr.Common.Dtos.In;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models {
  public class Version {
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required] public int Major { get; set; }

    [Required] public int Minor { get; set; }

    [Required] public int Patch { get; set; }

    public string? Documentation { get; set; }

    [Required] public string File { get; set; }

    public DateTime Created { get; set; }

    public bool Archived { get; set; }

    public List<Account> Participants { get; set; }

    public int Downloads { get; set; }

    public Version() : base() {}
    public Version(VersionDtoIn dto) {
      Major = dto.Major;
      Minor = dto.Minor;
      Patch = dto.Patch;
      Documentation = dto.Documentation;
      File = dto.File;
    }
  }
}

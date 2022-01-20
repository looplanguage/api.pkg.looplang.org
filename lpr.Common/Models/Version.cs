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

    public DateTime Created { get; set; }

    public bool Deprecated { get; set; }
    public int Downloads { get; set; }

    public Version(int major, int minor, int patch)
    {
        Id = Guid.NewGuid();
        Major = major;
        Minor = minor;
        Patch = patch;
        Created = DateTime.UtcNow;
    }

    public Version() { }
  }
}

using lpr.Common.Dtos.In;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models {
    public class Package {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required] [MaxLength(100)] [Column(
            TypeName = "varchar(100)")] // Prevents international characters
        public string Name {
        get; set;
        } = string.Empty;

        [NotMapped] public int? Downloads {
            get {
            if (Versions != null)
                return Versions.Select(v => v.Downloads).Sum();
            return null;
            }
        }

        public List<Version> Versions { get; set; }

        public string? Documentation { get; set; }

        public DateTime Created { get; set; }

        public bool Archived { get; set; }
        public Organisation? Organisation { get; set; }

        public Package() : base() 
        {
            Versions = new List<Version>();
        }
        public Package(PackageDtoIn dto) {
            Name = dto.Name;
            List<Version> versions = new() { };
            Versions = versions;
            Documentation = dto.Documentation;
            Archived = false;
            Created = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        public Version? GetLatestVersion()
        {
            return Versions.OrderByDescending(x => x.Major).ThenByDescending(x => x.Minor).ThenByDescending(x => x.Patch).FirstOrDefault();
        }

        public Version? GetVersion(Version v)
        {
            return Versions.Where(x => x.Major == v.Major && x.Minor == v.Minor && x.Patch == v.Patch).FirstOrDefault();
        }

    }
}

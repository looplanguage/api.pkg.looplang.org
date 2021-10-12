using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class Organisation
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public List<Package> Packages { get; set; }
        public string Documentation { get; set; }
        public DateTime Created { get; set; }
    }
}

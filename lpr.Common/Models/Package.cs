using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class Package
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Version> Versions { get; set; }
        public string? Documentation { get; set; }
        public DateTime Created {  get; set; }
    }
}

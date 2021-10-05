using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Package> Packages { get; set; }
    }
}

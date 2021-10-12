using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class Version
    {
        public Guid Id {  get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public string? Documentation { get; set; }
        public string File { get; set; }
        public DateTime Created { get; set; }
        public List<Account> Participants { get; set; }
        public int Downloads { get; set; }
    }
}

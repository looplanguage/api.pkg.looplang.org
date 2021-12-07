using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class Participant
    {
        public Guid Id {  get; set; }
        public Organisation? Organisation { get; set; }
        public Package? Package {  get; set; }
        public DateTime Created {  get; set; }
    }
}

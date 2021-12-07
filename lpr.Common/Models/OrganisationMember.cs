using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class OrganisationMember
    {
        public Guid Id {  get; set; }
        public Organisation? Organisation { get; set; }
        public Account? Account { get; set; }
        public DateTime Created {  get; set; }
    }
}

using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Data
{
    public interface IOrganisationData
    {
        void AddOrganisation(Organisation org);
        Organisation GetOrganisationById(string id);
    }
}

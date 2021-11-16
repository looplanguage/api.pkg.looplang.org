using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Data {
  public interface IOrganisationData {
    Organisation AddOrganisation(Organisation org);
    Task<List<Organisation>>
    GetOrganisationsPaginatedAsync(int amount, Guid? fromOrganisationId);
    Organisation GetOrganisationById(Guid id);
  }
}

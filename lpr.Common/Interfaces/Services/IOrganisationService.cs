using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Services {
  public interface IOrganisationService {

    Organisation AddOrganisation(string Name, string UserId);

    List<Organisation> GetOrganisationsPaginatedAsync(int amount,
                                                      Guid? fromOrganisationId);

    Organisation GetOrganisation(string OrgId);
  }
}

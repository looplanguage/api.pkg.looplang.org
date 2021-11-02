using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Services
{
public interface IOrganisationService
{
    int AddOrganisation(string Name, string UserId);
    Task<List<Organisation>> GetOrganisationsPaginatedAsync(int page, int amount);
    Organisation GetOrganisation(string OrgId);
}
}

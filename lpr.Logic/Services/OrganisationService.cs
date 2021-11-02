using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
public class OrganisationService: IOrganisationService
{
    private readonly IOrganisationData _organisationData;

    public OrganisationService(ILprDbContext ctx)
    {
        _organisationData = new OrganisationData(ctx);
    }

    public int AddOrganisation(string Name, string UserId)
    {
        //TODO check if user exists

        Organisation org = new Organisation(Name);
        _organisationData.AddOrganisation(org);
        return 200;
    }

    public async Task<List<Organisation>> GetOrganisationsPaginatedAsync(int page, int amount)
    {
        return await _organisationData.GetOrganisationsPaginatedAsync(page, amount);
    }

    public Organisation GetOrganisation(string OrgId)
    {
        return _organisationData.GetOrganisationById(OrgId);
    }
}
}

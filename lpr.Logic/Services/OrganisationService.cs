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

namespace lpr.Logic.Services {
public class OrganisationService : IOrganisationService {
    private readonly IOrganisationData _organisationData;

    public OrganisationService(ILprDbContext ctx) {
        _organisationData = new OrganisationData(ctx);
    }

    public void AddOrganisation(string Name, string UserId) {
        // TODO check if user exists
        /*
            if(user == null)
                throw new ApiException(401, new ErrorMessage(
            "User not Found"
            "The user is not found in the database make sure you have the right
           ID"
            ))
         */
        Organisation org = new Organisation(Name);
        _organisationData.AddOrganisation(org);
    }

    public Organisation GetOrganisation(string OrgId) {
        Organisation org = _organisationData.GetOrganisationById(OrgId);

        if (org == null)
            throw new ApiException(
                500,
                new ErrorMessage(
                    "Organisation not Found",
                    "The organisation was not found in the database make sure you have the right ID"));

        return org;
    }
}
}

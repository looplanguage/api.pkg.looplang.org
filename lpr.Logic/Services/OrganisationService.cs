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
    public OrganisationService(IOrganisationData organisationData) {
      _organisationData = organisationData;
    }

    public Organisation AddOrganisation(Organisation org) {
            // TODO check if user exists
            /*
                if(user == null)
                    throw new ApiException(401, new ErrorMessage(
                "User not Found"
                "The user is not found in the database make sure you have the right
               ID"
                ))
             */
            if(String.IsNullOrEmpty(org.Name) || org.Name.Length < 5)
                throw new ApiException(400, new ErrorMessage("Organisation name not allowed", "The organisaton name must be longer than 4 charachters"));

            if (!Utilities.ValidateName(org.Name))
                throw new ApiException(400, new ErrorMessage("Organisation name not allowed", "The organisaton name cannot contain any numbers or special characters"));

      return _organisationData.AddOrganisation(org);
    }

    public Organisation? GetOrganisation(Guid OrgId) {

      return _organisationData.GetOrganisationById(OrgId);
    }

    public async Task<List<Organisation>> GetOrganisationsPaginatedAsync(int amount, Guid? lastOrganisationId) {
      return await _organisationData.GetOrganisationsPaginatedAsync(
          amount, lastOrganisationId);
    }
  }
}

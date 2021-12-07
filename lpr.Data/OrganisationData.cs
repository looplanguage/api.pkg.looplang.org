using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Data {
  public class OrganisationData : IOrganisationData {
    private readonly ILprDbContext _dbContext;
    public Organisation AddOrganisation(Organisation org) {
      _dbContext.Add(org);
      _dbContext.SaveChanges();
      return org;
    }

    public Organisation? GetOrganisationById(Guid id) {
      return _dbContext?.Organisation?.FirstOrDefault(x => x.Id.ToString() ==
                                                         id.ToString());
    }

    public async Task<List<Organisation>>
    GetOrganisationsPaginatedAsync(int amount, Guid? fromOrganisationId) {

      if (fromOrganisationId == null) {
        return await _dbContext.Organisation.OrderBy(o => o.Id)
            .Take(amount)
            .ToListAsync();
      } else {
        return await _dbContext.Organisation.OrderBy(o => o.Id)
            .Where(o => o.Id.CompareTo((Guid)fromOrganisationId) > 0)
            .Take(amount)
            .ToListAsync();
      }
    }

    public OrganisationData(ILprDbContext db) { _dbContext = db; }
  }
}

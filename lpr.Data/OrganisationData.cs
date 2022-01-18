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
    public OrganisationData(ILprDbContext db) { _dbContext = db; }
    public Organisation AddOrganisation(Organisation org)
    {
      _dbContext.Add(org);
      _dbContext.SaveChanges();

      return org;
    }

    public Organisation? GetOrganisationById(Guid id) {
      return _dbContext?.Organisation?.FirstOrDefault(x => x.Id.ToString() ==
                                                         id.ToString());
    }

    public async Task<List<Organisation>> GetOrganisationsPaginatedAsync(int amount, Guid? fromOrganisationId) {

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

    public async Task<List<Organisation>> GetOrganisationsFromAccountAsync(Guid accountId)
    {
      List<OrganisationMember> members = await _dbContext.OrganisationMember.Where(a => a.Id == accountId).ToListAsync();

      List<Organisation?> organisations = members.Select(m => m.Organisation).ToList();
      List<Organisation> output = organisations.Where(p => p != null).Select(x => x).Cast<Organisation>().ToList();

      return output;
    }

  }
}

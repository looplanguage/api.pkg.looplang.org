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

namespace lpr.Data
{
public class OrganisationData : IOrganisationData
{
    private readonly ILprDbContext _DbContext;
    public void AddOrganisation(Organisation org)
    {
        _DbContext.Add(org);
        _DbContext.SaveChanges();
    }

    public Organisation GetOrganisationById(string id)
    {
        return _DbContext.Organisation.FirstOrDefault(x => x.Id.ToString() == id);
    }

    public async Task<List<Organisation>> GetOrganisationsPaginatedAsync(int page, int amount)
    {
        return await _DbContext.Organisation.Skip(page * amount).Take(amount).ToListAsync();
    }

    public OrganisationData(ILprDbContext _db)
    {
        _DbContext = _db;
    }
}
}

using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Models;
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

        public OrganisationData(ILprDbContext _db)
        {
            _DbContext = _db;
        }
    }
}

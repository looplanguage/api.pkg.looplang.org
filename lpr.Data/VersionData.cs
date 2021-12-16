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
    public class VersionData : IVersionData
    {
        private readonly ILprDbContext _ctx;
        public VersionData(ILprDbContext ctx) { _ctx = ctx; }
        public void AddVersion(Common.Models.Version version, Package package)
        {
            package.Versions.Add(version);
            _ctx.Version.Add(version);
            _ctx.SaveChanges();
        }
    }
}

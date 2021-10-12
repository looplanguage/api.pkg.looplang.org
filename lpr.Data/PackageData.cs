using lpr.Common.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Data
{
    public class PackageData
    {
        private readonly ILprDbContext _ctx;

        public PackageData(ILprDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}

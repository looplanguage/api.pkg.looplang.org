using lpr.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Data
{
    public interface IVersionData
    {
        void AddVersion(Models.Version version, Package package);
    }
}

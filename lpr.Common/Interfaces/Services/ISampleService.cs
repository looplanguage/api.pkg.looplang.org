using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Services
{
    public interface ISampleService
    {
        public Task<int> getUserById(int id);
    }
}

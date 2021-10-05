using lpr.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
    public class SampleService: ISampleService
    {
        private readonly ISampleContext _ctx;

        public SampleService(ISampleContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> getUserById(int id)
        {
            return 69 + id;
        }
    }
}

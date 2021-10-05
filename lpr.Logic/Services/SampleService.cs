using lpr.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
    public class SampleService
    {
        private readonly ISampleContext _ctx;

        public SampleService(ISampleContext ctx)
        {
            _ctx = ctx;
        }
    }
}

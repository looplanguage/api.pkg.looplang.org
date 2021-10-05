﻿using lpr.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Data.Contexts
{
    public class SampleContext : DbContext, ISampleContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options) { }
    }
}

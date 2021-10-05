using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces
{
    public interface ISampleContext : IDbContext, IDisposable
    {
        public DbSet<Account> Account {  get; set; }
    }
}

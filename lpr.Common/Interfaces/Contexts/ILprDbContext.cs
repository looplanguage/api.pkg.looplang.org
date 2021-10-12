using lpr.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Interfaces.Contexts
{
    public interface ILprDbContext : IDbContext, IDisposable
    {
        public DbSet<Account> Account {  get; set; }
        public DbSet<Organisation> Organisation { get; set; }
    }
}

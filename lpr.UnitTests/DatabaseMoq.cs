
using System;
using System.Threading.Tasks;
using lpr.Common.Interfaces.Contexts;
using lpr.Data.Contexts;
using Microsoft.EntityFrameworkCore;

class DatabaseMoq
{
    public static ILprDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<LprContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        var databaseContext = new LprContext(options);
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}
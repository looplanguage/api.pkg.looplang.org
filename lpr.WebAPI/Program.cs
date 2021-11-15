using System;
using lpr.WebAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

string? connectionString =
    Environment.GetEnvironmentVariable("MariaDB_ConnectionString");
if (connectionString == null) {
    connectionString = "Server=localhost;Database=LPR;User=root;Password=root";
}

Application app = new Application(args);

app.AddDatabaseConnection(connectionString);
app.Run();

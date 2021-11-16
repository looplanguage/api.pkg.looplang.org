using System;
using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Data;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using lpr.WebAPI.Middleware;
using lpr.WebAPI.Services;
using lpr.WebAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ILprDbContext, LprContext>(options => {
  string? connectionString =
      Environment.GetEnvironmentVariable("MariaDB_ConnectionString");
  if (connectionString == null) {
    connectionString = "Server=localhost;Database=LPR;User=root;Password=root";
  }
  options.UseMySql(connectionString,
                   new MariaDbServerVersion(new Version(10, 5, 9)));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IPackageData, PackageData>();

//TODO: move to logic layer or keep it at webAPI?
builder.Services.AddScoped<IJWTService, JWTService>(_ => new JWTService("test"));

using (var scope = builder.Services.BuildServiceProvider().CreateScope()) {
    using (var context = scope.ServiceProvider.GetService<ILprDbContext>()) {
        context.Database.EnsureCreated();
    }
}

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title = "lpr.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(); app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "lpr.WebAPI v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();// Is this used?

app.MapControllers();

app.UseMiddleware<JWTMiddleware>();
app.UseMiddleware<GlobalErrorHandler>();

app.Run();

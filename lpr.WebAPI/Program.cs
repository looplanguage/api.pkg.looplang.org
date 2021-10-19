using System;
using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.Services.AddScoped<ISampleService, SampleService>();

builder.Services.AddDbContext<ILprDbContext, LprContext>(options =>
{
    options.UseMySql("Server=db;Database=LPR;User=root;Password=root;Port=6603", 
        new MariaDbServerVersion(new Version(10, 5, 9)));
});

//builder.Services.AddDbContext<ILprDbContext, new LprContext()>()


using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    using (var context = scope.ServiceProvider.GetService<ILprDbContext>())
    {
        context.Database.EnsureCreated();
    }
}



    builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "lpr.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "lpr.WebAPI v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

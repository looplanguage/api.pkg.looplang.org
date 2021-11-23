using System;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Data;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using lpr.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace lpr.WebAPI {
  public class Application {
    public WebApplicationBuilder builder;
    public Application(string[] args) {
        builder = WebApplication.CreateBuilder(args);
    }

    public void AddDatabaseConnection(string connectionString) {
        builder.Services.AddDbContext<ILprDbContext, LprContext>(options => {
          options.UseMySql(connectionString,
                          new MariaDbServerVersion(new Version(10, 5, 9)));
        });

        builder.Services.AddScoped<IPackageService, PackageService>();
        builder.Services.AddScoped<IPackageData, PackageData>();

        using (var scope =
                    builder.Services.BuildServiceProvider().CreateScope()) {
          using (var context =
                      scope.ServiceProvider.GetService<ILprDbContext>()) {
            context.Database.EnsureCreated();
          }
        } builder.Services.AddControllers();
    }

    public void AddGitHubOauth(string clientId, string clientSecret)
    {
        builder.Services.AddScoped<IAuthService, AuthService>(x =>
            new AuthService(clientId,clientSecret,x.GetRequiredService<IJWTService>())
        );
    }

    public void AddJwtService(string jwtTokenSecret)
    {
        //TODO: move to logic layer or keep it at webAPI?
        builder.Services.AddScoped<IJWTService, JWTService>(_ =>
            new JWTService(jwtTokenSecret)
        );
    }

    public void AddSwagger() {
        builder.Services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new() { Title = "lpr.WebAPI", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "X-JWT-Token",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
          });
        });
    }

    protected WebApplication Build() {
        WebApplication app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c => {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "lpr.WebAPI v1");
          c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<GlobalErrorHandler>();
        app.UseMiddleware<JWTMiddleware>();

        return app;
    }

    public void Run() { this.Build().Run(); }
  }

}

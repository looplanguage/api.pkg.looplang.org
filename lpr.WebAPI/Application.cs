using System;
using Amazon.S3;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Data;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using lpr.WebAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        builder.Services.AddScoped<IVersionData, VersionData>();

        using (var scope =
                    builder.Services.BuildServiceProvider().CreateScope()) {
          using (var context =
                      scope.ServiceProvider.GetService<ILprDbContext>()) {
              if (context != null)
              {
                  if (context.Database.EnsureCreated())
                  {
                      SeederService seederService = new SeederService(context);
                      seederService.Seed();
                  }
              }
          }
        } builder.Services.AddControllers();
    }

    public void AddGitHubOauth(string? clientId, string? clientSecret)
    {
        builder.Services.AddScoped<IAccountData, AccountData>();
        builder.Services.AddScoped<IAuthService, AuthService>(x =>
            new AuthService(clientId,clientSecret,x.GetRequiredService<IJWTService>(),x.GetRequiredService<IAccountData>())
        );
        builder.Services.AddScoped<IGitHubService, GitHubService>(x =>
            new GitHubService(clientId,clientSecret,x.GetRequiredService<IAccountData>())
        );
    }

    public void AddObjectStorage(string? accesskey,string? privatekey, string? serviceUrl)
    {
        if (accesskey == null || privatekey == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Warning: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Could not configure Objectstorage Client, Some features may crash the server");
            return;
        }

        AmazonS3Config config = new AmazonS3Config()
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true
        };

        builder.Services.AddScoped<IAmazonS3>(s => new AmazonS3Client(accesskey, privatekey, config));
    }

    public void AddJwtService(string? jwtTokenSecret)
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

    public void ConfigureCORS()
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "Localhost",
                builder =>
                {
                    builder.WithOrigins(
                        "http://localhost:8080",
                        "https://localhost:8080"
                        );
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

        app.UseCors("Localhost");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

            app.UseMiddleware<JWTMiddleware>();
            app.UseMiddleware<GlobalErrorHandler>();


        return app;
    }

    public void Run() { this.Build().Run(); }
  }

}

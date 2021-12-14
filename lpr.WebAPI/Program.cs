using System;
using System.Linq;
using lpr.WebAPI;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

string? connectionString =
    Environment.GetEnvironmentVariable("MariaDB_ConnectionString");
if (connectionString == null)
  connectionString = "Server=localhost;Database=LPR;User=root;Password=root;";

string? githubClientId = Environment.GetEnvironmentVariable("lpr_github_clientid");
string? githubClientSecret = Environment.GetEnvironmentVariable("lpr_github_clientsecret");

string? jwtTokenSecret = Environment.GetEnvironmentVariable("lpr_token_secret");
string? s3accesskey = Environment.GetEnvironmentVariable("S3_AccessKey");
string? s3privatekey = Environment.GetEnvironmentVariable("S3_PrivateKey");
var app = new Application(args);

app.ConfigureCORS();
app.AddDatabaseConnection(connectionString);
app.AddGitHubOauth(githubClientId, githubClientSecret);
app.AddJwtService(jwtTokenSecret);
app.AddSwagger();
app.AddObjectStorage(s3accesskey, s3privatekey);

app.Run();

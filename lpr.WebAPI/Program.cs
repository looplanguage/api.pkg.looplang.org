using lpr.WebAPI;

var builder = WebApplication.CreateBuilder(args);

string? connectionString =
    Environment.GetEnvironmentVariable("MariaDB_ConnectionString");
if (connectionString == null)
  connectionString = "Server=localhost;Database=LPR;User=root;Password=root;";

string? githubClientId = Environment.GetEnvironmentVariable("lpr_github_clientid");
string? githubClientSecret = Environment.GetEnvironmentVariable("lpr_github_clientsecret");

string? jwtTokenSecret = Environment.GetEnvironmentVariable("lpr_token_secret");    

string?[] secrets = {connectionString,githubClientId,githubClientSecret,jwtTokenSecret};

if(secrets.Any(s => s == null))
    throw new Exception("Some configuration is null.");

var app = new Application(args);

app.ConfigureCORS();
app.AddDatabaseConnection(connectionString);
app.AddGitHubOauth(githubClientId, githubClientSecret);
app.AddJwtService(jwtTokenSecret);
app.AddSwagger();

app.Run();

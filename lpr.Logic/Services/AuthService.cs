using lpr.Common.Interfaces.Services;
using lpr.Common.Interfaces.Data;
using System.Net.Http.Json;
using lpr.Common.Models;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using lpr.Common.Dtos.In;

namespace lpr.Logic.Services
{
    public class AuthService: IAuthService
    {
        private readonly string? _clientId;
        private readonly string? _clientSecret;
        private readonly IJWTService _jwtSrv;
        private readonly IAccountData _accountData;

        public AuthService(string? clientId, string? clientSecret, IJWTService jwtService, IAccountData accountData)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _jwtSrv = jwtService;
            _accountData = accountData;
        }

        public async Task<Account> GetAccountById(Guid accountId)
        {
            return await _accountData.GetAccountById(accountId);
        }

        public async Task<Account> UpdateAccount(UpdateAccountDtoIn account, Guid accountId)
        {
            Account currentAccount = await GetAccountById(accountId);

            currentAccount.Name = account.Name ?? currentAccount.Name;
            currentAccount.Logo = account.Logo ?? currentAccount.Logo;

            await _accountData.UpdateAccount(currentAccount);
            return currentAccount;
        }

        public async Task<string> ValidateGitHubAccessToken(string authKey)
        {
            if((new List<string?>{_clientId,_clientSecret}).Any(id => id == null))
                throw new Exception("This method cannot be used because it's missing GitHub related secret values.");

            var body = JsonContent.Create(new {
                client_id = _clientId,
                client_secret = _clientSecret,
                code = authKey
            });

            //https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://github.com");//TODO: don't hard-code

            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("LPR", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var responseRaw = await client.PostAsJsonAsync("/login/oauth/access_token", body.Value);
            string response = await responseRaw.Content.ReadAsStringAsync();
            var githubAuth = JObject.Parse(response);

            if (githubAuth.ContainsKey("access_token"))
            {
                string accessToken = githubAuth.Property("access_token").Value.ToString();
                string jwt = GenerateJWT(new Claim(ClaimTypes.Authentication, accessToken));
                //string jwt = _jwtSrv.GenerateToken(model);
                return jwt;
            }
            else
                throw new Exception("Not a valid authKey has been given, is used or expired.");
        }

        public string GenerateJWT(Claim claim)//TODO: this may be temporarily.
        {
          Claim[] claims = new Claim[] { claim };
          JWTContainerModel model = new JWTContainerModel()
          {
              Claims = claims
          };
          return _jwtSrv.GenerateToken(model);
        }
  }
}

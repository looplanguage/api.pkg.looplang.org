using lpr.Common.Interfaces.Services;
using lpr.Common.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using lpr.Common.Models;
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace lpr.Logic.Services
{
    public class AuthService: IAuthService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IJWTService _jwtSrv;
        private readonly IAccountData _accountData;//TODO: yet unused!!!!!

        public AuthService(string clientId, string clientSecret, IJWTService jwtService, IAccountData accountData)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _jwtSrv = jwtService;
            _accountData = accountData;
        }

        public async Task<string> ValidateGitHubAccessToken(string authKey)
        {
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

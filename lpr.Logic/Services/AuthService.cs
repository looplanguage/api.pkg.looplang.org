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
        private readonly IJWTService _jwtSrv;
        //private readonly IAuthData _data;
        public AuthService(IJWTService jwtService)
        {
            _jwtSrv = jwtService;
        }

        public const string clientId = "9f93c7a725e27ee50ed3";
        public const string clientSecret = "<<secret>>";

        public async Task<string> ValidateGitHubAccessToken(string authKey)
        {
            var body = JsonContent.Create(new {
                client_id = clientId,
                client_secret = clientSecret,
                code = authKey
            });

            //https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://github.com");

            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("LPR", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var responseRaw = await client.PostAsJsonAsync("/login/oauth/access_token", body.Value);
            string response = await responseRaw.Content.ReadAsStringAsync();
            var githubAuth = JObject.Parse(response);

            if (githubAuth.ContainsKey("access_token"))
            {
                Claim[] claims = new Claim[] { new Claim(ClaimTypes.Authentication, githubAuth.Property("access_token").Value.ToString()) };
                JWTContainerModel model = new JWTContainerModel()
                {
                    Claims = claims
                };
                string token = _jwtSrv.GenerateToken(model);
                return token;
            }
            else
                throw new Exception("Not a valid authKey has been given, is used or expired.");
        }
    }
}

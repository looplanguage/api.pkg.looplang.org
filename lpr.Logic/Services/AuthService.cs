using lpr.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
    public class AuthService: IAuthService
    {
        //private readonly IAuthData _data;
        //public AuthService(IAuthData data) { _data = data; }

        public const string clientId = "9f93c7a725e27ee50ed3";
        public const string clientSecret = "<<<SECRET>>>";

        public async Task<string> ValidateGitHubAccessToken(string authKey)
        {
            var bodyRaw = new {
                client_id = clientId,
                client_secret = clientSecret,
                code = authKey
            };

            var body = JsonContent.Create(bodyRaw);

            //https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://github.com");

            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("LPR", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("/login/oauth/access_token", body.Value);
            string githubAuth = await response.Content.ReadAsStringAsync();
            if (githubAuth.Contains("access_token"))
                return githubAuth;
            else
                throw new Exception('Not a valid authKey has been given, is used or expired.');
        }
    }
}

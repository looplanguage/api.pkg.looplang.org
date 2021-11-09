using lpr.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Logic.Services
{
    public class AuthService: IAuthService
    {
        //private readonly IAuthData _data;
        //public AuthService(IAuthData data) { _data = data; }

        public async Task<string> ValidateGitHubAccessToken(string token)
        {
            //https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://github.com/login/oauth/access_token");

            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("LPR", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

            var response = await client.PostAsync("/", null);
            return response.ToString();
            //return await _data.ValidateGitHubAccessToken(page, amount);
        }
    }
}

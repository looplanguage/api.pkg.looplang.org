using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {

        private readonly IAuthService _srv;
        private readonly IGitHubService _githubSrv;
        public AuthController(IAuthService authService, IGitHubService githubService)
        {
            _srv = authService;
            _githubSrv = githubService;
        }

        [HttpPost("/LoginOrRegister/GitHub/{authenticationKey}")]
        [ProducesResponseType(StatusCodes.Status200OK)]//For Login
        [ProducesResponseType(StatusCodes.Status201Created)]//For Register
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginOrRegisterGitHub(string authenticationKey)
        {
            string token = await _srv.ValidateGitHubAccessToken(authenticationKey);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            string accessToken = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Authentication).Value;

            GithubUser githubUser = await _githubSrv.GetGithubUser(accessToken);//TODO change to user data

            Account account = await _githubSrv.GetRegisteredUser(githubUser.Id);
            
            if(account != null)//Login:
            {
                string JsonOutput =  JsonSerializer.Serialize(_srv.GenerateJWT(new Claim(ClaimTypes.Authentication, account.Id.ToString()))); 
                return StatusCode(200, JsonOutput);
            }
            else//Register
            {
                account = await _githubSrv.Register(githubUser);
                string JsonOutput =  JsonSerializer.Serialize(_srv.GenerateJWT(new Claim(ClaimTypes.Authentication, account.Id.ToString()))); 
                return StatusCode(201, JsonOutput);
            }
        }
    }
}

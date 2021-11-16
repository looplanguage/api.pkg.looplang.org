using System.Threading.Tasks;
using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using lpr.WebAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {

        private readonly IAuthService _srv;
        public AuthController(IAuthService authService)
        {
            _srv = authService;
        }

        [HttpGet("/Login/GitHub/{authenticationKey}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ValidateGitHubAccessToken(string authenticationKey) {

            string output = await _srv.ValidateGitHubAccessToken(authenticationKey);
            return StatusCode(200, output);
        }
    }
}

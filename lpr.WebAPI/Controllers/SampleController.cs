using lpr.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _srv;
        public SampleController(ISampleService srv)
        {
            _srv = srv;
        }

        /// <summary>
        ///     Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the searched user.</param>  
        /// <response code="200">Returns the found user.</response>
        /// <response code="500">A Server error has occured.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getUserById(int id)
        {
            return StatusCode(200, id);
        }
    }
}

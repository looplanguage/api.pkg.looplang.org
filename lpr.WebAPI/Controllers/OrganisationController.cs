using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using lpr.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganisationController : ControllerBase
    {

        private readonly IOrganisationService _organisationService;
        public OrganisationController(ILprDbContext dbContext)
        {
            _organisationService = new OrganisationService(dbContext);
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
        public async Task<IActionResult> getOrganisation(string id)
        {
            Organisation org = _organisationService.GetOrganisation(id);

            if (org == null)
                return StatusCode(500, org);

            return StatusCode(200, org);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> createOrganisation(NewOrganisation newOrganisation)
        {
            int res = _organisationService.AddOrganisation(newOrganisation.Name, newOrganisation.User);
            return StatusCode(res);
        }

        //Source downloading files:
        //https://codeburst.io/download-files-using-web-api-ae1d1025f0a9
    }
}

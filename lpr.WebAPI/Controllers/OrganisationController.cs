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
  public class OrganisationController : ControllerBase {

    private readonly IOrganisationService _organisationService;
    public OrganisationController(ILprDbContext dbContext) {
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
    public async Task<IActionResult> getOrganisation(string id) {
      try {
        Organisation org = _organisationService.GetOrganisation(id);

        return StatusCode(200, org);
      } catch (ApiException ex) {
        return StatusCode(ex.ErrorCode, ex.ErrorMessage);
      }
    }

    /// <summary>
    ///     Creates a new Organisation with a Name and a User
    /// </summary>
    /// <param name="newOrganisation"></param>
    /// <response code="200">Returns Ok</response>
    /// <response code="401">The User is not authorised</response>
    /// <response code="500">A Server error has occured.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult>
    createOrganisation(NewOrganisation Organisation) {
      try {
        int res = _organisationService.AddOrganisation(Organisation.Name,
                                                       Organisation.User);
        return StatusCode(res);
      } catch (ApiException ex) {
        return StatusCode(ex.ErrorCode, ex.ErrorMessage);
      }
    }

    // Source downloading files:
    // https://codeburst.io/download-files-using-web-api-ae1d1025f0a9
  }
}

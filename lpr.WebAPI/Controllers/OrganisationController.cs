using System;
using System.Collections.Generic;
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

      Organisation org = _organisationService.GetOrganisation(id);
      return StatusCode(200, org);
    }

    /// <summary>
    ///     Creates a new Organisation with a Name and a User
    /// </summary>
    /// <param name="newOrganisation"></param>
    /// <response code="200">Returns Ok</response>
    /// <response code="401">The User is not authorised</response>
    /// <response code="500">A Server error has occured.</response>

    [HttpGet("GetOrganisationsPaginated/{amount}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult>
    GetOrganisationsPaginated(Guid? fromOrganisationId = null,
                              int amount = 25) {
      try {
        List<Organisation> organisations = await
            _organisationService.GetOrganisationsPaginatedAsync(
                amount, fromOrganisationId);

        return StatusCode(200, organisations);
      } catch {
        return StatusCode(500);
      }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult>
    createOrganisation(NewOrganisation Organisation) {

      _organisationService.AddOrganisation(Organisation.Name,
                                           Organisation.User);
      return StatusCode(200);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lpr.Common.Interfaces;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using lpr.WebAPI.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using lpr.Common.Dtos.In;

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
    [Authenticated]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetOrganisation(string id)
    {
      Organisation? org = _organisationService.GetOrganisation(Guid.Parse(id));
      return StatusCode(200, org);
    }

    /// <summary>
    ///     Creates a new Organisation with a Name and a User
    /// </summary>
    /// <param name="fromOrganisationId"></param>
    /// <param name="amount"></param>
    /// <response code="200">Returns Ok</response>
    /// <response code="401">The User is not authorised</response>
    /// <response code="500">A Server error has occured.</response>

    [HttpGet("GetOrganisationsPaginated/{amount}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrganisationsPaginated(Guid? fromOrganisationId = null, int amount = 25)
    {
      List<Organisation> organisations = await _organisationService.GetOrganisationsPaginatedAsync(amount, fromOrganisationId);

      return StatusCode(200, organisations);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateOrganisation(string organisationName)
    {
      //JWT is checked in the JWTMiddleware so this will always contain a token.
      Request.Headers.TryGetValue("X-JWT-Token", out var token);
      var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

      Organisation org = new Organisation();
      org.Name = organisationName;
      _organisationService.AddOrganisation(org);

      return StatusCode(200);
    }
  }
}

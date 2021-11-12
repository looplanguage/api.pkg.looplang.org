using System;
using lpr.Common.Dtos.In;
using lpr.Common.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using lpr.Common.Interfaces;
using lpr.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class PackageController : ControllerBase {
    private readonly IPackageService _srv;
    public PackageController(IPackageService srv) { _srv = srv; }

    [HttpGet("GetPackagesPaginated/{page}/{amount}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPackagesPaginated(int page,
                                                          int amount) {
      List<Package> output =
        await _srv.GetPackagesPaginatedAsync(page, amount);
      return StatusCode(200, output);
    }

    [HttpGet("GetTopPackages/{amount}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTopPackages(int amount) {
      List<Package> output = await _srv.GetTopPackagesAsync(amount);
      return StatusCode(200, output);
    }

    [HttpPost("CreatePackage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult>
    CreatePackage([FromBody] PackageDtoIn newPackage) {
      Package output = await _srv.CreatePackageAsync(new Package(newPackage));
      return StatusCode(200, output);
    }

    [HttpGet("GetFullPackage/{packageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFullPackage(Guid packageId) {
      Package output = await _srv.GetFullPackageAsync(packageId);
      return StatusCode(200, output);
    }

    [HttpDelete("ArchivePackage/{packageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ArchivePackage(Guid packageId) {
      return StatusCode(200, await _srv.ArchivePackageAsync(packageId));
    }
  }
}

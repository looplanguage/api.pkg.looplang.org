using System.Collections.Generic;
using System.Threading.Tasks;
using lpr.Common.Interfaces;
using lpr.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lpr.WebAPI.Controllers
{
[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private readonly IPackageService _srv;
    public PackageController(IPackageService srv)
    {
        _srv = srv;
    }

    // GET: PackageController
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(int page, int amount)
    {
        List<Package> output = await _srv.GetPackagesPaginatedAsync(page, amount);
        return StatusCode(200, output);
    }
}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize("ApiScope")]
public class IdentityController : ControllerBase
{
    public IdentityController()
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
}
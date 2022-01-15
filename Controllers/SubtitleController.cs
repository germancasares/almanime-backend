using Almanime.Models.DTO;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SubtitleController : ControllerBase
{
    private readonly ISubtitleService _subtitleService;

    public SubtitleController(ISubtitleService subtitleService)
    {
        _subtitleService = subtitleService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostAsync([FromForm]SubtitleDTO subtitleDTO)
    {
        var subtitle = await _subtitleService.Create(subtitleDTO, User.GetAuth0ID());

        return Ok(subtitle.Url);
    }
}

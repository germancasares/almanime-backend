using Almanime.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeService _episodeService;

    public EpisodeController(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    [HttpPost("populate/anime/{animeSlug}")]
    public async Task<IActionResult> Populate(string animeSlug)
    {
        await _episodeService.Populate(animeSlug);
        return Ok();
    }
}

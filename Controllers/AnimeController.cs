using Almanime.Models.Enums;
using Almanime.Models.Views;
using Almanime.Models.Views.Derived;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Almanime.Utils.DataAnnotations;
using API.Utils.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly IAnimeService _animeService;

    public AnimeController(IAnimeService animeService)
    {
        _animeService = animeService;
    }

    [HttpGet("year/{year}/season/{season}")]
    public IActionResult GetSeason(
        int year,
        ESeason season,
        [FromQuery] int page = 1,
        [FromQuery][Max(25)] int size = 8,
        [FromQuery] bool includeMeta = false
    )
    {
        var animeSeason = _animeService.GetSeason(year, season);

        var animeSeasonPage = animeSeason
            .OrderBy(a => string.IsNullOrWhiteSpace(a.CoverImageUrl))
            .ThenBy(a => a.Name)
            .Page(page, size);

        var animeSeasonPageView = new ModelWithMetaView<List<AnimeView>>
        {
            Models = animeSeasonPage.Select(a => a.MapToView()).ToList(),
        };

        if (includeMeta)
        {
            animeSeasonPageView.Meta = new PaginationMetaView
            {
                BaseUrl = Request.GetFullPath(),
                Count = animeSeason.Count(),
                CurrentPage = page,
                PageSize = size,
            };
        }

        return Ok(animeSeasonPageView);
    }

    [HttpPost("populate/year/{year}/season/{season}")]
    public async Task<IActionResult> PopulateSeasonAsync(int year, ESeason season)
    {
        await _animeService.PopulateSeason(year, season);
        return Ok();
    }
}

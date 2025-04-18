using Almanime.Models.Enums;
using Almanime.Models.Views;
using Almanime.Models.Views.Derived;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Almanime.Utils.DataAnnotations;
using Almanime.Utils.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers.v1;

[Route("v1/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly IAnimeService _animeService;

    public AnimeController(IAnimeService animeService) => _animeService = animeService;

    [HttpGet]
    public IActionResult Get()
    {
        var animes = _animeService.Get();

        return Ok(animes.Select(anime => new
        {
            anime.Slug,
            anime.Name,
            anime.Season,
            anime.Status,
            anime.CoverImages,
            Episodes = anime.Episodes.Count,
        }));
    }

    [HttpGet("search/{animeName}")]
    public IActionResult Search(string animeName)
    {
        var results = _animeService.Search(animeName);

        return Ok(results);
    }

    [HttpGet("slug/{slug}")]
    public IActionResult GetBySlug(string slug)
    {
        var anime = _animeService.GetBySlug(slug);

        return anime == null ? NotFound() : Ok(anime.MapToView());
    }

    [HttpGet("bookmarked")]
    [Authorize]
    public IActionResult GetByBookmarks()
    {
        var auth0ID = User.GetAuth0ID();

        return Ok(_animeService.GetByBookmarks(auth0ID).Select(anime => new
        {
            anime.Slug,
            anime.Name,
            anime.Season,
            anime.Status,
            anime.CoverImages,
            Episodes = anime.Episodes.Count,
        }));
    }

    [HttpGet("year/{year}/season/{season}")]
    public IActionResult GetSeason(
      int year,
      ESeason season,
      [FromQuery][Min(1)] int page = 1,
      [FromQuery][Max(25)] int size = 8,
      [FromQuery] bool includeMeta = false
    )
    {
        var animeSeason = _animeService.GetSeason(year, season);

        var animeSeasonPage = animeSeason
          .OrderByDescending(anime => anime.CoverImages != null && anime.CoverImages.Tiny != null)
          .ThenBy(a => a.Name)
          .Page(page, size);

        var animeSeasonPageView = new ModelWithMetaView<List<AnimeView>>
        {
            Models = animeSeasonPage.Select(a => a.MapToView()).ToList(),
        };

        if (includeMeta)
        {
            animeSeasonPageView = animeSeasonPageView with
            {
                Meta = new()
                {
                    BaseUrl = Request.GetFullPath(),
                    Count = animeSeason.Count(),
                    CurrentPage = page,
                    PageSize = size,
                }
            };
        }

        return Ok(animeSeasonPageView);
    }

    [HttpPost("populate/year/{year}/season/{season}")]
    [Authorize("write:animes")]
    public async Task<IActionResult> PopulateSeasonAsync(int year, ESeason season)
    {
        await _animeService.PopulateSeason(year, season);
        return NoContent();
    }
}

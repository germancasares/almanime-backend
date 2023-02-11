using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookmarkController : ControllerBase
{
  private readonly IBookmarkService _bookmarkService;

  public BookmarkController(IBookmarkService bookmarkService)
  {
    _bookmarkService = bookmarkService;
  }

  [HttpGet]
  [Authorize]
  public IActionResult Get()
  {
    var auth0ID = User.GetAuth0ID();

    return Ok(_bookmarkService.GetByAuth0ID(auth0ID).Select(bookmark => bookmark.Anime.Slug));
  }

  [HttpPost("animeSlug/{animeSlug}")]
  [Authorize]
  public IActionResult Create(string animeSlug)
  {
    var auth0ID = User.GetAuth0ID();

    _bookmarkService.Create(auth0ID, animeSlug);

    return NoContent();
  }

  [HttpDelete("animeSlug/{animeSlug}")]
  [Authorize]
  public IActionResult Delete(string animeSlug)
  {
    var auth0ID = User.GetAuth0ID();

    _bookmarkService.Delete(auth0ID, animeSlug);

    return NoContent();
  }
}

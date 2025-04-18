using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers.v1;

[Route("v1/[controller]")]
[ApiController]
public class FansubController : ControllerBase
{
    private readonly IFansubService _fansubService;

    public FansubController(IFansubService fansubService)
    {
        _fansubService = fansubService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var fansubs = _fansubService.Get();

        return Ok(fansubs.Select(fansub => new
        {
            fansub.Acronym,
            fansub.Name,
            fansub.Webpage,
            fansub.CreationDate,
            Members = fansub.FansubRoles.Select(role => role.Memberships).Count(),
        }));
    }

    [HttpGet("search/{fansubName}")]
    public IActionResult Search(string fansubName)
    {
        var results = _fansubService.Search(fansubName);

        return Ok(results);
    }

    [HttpGet("acronym/{acronym}")]
    public IActionResult GetByAcronym(string acronym)
    {
        var fansub = _fansubService.GetByAcronym(acronym);

        return fansub == null
          ? NotFound()
          : Ok(new
          {
              fansub.Acronym,
              fansub.Name,
              fansub.Webpage,
              fansub.CreationDate,
              Members = fansub.FansubRoles.Sum(role => role.Memberships.Count),
          });
    }

    [HttpGet("acronym/{acronym}/isMember")]
    [Authorize]
    public IActionResult IsMember(string acronym)
    {
        var isMember = _fansubService.IsMember(acronym, User.GetAuth0ID());

        return Ok(isMember);
    }

    [HttpGet("acronym/{acronym}/members")]
    public IActionResult GetMembers(string acronym)
    {
        var members = _fansubService.GetMembers(acronym);

        return Ok(members.Select(member => new
        {
            member.User.Name,
            Role = member.FansubRole.Name,
        }));
    }

    [HttpGet("acronym/{acronym}/subtitles")]
    public IActionResult GetSubtitles(string acronym)
    {
        var subtitles = _fansubService.GetSubtitles(acronym);

        return Ok(subtitles.Select(subtitle => new
        {
            subtitle.ID,
            subtitle.Url,
            subtitle.Format,
            subtitle.CreationDate,
            subtitle.Language,
            Anime = subtitle.Episode.Anime.Name,
            AnimeSlug = subtitle.Episode.Anime.Slug,
            Episode = subtitle.Episode.Number,
            User = subtitle.Membership.User.Name,
        }));
    }

    [HttpGet("acronym/{acronym}/subtitles/drafts")]
    [Authorize]
    public IActionResult GetSubtitlesDrafts(string acronym)
    {
        var user = User.GetAuth0ID();

        var subtitles = _fansubService.GetSubtitlesDrafts(acronym, user);

        return Ok(subtitles.Select(subtitle => new
        {
            subtitle.ID,
            subtitle.Url,
            subtitle.Format,
            subtitle.CreationDate,
            subtitle.Language,
            Anime = subtitle.Episode.Anime.Name,
            AnimeSlug = subtitle.Episode.Anime.Slug,
            Episode = subtitle.Episode.Number,
            User = subtitle.Membership.User.Name,
        }));
    }

    [HttpGet("acronym/{acronym}/roles")]
    [Authorize]
    public IActionResult GetRoles(string acronym)
    {
        var roles = _fansubService.GetRoles(acronym);

        return Ok(roles);
    }

    [HttpPut("acronym/{acronym}/roles")]
    [Authorize]
    public IActionResult PutRoles(string acronym, Dictionary<string, IEnumerable<EPermission>> roles)
    {
        var user = User.GetAuth0ID();

        _fansubService.UpdateRoles(acronym, user, roles);

        return NoContent();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(FansubDTO fansubDTO)
    {
        var user = User.GetAuth0ID();

        var fansub = _fansubService.Create(fansubDTO, user);

        return Ok(new
        {
            fansub.Acronym,
            fansub.Name,
            fansub.Webpage,
        });
    }

    [HttpPost("acronym/{acronym}/join")]
    [Authorize]
    public IActionResult Join(string acronym)
    {
        var user = User.GetAuth0ID();

        _fansubService.Join(acronym, user);

        return NoContent();
    }
}

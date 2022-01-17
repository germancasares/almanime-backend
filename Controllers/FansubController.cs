using Almanime.Models.DTO;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
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
            Members = fansub.Members.Count,
        }));
    }

    [HttpGet("acronym/{acronym}")]
    public IActionResult GetByAcronym(string acronym)
    {
        var fansub = _fansubService.GetByAcronym(acronym);

        if (fansub == null) return NotFound();

        return Ok(new {
            fansub.Acronym,
            fansub.Name,
            fansub.Webpage,
            fansub.CreationDate,
            Members = fansub.Members.Count,
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
            Role = member.Role.ToString(),
        }));
    }

    [HttpGet("acronym/{acronym}/subtitles")]
    public IActionResult GetSubtitles(string acronym)
    {
        var subtitles = _fansubService.GetSubtitles(acronym);

        return Ok(subtitles.Select(subtitle => new {
            subtitle.ID,
            subtitle.Url,
            subtitle.Format,
            subtitle.CreationDate,
            Anime = subtitle.Episode.Anime.Name,
            Episode = subtitle.Episode.Number,
            User = subtitle.Member.User.Name,
        }));
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(FansubDTO fansubDTO)
    {
        var fansub = _fansubService.Create(fansubDTO, User.GetAuth0ID());

        return Ok(new {
            fansub.Acronym,
            fansub.Name,
            fansub.Webpage,
        });
    }
}

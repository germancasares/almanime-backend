using Almanime.Models;
using Almanime.Models.DTO;

namespace Almanime.Services.Interfaces;

public interface ISubtitleService
{
    Task<Subtitle> Create(SubtitleDTO subtitleDTO, string? auth0ID);
}

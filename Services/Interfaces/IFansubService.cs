using Almanime.Models;
using Almanime.Models.DTO;

namespace Almanime.Services.Interfaces;

public interface IFansubService
{
    Fansub Create(FansubDTO fansubDTO, string? auth0ID);
    Fansub? GetByAcronym(string acronym);
    ICollection<Member> GetMembers(string acronym);
    IEnumerable<Subtitle> GetSubtitles(string acronym);
    bool IsMember(string acronym, string? auth0ID);
}

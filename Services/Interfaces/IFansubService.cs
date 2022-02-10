using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.DTO;

namespace Almanime.Services.Interfaces;

public interface IFansubService
{
    Fansub Create(FansubDTO fansubDTO, string? auth0ID);
    IQueryable<Fansub> Get();
    Fansub? GetByAcronym(string acronym);
    //ICollection<Member> GetMembers(string acronym);
    //IEnumerable<Subtitle> GetSubtitles(string acronym);
    //bool IsMember(string acronym, string? auth0ID);
    void Join(string acronym, string? auth0ID);
    IReadOnlyCollection<FansubDocument> Search(string fansubName);
}

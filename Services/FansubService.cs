using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;
using Domain.Enums;

namespace Almanime.Services;

public class FansubService : IFansubService
{
    private readonly AlmanimeContext _context;
    public FansubService(AlmanimeContext context)
    {
        _context = context;
    }

    public bool IsMember(string acronym, string? auth0ID)
    {
        if (auth0ID == null) return false;

        return _context.Members.Any(member => member.Fansub.Acronym == acronym && member.User.Auth0ID == auth0ID);
    }

    public Fansub? GetByAcronym(string acronym) => _context.Fansubs.GetByAcronym(acronym);
    public ICollection<Member> GetMembers(string acronym) => 
        _context.Fansubs.GetByAcronym(acronym)?.Members ?? throw new ArgumentNullException(nameof(acronym));
    public IEnumerable<Subtitle> GetSubtitles(string acronym) => 
        GetMembers(acronym).SelectMany(member => member.Subtitles)
        .Where(subtitle => subtitle.Status == ESubtitleStatus.Published) ?? throw new ArgumentNullException(nameof(acronym));

    public Fansub Create(FansubDTO fansubDTO, string? auth0ID)
    {
        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));

        var user = _context.Users.GetByAuth0ID(auth0ID);

        if (user == null) throw new ArgumentNullException(nameof(auth0ID));

        var fansub = _context.Fansubs.Add(fansubDTO.MapToModel()).Entity;

        _context.Members.Add(new Member
        {
            Fansub = fansub,
            User = user,
            Role = EFansubRole.Founder,
        });

        _context.SaveChanges();

        return fansub;
    }
}

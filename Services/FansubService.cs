using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;
using Domain.Enums;
using Nest;

namespace Almanime.Services;

public class FansubService : IFansubService
{
    private readonly AlmanimeContext _context;
    private readonly ElasticClient _elasticClient;

    public FansubService(AlmanimeContext context, ElasticClient elasticClient)
    {
        _context = context;
        _elasticClient = elasticClient;
    }

    public bool IsMember(string acronym, string? auth0ID)
    {
        if (auth0ID == null) return false;

        return _context.Memberships.Any(membership => membership.FansubRole.Fansub.Acronym == acronym && membership.User.Auth0ID == auth0ID);
    }
    public IQueryable<Fansub> Get() => _context.Fansubs.AsQueryable();

    public IReadOnlyCollection<FansubDocument> Search(string fansubName) => _elasticClient.Search<FansubDocument>(s =>
    s.Index("fansubs").From(0).Size(10)
        .Query(q => q.QueryString(qs => qs.Query(fansubName).DefaultField(f => f.Name).DefaultOperator(Operator.And)))
    ).Documents;

    public Fansub? GetByAcronym(string acronym) => _context.Fansubs.GetByAcronym(acronym);
    public IEnumerable<Membership> GetMembers(string acronym) =>
        _context.Fansubs.GetByAcronym(acronym)?.FansubRoles.SelectMany(role => role.Memberships) ?? throw new ArgumentNullException(nameof(acronym));
    public IEnumerable<Subtitle> GetSubtitles(string acronym) =>
        GetMembers(acronym).SelectMany(membership => membership.Subtitles)
        .Where(subtitle => subtitle.Status == ESubtitleStatus.Published) ?? throw new ArgumentNullException(nameof(acronym));

    public Fansub Create(FansubDTO fansubDTO, string? auth0ID)
    {
        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));
        var user = _context.Users.GetByAuth0ID(auth0ID);
        if (user == null) throw new ArgumentNullException(nameof(auth0ID));

        var fansub = _context.Fansubs.Add(fansubDTO.MapToModel()).Entity;

        var createSubtitlePermission = _context.Permission.Single(p => p.Grant == EPermission.CreateSubtitle);
        var adminRole = _context.FansubRoles.Add(new FansubRole
        {
            Fansub = fansub,
            Name = "Admin",
            Permissions = new[] { createSubtitlePermission }
        }).Entity;

        _context.Memberships.Add(new Membership
        {
            UserID = user.ID,
            FansubID = fansub.ID,
            RoleID = adminRole.ID,
        });

        _context.SaveChanges();

        _elasticClient.Index(fansub.MapToDocument(), idx => idx.Index("fansubs"));

        return fansub;
    }

    public void Join(string acronym, string? auth0ID)
    {
        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));
        var user = _context.Users.GetByAuth0ID(auth0ID);
        if (user == null) throw new ArgumentNullException(nameof(auth0ID));

        var fansub = _context.Fansubs.GetByAcronym(acronym);
        if (fansub == null) throw new ArgumentNullException(nameof(acronym));

        var isAlreadyMember = _context.Memberships.Any(membership => membership.UserID == user.ID && membership.FansubID == fansub.ID);
        if (isAlreadyMember) throw new InvalidOperationException("User is already a member of the fansub");

        var memberRole = _context.FansubRoles.SingleOrDefault(role => role.FansubID == fansub.ID && role.Name == "Member");
        if (memberRole == null)
        {
            var createSubtitlePermission = _context.Permission.Single(p => p.Grant == EPermission.CreateSubtitle);


            memberRole = _context.FansubRoles.Add(new FansubRole
            {
                Fansub = fansub,
                Name = "Member",
                Permissions = new [] { createSubtitlePermission }
            }).Entity;
        }

        _context.Memberships.Add(new Membership
        {
            UserID = user.ID,
            FansubID = fansub.ID,
            RoleID = memberRole.ID,
        });

        _context.SaveChanges();
    }
}

using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Almanime.Utils.Mappers;
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

  public bool IsMember(string acronym, string auth0ID) => _context.Memberships
      .Any(membership => membership.FansubRole.Fansub.Acronym == acronym && membership.User.Auth0ID == auth0ID);
  public IQueryable<Fansub> Get() => _context.Fansubs.AsQueryable();

  public IReadOnlyCollection<FansubDocument> Search(string fansubName) => _elasticClient.Search<FansubDocument>(s =>
  s.Index("fansubs").From(0).Size(10)
    .Query(q => q.QueryString(qs => qs.Query(fansubName).DefaultField(f => f.Name).DefaultOperator(Operator.And)))
  ).Documents;

  public Fansub GetByAcronym(string acronym) => _context.Fansubs.GetByAcronym(acronym);
  public IEnumerable<Membership> GetMembers(string acronym) =>
    _context.Fansubs.GetByAcronym(acronym).FansubRoles.SelectMany(role => role.Memberships);
  public IEnumerable<Subtitle> GetSubtitles(string acronym) =>
    GetMembers(acronym).SelectMany(membership => membership.Subtitles)
    .Where(subtitle => subtitle.Status == ESubtitleStatus.Published);

  public IEnumerable<Subtitle> GetSubtitlesDrafts(string acronym, string auth0ID)
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var fansub = _context.Fansubs.GetByAcronym(acronym);

    _context.Memberships.ThrowIfUserDoesntHavePermissionInFansub(fansub, user, EPermission.PublishSubtitle);

    return GetMembers(acronym).SelectMany(membership => membership.Subtitles)
    .Where(subtitle => subtitle.Status == ESubtitleStatus.Draft);
  }

  public Dictionary<string, IEnumerable<EPermission>> GetRoles(string acronym)
  {
    var fansubPermissions = _context.FansubRoles
      .Where(r => r.Fansub.Acronym == acronym)
      .Select(role => new KeyValuePair<string, IEnumerable<EPermission>>(
        role.Name,
        role.Permissions.Select(p => p.Grant)
      ));

    return new Dictionary<string, IEnumerable<EPermission>>(fansubPermissions);
  }

  public Fansub Create(FansubDTO fansubDTO, string auth0ID)
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var fansub = _context.Fansubs.Add(fansubDTO.MapToModel()).Entity;

    var draftSubtitlePermission = _context.Permission.Single(p => p.Grant == EPermission.DraftSubtitle);
    var publishSubtitlePermission = _context.Permission.Single(p => p.Grant == EPermission.PublishSubtitle);
    var editPermissionsPermission = _context.Permission.Single(p => p.Grant == EPermission.EditPermissions);

    var adminRole = _context.FansubRoles.Add(new FansubRole(
      name: "admin",
      fansubID: fansub.ID,
      permissions: new[] { draftSubtitlePermission, publishSubtitlePermission, editPermissionsPermission }
    )).Entity;

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

  public void Join(string acronym, string auth0ID)
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var fansub = _context.Fansubs.GetByAcronym(acronym);

    var isAlreadyMember = _context.Memberships.Any(membership => membership.UserID == user.ID && membership.FansubID == fansub.ID);
    if (isAlreadyMember)
    {
      throw new AlmDbException(
        EValidationCode.AlreadyInDB, "member", new()
        {
          { nameof(user), user.Name },
          { nameof(fansub), fansub.Name },
        }
      );
    }

    var memberRole = _context.FansubRoles.SingleOrDefault(role => role.FansubID == fansub.ID && role.Name == "Member");
    if (memberRole == null)
    {
      var draftSubtitlePermission = _context.Permission.Single(p => p.Grant == EPermission.DraftSubtitle);
      var publishSubtitlePermission = _context.Permission.Single(p => p.Grant == EPermission.DraftSubtitle);

      memberRole = _context.FansubRoles.Add(new FansubRole(
        name: "Member",
        fansubID: fansub.ID,
        permissions: new[] { draftSubtitlePermission, publishSubtitlePermission }
      )).Entity;
    }

    _context.Memberships.Add(new Membership
    {
      UserID = user.ID,
      FansubID = fansub.ID,
      RoleID = memberRole.ID,
    });

    _context.SaveChanges();
  }

  public void UpdateRoles(string acronym, string auth0ID, Dictionary<string, IEnumerable<EPermission>> roles)
  {
    var user = _context.Users.GetByAuth0ID(auth0ID);
    var fansub = _context.Fansubs.GetByAcronym(acronym);

    _context.Memberships.ThrowIfUserDoesntHavePermissionInFansub(fansub, user, EPermission.EditPermissions);

    if (!roles["Admin"].Contains(EPermission.EditPermissions))
    {
      roles["Admin"] = roles["Admin"].Append(EPermission.EditPermissions);
    }

    foreach (var roleName in roles.Keys)
    {
      var role = _context.FansubRoles.SingleOrDefault(r => r.Name == roleName && r.FansubID == fansub.ID);
      if (role == null) continue;

      role.Permissions.Clear();
      role.Permissions = _context.Permission.Where(p => roles[roleName].Contains(p.Grant)).ToList();
    }

    _context.SaveChanges();
  }
}

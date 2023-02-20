using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class MemberQueries
{
  public static Membership GetByFansubAndUser(this DbSet<Membership> memberships, Guid fansubID, Guid userID)
  {
    var membership = memberships.SingleOrDefault(membership => membership.FansubID == fansubID && membership.UserID == userID);
    return membership ?? throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(membership), new()
    {
      { nameof(fansubID), fansubID },
      { nameof(userID), userID },
    });
  }

  public static bool HasUserPermissionInFansub(this DbSet<Membership> memberships, Guid fansubID, Guid userID, EPermission permission) =>
    GetByFansubAndUser(memberships, fansubID, userID)?
    .FansubRole
    .Permissions
    .Any(
      p => p.Grant == permission
    ) ?? false;

  public static void ThrowIfUserDoesntHavePermissionInFansub(this DbSet<Membership> memberships, Fansub fansub, User user, EPermission permission)
  {
    var hasPermission = HasUserPermissionInFansub(memberships, fansub.ID, user.ID, permission);
    if (!hasPermission) throw new AlmPermissionException(permission, user.Name, fansub.Name);
  }
}

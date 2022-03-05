using Almanime.Models;
using Almanime.Models.Enums;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class MemberQueries
{
    public static Membership GetByFansubAndUser(this DbSet<Membership> memberships, Guid fansubID, Guid userID)
    {
        var membership = memberships.SingleOrDefault(membership => membership.FansubID == fansubID && membership.UserID == userID);
        if (membership == null)
        {
            throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(membership), new()
            {
                { nameof(fansubID), fansubID },
                { nameof(userID), userID },
            });
        }

        return membership;
    }

    public static bool HasUserPermissionInFansub(this DbSet<Membership> memberships, Guid fansubID, Guid userID, EPermission permission) =>
        GetByFansubAndUser(memberships, fansubID, userID)?
        .FansubRole
        .Permissions
        .Any(
            p => p.Grant == permission
        ) ?? false;
}

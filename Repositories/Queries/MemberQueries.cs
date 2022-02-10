using Almanime.Models;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class MemberQueries
{
    public static Membership? GetByFansubAndUser(this DbSet<Membership> memberships, Guid fansubID, Guid userID) 
        => memberships.SingleOrDefault(membership => membership.FansubID == fansubID && membership.UserID == userID);
}

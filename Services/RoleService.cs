using Almanime.Models;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Services.Interfaces;

namespace Almanime.Services;

public class RoleService(
  AlmanimeContext context
    ) : IRoleService
{
    private readonly AlmanimeContext _context = context;

    public Dictionary<string, IEnumerable<EPermission>> GetByUser(User user)
    {
        var userPermissions = _context.Memberships
          .Where(membership => membership.UserID == user.ID)
          .Select(membership => new KeyValuePair<string, IEnumerable<EPermission>>(
              membership.FansubRole.Fansub.Acronym,
              membership.FansubRole.Permissions.Select(permission => permission.Grant)
            )
          ).ToList();

        return new Dictionary<string, IEnumerable<EPermission>>(userPermissions);
    }
}

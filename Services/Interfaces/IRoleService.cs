using Almanime.Models;
using Almanime.Models.Enums;

namespace Almanime.Services.Interfaces;

public interface IRoleService
{
  Dictionary<string, IEnumerable<EPermission>> GetByUser(User user);
}

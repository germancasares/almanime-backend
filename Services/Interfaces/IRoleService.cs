using Almanime.Models;
using Domain.Enums;

namespace Almanime.Services.Interfaces;

public interface IRoleService
{
    Dictionary<string, IEnumerable<EPermission>> GetByUser(User user);
}

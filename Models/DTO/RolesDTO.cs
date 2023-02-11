using Almanime.Models.Enums;

namespace Almanime.Models.DTO;

public record RolesDTO
{
  public Dictionary<string, IEnumerable<EPermission>>? Roles { get; set; }
}

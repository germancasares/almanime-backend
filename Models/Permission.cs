using Almanime.Models.Enums;

namespace Almanime.Models;

public class Permission : Base
{
  public EPermission Grant { get; set; }

  public virtual ICollection<FansubRole> FansubRoles { get; set; } = default!;
}

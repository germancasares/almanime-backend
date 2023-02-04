namespace Almanime.Models;

public class FansubRole : Base
{
  public FansubRole(
    string name,
    Guid fansubID,
    ICollection<Permission> permissions
  ) : this(name, fansubID)
  {
    Permissions = permissions;
  }

  protected FansubRole(
    string name,
    Guid fansubID
  )
  {
    Name = name;
    FansubID = fansubID;
  }

  public string Name { get; set; }

  public Guid FansubID { get; set; }
  public virtual Fansub Fansub { get; set; } = default!;

  public virtual ICollection<Membership> Memberships { get; set; } = default!;
  public virtual ICollection<Permission> Permissions { get; set; } = default!;
}

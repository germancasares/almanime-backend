namespace Almanime.Models;

public class FansubRole : Base
{
  public FansubRole(
    string name,
    Fansub fansub,
    ICollection<Permission> permissions
  ) : this(name, fansub.ID, fansub)
  {
    Permissions = permissions;
  }

  protected FansubRole(
    string name,
    Guid fansubID,
    Fansub fansub
  )
  {
    Name = name;
    FansubID = fansubID;
    Fansub = fansub;
  }

  public string Name { get; set; }

  public Guid FansubID { get; set; }
  public virtual Fansub Fansub { get; set; } = default!;

  public virtual ICollection<Membership> Memberships { get; set; } = default!;
  public virtual ICollection<Permission> Permissions { get; set; } = default!;
}

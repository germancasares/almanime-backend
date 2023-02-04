namespace Almanime.Models;

public class User : Base
{
  public User(string auth0ID, string name)
  {
    Auth0ID = auth0ID ?? throw new ArgumentNullException(nameof(auth0ID));
    Name = name ?? throw new ArgumentNullException(nameof(name));
  }

  public string Auth0ID { get; set; }
  public string Name { get; set; }

  public virtual ICollection<Bookmark> Bookmarks { get; set; } = default!;
  public virtual ICollection<Membership> Memberships { get; set; } = default!;
}

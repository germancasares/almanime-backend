namespace Almanime.Models;

public class User(string auth0ID, string name) : Base
{

    public string Auth0ID { get; set; } = auth0ID ?? throw new ArgumentNullException(nameof(auth0ID));
    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));

    public virtual ICollection<Bookmark> Bookmarks { get; set; } = default!;
    public virtual ICollection<Membership> Memberships { get; set; } = default!;
}

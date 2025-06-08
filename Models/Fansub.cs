namespace Almanime.Models;

public class Fansub(string acronym, string name, string? webpage) : Base
{

    public string Acronym { get; set; } = acronym ?? throw new ArgumentNullException(nameof(acronym));

    public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));
    public string? Webpage { get; set; } = webpage;

    public virtual ICollection<FansubRole> FansubRoles { get; set; } = default!;
}

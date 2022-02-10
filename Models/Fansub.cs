namespace Almanime.Models;

public class Fansub : Base
{
    public Fansub(string acronym, string name, string? webpage)
    {
        Acronym = acronym ?? throw new ArgumentNullException(nameof(acronym));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Webpage = webpage;
    }

    public string Acronym { get; set; }

    public string Name { get; set; }
    public string? Webpage { get; set; }
}

namespace Almanime.Models;

public class Episode : Base
{
    public Episode(int number, string? name, DateTime? aired, int? duration, Guid animeID)
    {
        Number = number;
        Name = name;
        Aired = aired;
        Duration = duration;
        AnimeID = animeID;
    }

    public int Number { get; set; }
    public string? Name { get; set; }
    public DateTime? Aired { get; set; }
    public int? Duration { get; set; }

    public Guid AnimeID { get; set; }
    public virtual Anime Anime { get; set; } = default!;
}

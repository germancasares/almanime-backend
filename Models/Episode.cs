namespace Almanime.Models;

public class Episode(
  int number,
  string? name,
  DateTime? aired,
  int? duration,
  Guid animeID
    ) : Base
{

    public int Number { get; set; } = number;
    public string? Name { get; set; } = name;
    public DateTime? Aired { get; set; } = aired;
    public int? Duration { get; set; } = duration;

    public Guid AnimeID { get; set; } = animeID;
    public virtual Anime Anime { get; set; } = default!;

    public virtual ICollection<Subtitle> Subtitles { get; set; } = default!;
}

namespace Almanime.Models;

public class Bookmark : Base
{
  public Guid AnimeID { get; set; }
  public virtual Anime Anime { get; set; } = default!;

  public Guid UserID { get; set; }
  public virtual User User { get; set; } = default!;
}

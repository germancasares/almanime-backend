namespace Almanime.Models.Documents;

public class AnimeDocument
{
    public Guid ID { get; set; }
    public DateTime CreationDate { get; set; }
    public string Slug { get; set; } = default!;
    public string Name { get; set; } = default!;
}

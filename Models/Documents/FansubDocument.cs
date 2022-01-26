namespace Almanime.Models.Documents;

public class FansubDocument
{
    public Guid ID { get; set; }
    public DateTime CreationDate { get; set; }
    public string Acronym { get; set; } = default!;
    public string Name { get; set; } = default!;
}

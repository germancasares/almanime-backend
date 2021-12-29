namespace Almanime.Models.DTO;

public record EpisodeDTO
{
    public int Number { get; set; }
    public string? Name { get; set; }
    public DateTime? Aired { get; set; }
    public int? Duration { get; set; }
}

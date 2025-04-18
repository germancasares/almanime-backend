namespace Almanime.Models.DTO;

public record EpisodeDTO
{
    public int Number { get; init; }
    public string? Name { get; set; }
    public DateTime? Aired { get; init; }
    public int? Duration { get; init; }
}

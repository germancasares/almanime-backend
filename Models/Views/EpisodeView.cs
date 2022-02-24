namespace Almanime.Models.Views;

public readonly record EpisodeView
{
    public Guid ID { get; init; }

    public int Number { get; init; }
    public string? Name { get; init; }
    public DateTime? Aired { get; init; }
    public int? Duration { get; init; }
}

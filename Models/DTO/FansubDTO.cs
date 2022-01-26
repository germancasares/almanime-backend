namespace Almanime.Models.DTO;

public readonly record struct FansubDTO
{
    public string? Acronym { get; init; }
    public string? Name { get; init; }
    public string? Webpage { get; init; }
}

namespace Almanime.ETL.Kitsu;

public record Manami
{
  public List<ManamiAnime> Data { get; set; } = new();
}

public record ManamiAnime
{
  public string? Type { get; set; }
  public List<string> Sources { get; set; } = new();
}

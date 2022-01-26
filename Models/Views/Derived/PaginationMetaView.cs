namespace Almanime.Models.Views.Derived;

public readonly record struct PaginationMetaView
{
    public string? BaseUrl { private get; init; }
    public int PageSize { private get; init; }
    public int CurrentPage { private get; init; }
    public int Count { get; init; }
    public string First => $"{BaseUrl}?page=1&size={PageSize}";
    public string? Previous => CurrentPage == 1 ? null : $"{BaseUrl}?page={CurrentPage - 1}&size={PageSize}";
    public string? Next => CurrentPage * PageSize <= Count ? $"{BaseUrl}?page={CurrentPage + 1}&size={PageSize}" : null;
    public string Last => $"{BaseUrl}?page={Math.Ceiling(Count / (double)PageSize)}&size={PageSize}";
}

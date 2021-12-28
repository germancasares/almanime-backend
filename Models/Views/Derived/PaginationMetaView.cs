namespace Almanime.Models.Views.Derived;

public record PaginationMetaView
{
    public string? BaseUrl { private get; set; }
    public int PageSize { private get; set; }
    public int CurrentPage { private get; set; }
    public int Count { get; set; }
    public string First => $"{BaseUrl}?page=1&size={PageSize}";
    public string? Previous => CurrentPage == 1 ? null : $"{BaseUrl}?page={CurrentPage - 1}&size={PageSize}";
    public string? Next => CurrentPage * PageSize <= Count ? $"{BaseUrl}?page={CurrentPage + 1}&size={PageSize}" : null;
    public string Last => $"{BaseUrl}?page={Math.Ceiling(Count / (double)PageSize)}&size={PageSize}";
}

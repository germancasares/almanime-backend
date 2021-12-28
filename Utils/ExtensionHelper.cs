using System.Text.Json;

namespace Almanime.Utils;

public static class ExtensionHelper
{
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize) => source.Skip((page - 1) * pageSize).Take(pageSize);
    public static string GetFullPath(this HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}";

}

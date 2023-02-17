using Almanime.Models;
using System.Security.Claims;

namespace Almanime.Utils;

public static class ExtensionHelper
{
  public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize) => source.Skip((page - 1) * pageSize).Take(pageSize);
  public static string GetFullPath(this HttpRequest request) => $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}";
  public static string GetAuth0ID(this ClaimsPrincipal user)
  {
    var auth0ID = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    return auth0ID ?? throw new AlmNullException(nameof(auth0ID));
  }

  public static string GetExtension(this IFormFile file) => Path.GetExtension(file.FileName);
}

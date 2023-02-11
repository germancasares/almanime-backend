using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface IBookmarkService
{
  void Create(string auth0ID, string slug);
  void Delete(string auth0ID, string slug);
  IQueryable<Bookmark> GetByAuth0ID(string auth0ID);
}

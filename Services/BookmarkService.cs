using Almanime.Models;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;

namespace Almanime.Services;

public class BookmarkService(AlmanimeContext context) : IBookmarkService
{
    private readonly AlmanimeContext _context = context;

    public IQueryable<Bookmark> GetByAuth0ID(string auth0ID) => _context.Bookmarks.Where(bookmark => bookmark.User.Auth0ID == auth0ID);

    public void Create(string auth0ID, string slug)
    {
        var user = _context.Users.GetByAuth0ID(auth0ID);
        var anime = _context.Animes.GetBySlug(slug);

        var bookmark = _context.Bookmarks.SingleOrDefault(bookmark => bookmark.UserID == user.ID && bookmark.AnimeID == anime.ID);
        if (bookmark != null) return;

        _context.Bookmarks.Add(new Bookmark
        {
            Anime = anime,
            User = user,
        });
        _context.SaveChanges();
    }

    public void Delete(string auth0ID, string slug)
    {
        var user = _context.Users.GetByAuth0ID(auth0ID);
        var anime = _context.Animes.GetBySlug(slug);

        var bookmark = _context.Bookmarks.SingleOrDefault(bookmark => bookmark.UserID == user.ID && bookmark.AnimeID == anime.ID);
        if (bookmark == null) return;

        _context.Bookmarks.Remove(bookmark);
        _context.SaveChanges();
    }
}

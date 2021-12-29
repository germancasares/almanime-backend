using Almanime.Models;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class AnimeQueries
{
    public static Anime? GetByKitsuID(this DbSet<Anime> animes, int kitsuID) => animes.SingleOrDefault(anime => anime.KitsuID == kitsuID);
    public static Anime? GetBySlug(this DbSet<Anime> animes, string slug) => animes.SingleOrDefault(anime => anime.Slug == slug);
}

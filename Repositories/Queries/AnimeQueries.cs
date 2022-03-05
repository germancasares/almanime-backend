using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class AnimeQueries
{
    public static Anime? GetByKitsuID(this DbSet<Anime> animes, int kitsuID) => animes.SingleOrDefault(anime => anime.KitsuID == kitsuID);

    public static Anime GetBySlug(this DbSet<Anime> animes, string slug)
    {
        var anime = animes.SingleOrDefault(anime => anime.Slug == slug);
        if (anime == null)
        {
            throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(anime), new()
            {
                { nameof(slug), slug },
            });
        }

        return anime;
    }
}

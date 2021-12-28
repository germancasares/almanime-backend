using Almanime.Models;
using Almanime.Models.Enums;

namespace Almanime.Services.Interfaces
{
    public interface IAnimeService
    {
        IQueryable<Anime> GetSeason(int year, ESeason season);
        Task PopulateSeason(int year, ESeason season);
    }
}

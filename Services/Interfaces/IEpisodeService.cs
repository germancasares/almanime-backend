namespace Almanime.Services.Interfaces;

public interface IEpisodeService
{
    Task Populate(string animeSlug);
}

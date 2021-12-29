using Almanime.Models;
using Almanime.Models.DTO;

namespace Almanime.Utils.Mappers;

public static class EpisodeMapper
{
    public static Episode MapToModel(this EpisodeDTO episodeDTO, Guid animeID)
    {
        return new Episode(
            number: episodeDTO.Number,
            name: episodeDTO.Name ?? throw new ArgumentNullException(nameof(episodeDTO), "The value of 'episodeDTO.Name' should not be null"),
            aired: episodeDTO.Aired,
            duration: episodeDTO.Duration,
            animeID: animeID
        );
    }

    public static Episode UpdateFromDTO(this Episode episode, EpisodeDTO episodeDTO)
    {
        episode.Aired = episodeDTO.Aired;
        episode.Number = episodeDTO.Number;
        episode.Name = episodeDTO.Name ?? throw new ArgumentNullException(nameof(episodeDTO), "The value of 'episodeDTO.Name' should not be null");
        episode.Duration = episodeDTO.Duration;

        return episode;
    }
}

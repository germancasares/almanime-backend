using Almanime.Models;
using Almanime.Models.Documents;
using Almanime.Models.DTO;

namespace Almanime.Utils.Mappers;

public static class FansubMapper
{
  public static Fansub MapToModel(this FansubDTO fansubDTO)
  {
    var isWebpageAnUri = Uri.TryCreate(fansubDTO.Webpage, UriKind.Absolute, out var uriResult)
      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

    return fansubDTO.Webpage != null && !isWebpageAnUri
      ? throw new ArgumentNullException(nameof(fansubDTO), "The value of 'fansubDTO.Webpage' is not a valid Uri")
      : new(
        acronym: fansubDTO.Acronym ?? throw new ArgumentNullException(nameof(fansubDTO), "The value of 'fansubDTO.Acronym' should not be null"),
        name: fansubDTO.Name ?? throw new ArgumentNullException(nameof(fansubDTO), "The value of 'fansubDTO.Name' should not be null"),
        webpage: fansubDTO.Webpage
      );
  }

  public static FansubDocument MapToDocument(this Fansub fansub) => new()
  {
    ID = fansub.ID,
    CreationDate = fansub.CreationDate,
    Acronym = fansub.Acronym,
    Name = fansub.Name
  };
}

using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class FansubQueries
{
    public static Fansub GetByAcronym(this DbSet<Fansub> fansubs, string acronym)
    {
        var fansub = fansubs.SingleOrDefault(fansub => fansub.Acronym == acronym);
        return fansub ?? throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(fansub), new()
    {
      { nameof(acronym), acronym },
    });
    }
}

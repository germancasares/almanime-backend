using Almanime.Models;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class FansubQueries
{
    public static Fansub? GetByAcronym(this DbSet<Fansub> fansubs, string acronym) => fansubs.SingleOrDefault(fansub => fansub.Acronym == acronym);
}

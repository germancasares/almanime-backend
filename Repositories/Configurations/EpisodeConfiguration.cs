using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class EpisodeConfiguration : BaseModelConfiguration<Episode>
{
    public override void Configure(EntityTypeBuilder<Episode> builder)
    {
        base.Configure(builder);

        builder.Property(anime => anime.Number).IsRequired();
        builder.Property(anime => anime.AnimeID).IsRequired();

        builder
            .HasOne(c => c.Anime)
            .WithMany(c => c.Episodes)
            .HasForeignKey(c => c.AnimeID);
    }
}

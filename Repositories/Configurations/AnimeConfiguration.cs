using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class AnimeConfiguration : BaseModelConfiguration<Anime>
{
    public override void Configure(EntityTypeBuilder<Anime> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(i => i.KitsuID)
            .IsUnique();
    }
}

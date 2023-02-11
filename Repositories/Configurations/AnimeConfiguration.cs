using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class AnimeConfiguration : BaseModelConfiguration<Anime>
{
  public override void Configure(EntityTypeBuilder<Anime> builder)
  {
    base.Configure(builder);

    builder.Property(anime => anime.KitsuID).IsRequired();
    builder.Property(anime => anime.Slug).IsRequired();
    builder.Property(anime => anime.Name).IsRequired();
    builder.Property(anime => anime.Season).IsRequired();
    builder.Property(anime => anime.Status).IsRequired();
    builder.Property(anime => anime.StartDate).IsRequired();


    builder.HasIndex(anime => anime.KitsuID).IsUnique();
    builder.OwnsOne(anime => anime.CoverImages);
    builder.OwnsOne(anime => anime.PosterImages);
  }
}

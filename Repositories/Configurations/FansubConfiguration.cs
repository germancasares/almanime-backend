using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class FansubConfiguration : BaseModelConfiguration<Fansub>
{
  public override void Configure(EntityTypeBuilder<Fansub> builder)
  {
    base.Configure(builder);

    builder.Property(fansub => fansub.Name).IsRequired();
    builder.Property(fansub => fansub.Acronym).IsRequired();

    builder.HasIndex(fansub => fansub.Name).IsUnique();
    builder.HasIndex(fansub => fansub.Acronym).IsUnique();
  }
}

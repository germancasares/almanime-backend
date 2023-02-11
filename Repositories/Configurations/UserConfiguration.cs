using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class UserConfiguration : BaseModelConfiguration<User>
{
  public override void Configure(EntityTypeBuilder<User> builder)
  {
    base.Configure(builder);

    builder.Property(user => user.Auth0ID).IsRequired();
    builder.Property(user => user.Name).IsRequired();

    builder.HasIndex(user => user.Auth0ID).IsUnique();
    builder.HasIndex(user => user.Name).IsUnique();
  }
}
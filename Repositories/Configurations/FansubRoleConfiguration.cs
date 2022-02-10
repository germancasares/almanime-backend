using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class FansubRoleConfiguration : BaseModelConfiguration<FansubRole>
{
    public override void Configure(EntityTypeBuilder<FansubRole> builder)
    {
        base.Configure(builder);

        builder.HasKey(member => new { member.ID, member.FansubID });

        builder.Property(user => user.Name).IsRequired();
    }
}

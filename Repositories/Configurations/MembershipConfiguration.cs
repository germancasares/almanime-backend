using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Almanime.Repositories.Configurations;

public class MembershipConfiguration : BaseModelConfiguration<Membership>
{
    public override void Configure(EntityTypeBuilder<Membership> builder)
    {
        base.Configure(builder);

        builder.Property(member => member.ID).HasValueGenerator<GuidValueGenerator>();

        builder.HasOne(member => member.FansubRole)
            .WithMany(fansubRole => fansubRole.Memberships)
            .HasForeignKey(member => new { member.RoleID, member.FansubID });

        builder.HasIndex(p => new { p.UserID, p.FansubID }).IsUnique();
    }
}
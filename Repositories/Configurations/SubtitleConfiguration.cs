using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class SubtitleConfiguration : BaseModelConfiguration<Subtitle>
{
    public override void Configure(EntityTypeBuilder<Subtitle> builder)
    {
        base.Configure(builder);

        builder.HasKey(subtitle => new
        {
            subtitle.EpisodeID,
            subtitle.MembershipID
        });

        builder
          .HasOne(p => p.Membership)
          .WithMany(b => b.Subtitles)
          .HasForeignKey(p => p.MembershipID)
          .HasPrincipalKey(b => b.ID);

        builder
          .HasOne(p => p.Episode)
          .WithMany(b => b.Subtitles)
          .HasForeignKey(p => p.EpisodeID)
          .HasPrincipalKey(b => b.ID);
    }
}
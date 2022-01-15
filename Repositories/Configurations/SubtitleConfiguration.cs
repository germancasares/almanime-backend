using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class SubtitleConfiguration : BaseModelConfiguration<Subtitle>
{
    public override void Configure(EntityTypeBuilder<Subtitle> builder)
    {
        base.Configure(builder);

        builder.HasKey(subtitle => new { subtitle.EpisodeID, subtitle.MemberID });

        builder
            .HasOne(p => p.Member)
            .WithMany(b => b.Subtitles)
            .HasForeignKey(p => p.MemberID)
            .HasPrincipalKey(b => b.ID);

        builder
            .HasOne(p => p.Episode)
            .WithMany(b => b.Subtitles)
            .HasForeignKey(p => p.EpisodeID)
            .HasPrincipalKey(b => b.ID);
    }
}
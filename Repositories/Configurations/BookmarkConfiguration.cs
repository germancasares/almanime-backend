using Almanime.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Almanime.Repositories.Configurations;

public class BookmarkConfiguration : BaseModelConfiguration<Bookmark>
{
    public override void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        base.Configure(builder);

        builder.Property(bookmark => bookmark.ID).HasValueGenerator<GuidValueGenerator>();

        builder.HasKey(bookmark => new { bookmark.AnimeID, bookmark.UserID });

        builder.HasOne(p => p.User).WithMany(b => b.Bookmarks).HasForeignKey(p => p.UserID).HasPrincipalKey(b => b.ID);
    }
}
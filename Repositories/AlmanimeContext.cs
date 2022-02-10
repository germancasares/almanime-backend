using Almanime.Models;
using Almanime.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories;

public partial class AlmanimeContext : DbContext
{
    public AlmanimeContext()
    {
    }

    public AlmanimeContext(DbContextOptions<AlmanimeContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnimeConfiguration());
        modelBuilder.ApplyConfiguration(new EpisodeConfiguration());
        modelBuilder.ApplyConfiguration(new SubtitleConfiguration());

        modelBuilder.ApplyConfiguration(new FansubConfiguration());
        modelBuilder.ApplyConfiguration(new FansubRoleConfiguration());
        modelBuilder.ApplyConfiguration(new MembershipConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new BookmarkConfiguration());
    }

    public DbSet<Anime> Animes => Set<Anime>();
    public DbSet<Episode> Episodes => Set<Episode>();
    public DbSet<Subtitle> Subtitles => Set<Subtitle>();

    public DbSet<Fansub> Fansubs => Set<Fansub>();
    public DbSet<FansubRole> FansubRoles => Set<FansubRole>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
}

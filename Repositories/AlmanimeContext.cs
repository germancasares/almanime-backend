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
    }

    public DbSet<Anime> Animes => Set<Anime>();
    public DbSet<Episode> Episodes => Set<Episode>();
}

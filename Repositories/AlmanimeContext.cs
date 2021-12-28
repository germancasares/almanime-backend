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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //{
        //    optionsBuilder.UseSqlServer("Name=ConnectionStrings:Almanime");
        //}
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.ApplyConfiguration(new AnimeConfiguration());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<Anime> Animes => Set<Anime>();
}

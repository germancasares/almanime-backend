using Almanime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class BaseModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Base
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(c => c.ID);

        builder
            .Property(c => c.ID)
            .ValueGeneratedOnAdd();

        builder
            .Property(c => c.CreationDate)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");
    }
}

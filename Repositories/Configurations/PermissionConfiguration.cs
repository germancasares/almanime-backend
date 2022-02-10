using Almanime.Models;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Almanime.Repositories.Configurations;

public class PermissionConfiguration : BaseModelConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);

        builder.HasData(
            new Permission
            {
                ID = new Guid("3332C912-2E46-48EE-86E8-8299DCF1127F"),
                Grant = EPermission.CreateSubtitle,
            }
        );
    }
}
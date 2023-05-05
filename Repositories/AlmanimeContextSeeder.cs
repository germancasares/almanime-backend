using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories;
    
public static class AlmanimeContextSeeder
{
  public static void Seed(ModelBuilder builder)
  {
    var draftSubtitlePermission = new Permission
    {
      ID = new Guid("3332C912-2E46-48EE-86E8-8299DCF1127F"),
      Grant = EPermission.DraftSubtitle,
    };
    var publishSubtitlePermission = new Permission
    {
      ID = new Guid("510F97BB-172C-4189-A31B-D7E39BD1CE71"),
      Grant = EPermission.PublishSubtitle,
    };
    var unpublishSubtitlePermission = new Permission
    {
      ID = new Guid("E8A5F5ED-DEF3-4140-A226-5D93DFC9ED15"),
      Grant = EPermission.UnpublishSubtitle,
    };
    var editPermissionsPermission = new Permission
    {
      ID = new Guid("C7D17F4C-57CA-4B3A-8029-EF14CBB5AAF0"),
      Grant = EPermission.EditPermissions,
    };

    builder.Entity<Permission>().HasData(
      draftSubtitlePermission,
      publishSubtitlePermission,
      unpublishSubtitlePermission,
      editPermissionsPermission
    );
  }
}
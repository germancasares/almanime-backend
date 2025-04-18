using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories;

public static class AlmanimeContextSeeder
{
    public static readonly Permission DRAFT_SUBTITLE_PERMISSION = new()
    {
        ID = new Guid("3332C912-2E46-48EE-86E8-8299DCF1127F"),
        Grant = EPermission.DraftSubtitle,
    };

    public static readonly Permission PUBLISH_SUBTITLE_PERMISSION = new()
    {
        ID = new Guid("510F97BB-172C-4189-A31B-D7E39BD1CE71"),
        Grant = EPermission.PublishSubtitle,
    };

    public static readonly Permission UNPUBLISH_SUBTITLE_PERMISSION = new()
    {
        ID = new Guid("E8A5F5ED-DEF3-4140-A226-5D93DFC9ED15"),
        Grant = EPermission.UnpublishSubtitle,
    };

    public static readonly Permission DELETE_SUBTITLE_PERMISSION = new()
    {
        ID = new Guid("D77DB126-4D92-41C0-B65F-7D76309596F1"),
        Grant = EPermission.DeleteSubtitle,
    };

    public static readonly Permission EDIT_PERMISSIONS_PERMISSION = new()
    {
        ID = new Guid("C7D17F4C-57CA-4B3A-8029-EF14CBB5AAF0"),
        Grant = EPermission.EditPermissions,
    };

    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<Permission>().HasData(
          DRAFT_SUBTITLE_PERMISSION,
          PUBLISH_SUBTITLE_PERMISSION,
          UNPUBLISH_SUBTITLE_PERMISSION,
          DELETE_SUBTITLE_PERMISSION,
          EDIT_PERMISSIONS_PERMISSION
        );

        var serosacUser = new User("google-oauth2|114846925867300920237", "Serosac")
        {
            ID = new Guid("110CA42F-C97E-4007-7F09-08DB44647523"),
        };
        builder.Entity<User>().HasData(serosacUser);

        var netflixFansub = new Fansub("NFLX", "Netflix", "https://www.netflix.com")
        {
            ID = new Guid("69D1F290-80F4-48CB-8C19-90195EA7BF4A"),
            CreationDate = new DateTime(1997, 8, 29),
        };
        builder.Entity<Fansub>().HasData(netflixFansub);

        var adminRoleForNetflix = new FansubRole("Admin", netflixFansub.ID, new List<Permission>())
        {
            ID = new Guid("2D2F1B59-F44F-44F9-B3A9-A8700606FE84"),
        };

        builder.Entity<FansubRole>().HasData(adminRoleForNetflix);
        builder.SharedTypeEntity<Dictionary<string, object>>("FansubRolePermission").HasData(
          new
          {
              PermissionsID = DRAFT_SUBTITLE_PERMISSION.ID,
              FansubRolesID = adminRoleForNetflix.ID,
              FansubRolesFansubID = adminRoleForNetflix.FansubID,
          },
          new
          {
              PermissionsID = PUBLISH_SUBTITLE_PERMISSION.ID,
              FansubRolesID = adminRoleForNetflix.ID,
              FansubRolesFansubID = adminRoleForNetflix.FansubID,
          },
          new
          {
              PermissionsID = UNPUBLISH_SUBTITLE_PERMISSION.ID,
              FansubRolesID = adminRoleForNetflix.ID,
              FansubRolesFansubID = adminRoleForNetflix.FansubID,
          },
          new
          {
              PermissionsID = DELETE_SUBTITLE_PERMISSION.ID,
              FansubRolesID = adminRoleForNetflix.ID,
              FansubRolesFansubID = adminRoleForNetflix.FansubID,
          },
          new
          {
              PermissionsID = EDIT_PERMISSIONS_PERMISSION.ID,
              FansubRolesID = adminRoleForNetflix.ID,
              FansubRolesFansubID = adminRoleForNetflix.FansubID,
          }
        );

        var serosacNetflixMembership = new Membership
        {
            ID = new Guid("E196CF16-D5B5-4D2A-A653-E2B157403254"),
            UserID = serosacUser.ID,
            FansubID = netflixFansub.ID,
            RoleID = adminRoleForNetflix.ID,
        };
        builder.Entity<Membership>().HasData(serosacNetflixMembership);
    }
}
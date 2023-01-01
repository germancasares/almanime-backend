using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FansubRolePermission",
                columns: table => new {
                    PermissionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FansubRolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FansubRolesFansubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FansubRolePermission", x => new { x.PermissionsID, x.FansubRolesID, x.FansubRolesFansubID });
                    table.ForeignKey(
                        name: "FK_FansubRolePermission_FansubRoles_FansubRolesID_FansubRolesFansubID",
                        columns: x => new { x.FansubRolesID, x.FansubRolesFansubID },
                        principalTable: "FansubRoles",
                        principalColumns: new[] { "ID", "FansubID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FansubRolePermission_Permission_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FansubRolePermission_FansubRolesID_FansubRolesFansubID",
                table: "FansubRolePermission",
                columns: new[] { "FansubRolesID", "FansubRolesFansubID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FansubRolePermission");

            migrationBuilder.DropTable(
                name: "Permission");
        }
    }
}

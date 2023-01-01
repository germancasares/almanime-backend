using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Almanime.Migrations
{
    public partial class FansubRolePKBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FansubRolePermission_FansubRoles_FansubRolesID",
                table: "FansubRolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_FansubRoles_FansubID",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_FansubID",
                table: "Memberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FansubRoles",
                table: "FansubRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FansubRolePermission",
                table: "FansubRolePermission");

            migrationBuilder.DropIndex(
                name: "IX_FansubRolePermission_PermissionsID",
                table: "FansubRolePermission");

            migrationBuilder.AddColumn<Guid>(
                name: "FansubRolesFansubID",
                table: "FansubRolePermission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FansubRoles",
                table: "FansubRoles",
                columns: new[] { "ID", "FansubID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FansubRolePermission",
                table: "FansubRolePermission",
                columns: new[] { "PermissionsID", "FansubRolesID", "FansubRolesFansubID" });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_RoleID_FansubID",
                table: "Memberships",
                columns: new[] { "RoleID", "FansubID" });

            migrationBuilder.CreateIndex(
                name: "IX_FansubRolePermission_FansubRolesID_FansubRolesFansubID",
                table: "FansubRolePermission",
                columns: new[] { "FansubRolesID", "FansubRolesFansubID" });

            migrationBuilder.AddForeignKey(
                name: "FK_FansubRolePermission_FansubRoles_FansubRolesID_FansubRolesFansubID",
                table: "FansubRolePermission",
                columns: new[] { "FansubRolesID", "FansubRolesFansubID" },
                principalTable: "FansubRoles",
                principalColumns: new[] { "ID", "FansubID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_FansubRoles_RoleID_FansubID",
                table: "Memberships",
                columns: new[] { "RoleID", "FansubID" },
                principalTable: "FansubRoles",
                principalColumns: new[] { "ID", "FansubID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FansubRolePermission_FansubRoles_FansubRolesID_FansubRolesFansubID",
                table: "FansubRolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_FansubRoles_RoleID_FansubID",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_RoleID_FansubID",
                table: "Memberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FansubRoles",
                table: "FansubRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FansubRolePermission",
                table: "FansubRolePermission");

            migrationBuilder.DropIndex(
                name: "IX_FansubRolePermission_FansubRolesID_FansubRolesFansubID",
                table: "FansubRolePermission");

            migrationBuilder.DropColumn(
                name: "FansubRolesFansubID",
                table: "FansubRolePermission");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FansubRoles",
                table: "FansubRoles",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FansubRolePermission",
                table: "FansubRolePermission",
                columns: new[] { "FansubRolesID", "PermissionsID" });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_FansubID",
                table: "Memberships",
                column: "FansubID");

            migrationBuilder.CreateIndex(
                name: "IX_FansubRolePermission_PermissionsID",
                table: "FansubRolePermission",
                column: "PermissionsID");

            migrationBuilder.AddForeignKey(
                name: "FK_FansubRolePermission_FansubRoles_FansubRolesID",
                table: "FansubRolePermission",
                column: "FansubRolesID",
                principalTable: "FansubRoles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_FansubRoles_FansubID",
                table: "Memberships",
                column: "FansubID",
                principalTable: "FansubRoles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

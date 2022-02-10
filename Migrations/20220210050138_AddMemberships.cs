using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddMemberships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Membership_FansubRoles_FansubRoleID_FansubRoleFansubID",
                table: "Membership");

            migrationBuilder.DropForeignKey(
                name: "FK_Membership_Users_UserID",
                table: "Membership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Membership",
                table: "Membership");

            migrationBuilder.DropIndex(
                name: "IX_Membership_FansubRoleID_FansubRoleFansubID",
                table: "Membership");

            migrationBuilder.DropColumn(
                name: "FansubRoleFansubID",
                table: "Membership");

            migrationBuilder.DropColumn(
                name: "FansubRoleID",
                table: "Membership");

            migrationBuilder.RenameTable(
                name: "Membership",
                newName: "Memberships");

            migrationBuilder.RenameIndex(
                name: "IX_Membership_UserID",
                table: "Memberships",
                newName: "IX_Memberships_UserID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Memberships",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_RoleID_FansubID",
                table: "Memberships",
                columns: new[] { "RoleID", "FansubID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_FansubRoles_RoleID_FansubID",
                table: "Memberships",
                columns: new[] { "RoleID", "FansubID" },
                principalTable: "FansubRoles",
                principalColumns: new[] { "ID", "FansubID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Users_UserID",
                table: "Memberships",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_FansubRoles_RoleID_FansubID",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Users_UserID",
                table: "Memberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_RoleID_FansubID",
                table: "Memberships");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "Membership");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_UserID",
                table: "Membership",
                newName: "IX_Membership_UserID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Membership",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "FansubRoleFansubID",
                table: "Membership",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FansubRoleID",
                table: "Membership",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Membership",
                table: "Membership",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_FansubRoleID_FansubRoleFansubID",
                table: "Membership",
                columns: new[] { "FansubRoleID", "FansubRoleFansubID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_FansubRoles_FansubRoleID_FansubRoleFansubID",
                table: "Membership",
                columns: new[] { "FansubRoleID", "FansubRoleFansubID" },
                principalTable: "FansubRoles",
                principalColumns: new[] { "ID", "FansubID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_Users_UserID",
                table: "Membership",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddFansubRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FansubRole_Fansubs_FansubID",
                table: "FansubRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Membership_FansubRole_FansubRoleID",
                table: "Membership");

            migrationBuilder.DropIndex(
                name: "IX_Membership_FansubRoleID",
                table: "Membership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FansubRole",
                table: "FansubRole");

            migrationBuilder.RenameTable(
                name: "FansubRole",
                newName: "FansubRoles");

            migrationBuilder.RenameIndex(
                name: "IX_FansubRole_FansubID",
                table: "FansubRoles",
                newName: "IX_FansubRoles_FansubID");

            migrationBuilder.AddColumn<Guid>(
                name: "FansubRoleFansubID",
                table: "Membership",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "FansubRoles",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FansubRoles",
                table: "FansubRoles",
                columns: new[] { "ID", "FansubID" });

            migrationBuilder.CreateIndex(
                name: "IX_Membership_FansubRoleID_FansubRoleFansubID",
                table: "Membership",
                columns: new[] { "FansubRoleID", "FansubRoleFansubID" });

            migrationBuilder.AddForeignKey(
                name: "FK_FansubRoles_Fansubs_FansubID",
                table: "FansubRoles",
                column: "FansubID",
                principalTable: "Fansubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_FansubRoles_FansubRoleID_FansubRoleFansubID",
                table: "Membership",
                columns: new[] { "FansubRoleID", "FansubRoleFansubID" },
                principalTable: "FansubRoles",
                principalColumns: new[] { "ID", "FansubID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FansubRoles_Fansubs_FansubID",
                table: "FansubRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Membership_FansubRoles_FansubRoleID_FansubRoleFansubID",
                table: "Membership");

            migrationBuilder.DropIndex(
                name: "IX_Membership_FansubRoleID_FansubRoleFansubID",
                table: "Membership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FansubRoles",
                table: "FansubRoles");

            migrationBuilder.DropColumn(
                name: "FansubRoleFansubID",
                table: "Membership");

            migrationBuilder.RenameTable(
                name: "FansubRoles",
                newName: "FansubRole");

            migrationBuilder.RenameIndex(
                name: "IX_FansubRoles_FansubID",
                table: "FansubRole",
                newName: "IX_FansubRole_FansubID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "FansubRole",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FansubRole",
                table: "FansubRole",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_FansubRoleID",
                table: "Membership",
                column: "FansubRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_FansubRole_Fansubs_FansubID",
                table: "FansubRole",
                column: "FansubID",
                principalTable: "Fansubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membership_FansubRole_FansubRoleID",
                table: "Membership",
                column: "FansubRoleID",
                principalTable: "FansubRole",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

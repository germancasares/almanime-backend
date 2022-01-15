using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddBaseToSubtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Members_MemberID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles");

            migrationBuilder.DropIndex(
                name: "IX_Subtitles_MemberID",
                table: "Subtitles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Members_ID",
                table: "Members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Subtitles",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberFansubID",
                table: "Subtitles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MemberUserID",
                table: "Subtitles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_EpisodeID",
                table: "Subtitles",
                column: "EpisodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_MemberFansubID_MemberUserID",
                table: "Subtitles",
                columns: new[] { "MemberFansubID", "MemberUserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Members_MemberFansubID_MemberUserID",
                table: "Subtitles",
                columns: new[] { "MemberFansubID", "MemberUserID" },
                principalTable: "Members",
                principalColumns: new[] { "FansubID", "UserID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Members_MemberFansubID_MemberUserID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles");

            migrationBuilder.DropIndex(
                name: "IX_Subtitles_EpisodeID",
                table: "Subtitles");

            migrationBuilder.DropIndex(
                name: "IX_Subtitles_MemberFansubID_MemberUserID",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "MemberFansubID",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "MemberUserID",
                table: "Subtitles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Subtitles",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles",
                columns: new[] { "EpisodeID", "MemberID" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Members_ID",
                table: "Members",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_MemberID",
                table: "Subtitles",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Members_MemberID",
                table: "Subtitles",
                column: "MemberID",
                principalTable: "Members",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

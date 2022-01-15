using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class SubtitleMemberFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Members_MemberFansubID_MemberUserID",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Members_MemberID",
                table: "Subtitles");

            migrationBuilder.DropIndex(
                name: "IX_Subtitles_MemberID",
                table: "Subtitles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Members_ID",
                table: "Members");

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
    }
}

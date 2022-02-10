using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddSubtitleMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles");

            migrationBuilder.AddColumn<Guid>(
                name: "MembershipID",
                table: "Subtitles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles",
                columns: new[] { "EpisodeID", "MembershipID" });

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_MembershipID",
                table: "Subtitles",
                column: "MembershipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Memberships_MembershipID",
                table: "Subtitles",
                column: "MembershipID",
                principalTable: "Memberships",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Memberships_MembershipID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles");

            migrationBuilder.DropIndex(
                name: "IX_Subtitles_MembershipID",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "MembershipID",
                table: "Subtitles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles",
                column: "EpisodeID");
        }
    }
}

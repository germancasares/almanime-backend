using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddSubtitles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitle_Episodes_EpisodeID",
                table: "Subtitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtitle",
                table: "Subtitle");

            migrationBuilder.DropIndex(
                name: "IX_Subtitle_EpisodeID",
                table: "Subtitle");

            migrationBuilder.RenameTable(
                name: "Subtitle",
                newName: "Subtitles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Subtitles",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles",
                column: "EpisodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitles_Episodes_EpisodeID",
                table: "Subtitles",
                column: "EpisodeID",
                principalTable: "Episodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtitles_Episodes_EpisodeID",
                table: "Subtitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtitles",
                table: "Subtitles");

            migrationBuilder.RenameTable(
                name: "Subtitles",
                newName: "Subtitle");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Subtitle",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtitle",
                table: "Subtitle",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Subtitle_EpisodeID",
                table: "Subtitle",
                column: "EpisodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitle_Episodes_EpisodeID",
                table: "Subtitle",
                column: "EpisodeID",
                principalTable: "Episodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

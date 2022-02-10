using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddEpisodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Animes_AnimeID",
                table: "Episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtitle_Episode_EpisodeID",
                table: "Subtitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode",
                table: "Episode");

            migrationBuilder.RenameTable(
                name: "Episode",
                newName: "Episodes");

            migrationBuilder.RenameIndex(
                name: "IX_Episode_AnimeID",
                table: "Episodes",
                newName: "IX_Episodes_AnimeID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Episodes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Animes_AnimeID",
                table: "Episodes",
                column: "AnimeID",
                principalTable: "Animes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitle_Episodes_EpisodeID",
                table: "Subtitle",
                column: "EpisodeID",
                principalTable: "Episodes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Animes_AnimeID",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtitle_Episodes_EpisodeID",
                table: "Subtitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes");

            migrationBuilder.RenameTable(
                name: "Episodes",
                newName: "Episode");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_AnimeID",
                table: "Episode",
                newName: "IX_Episode_AnimeID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Episode",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode",
                table: "Episode",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Animes_AnimeID",
                table: "Episode",
                column: "AnimeID",
                principalTable: "Animes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subtitle_Episode_EpisodeID",
                table: "Subtitle",
                column: "EpisodeID",
                principalTable: "Episode",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

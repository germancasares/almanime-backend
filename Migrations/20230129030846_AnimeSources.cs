using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class AnimeSources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AniDBID",
                table: "Animes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AniListID",
                table: "Animes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyAnimeListID",
                table: "Animes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AniDBID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "AniListID",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "MyAnimeListID",
                table: "Animes");
        }
    }
}

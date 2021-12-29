using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations;

public partial class AddAnimeImages : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "PosterImageUrl",
            table: "Animes",
            newName: "PosterImages_Tiny");

        migrationBuilder.RenameColumn(
            name: "CoverImageUrl",
            table: "Animes",
            newName: "PosterImages_Small");

        migrationBuilder.AddColumn<string>(
            name: "CoverImages_Original",
            table: "Animes",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "CoverImages_Small",
            table: "Animes",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "CoverImages_Tiny",
            table: "Animes",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "PosterImages_Original",
            table: "Animes",
            type: "nvarchar(max)",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CoverImages_Original",
            table: "Animes");

        migrationBuilder.DropColumn(
            name: "CoverImages_Small",
            table: "Animes");

        migrationBuilder.DropColumn(
            name: "CoverImages_Tiny",
            table: "Animes");

        migrationBuilder.DropColumn(
            name: "PosterImages_Original",
            table: "Animes");

        migrationBuilder.RenameColumn(
            name: "PosterImages_Tiny",
            table: "Animes",
            newName: "PosterImageUrl");

        migrationBuilder.RenameColumn(
            name: "PosterImages_Small",
            table: "Animes",
            newName: "CoverImageUrl");
    }
}

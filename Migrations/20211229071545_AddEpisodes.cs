using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations;

public partial class AddEpisodes : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Episodes",
            columns: table => new {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Number = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Aired = table.Column<DateTime>(type: "datetime2", nullable: true),
                Duration = table.Column<int>(type: "int", nullable: true),
                AnimeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Episodes", x => x.ID);
                table.ForeignKey(
                    name: "FK_Episodes_Animes_AnimeID",
                    column: x => x.AnimeID,
                    principalTable: "Animes",
                    principalColumn: "ID",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Episodes_AnimeID",
            table: "Episodes",
            column: "AnimeID");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Episodes");
    }
}

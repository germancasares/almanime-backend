using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddFansubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fansubs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Webpage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fansubs", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fansubs_Acronym",
                table: "Fansubs",
                column: "Acronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fansubs_Name",
                table: "Fansubs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fansubs");
        }
    }
}

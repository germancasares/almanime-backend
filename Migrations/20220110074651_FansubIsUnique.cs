using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class FansubIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Fansubs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Acronym",
                table: "Fansubs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
            migrationBuilder.DropIndex(
                name: "IX_Fansubs_Acronym",
                table: "Fansubs");

            migrationBuilder.DropIndex(
                name: "IX_Fansubs_Name",
                table: "Fansubs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Fansubs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Acronym",
                table: "Fansubs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

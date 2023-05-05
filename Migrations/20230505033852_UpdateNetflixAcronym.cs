using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNetflixAcronym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fansubs",
                keyColumn: "ID",
                keyValue: new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"),
                column: "Acronym",
                value: "NFLX");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fansubs",
                keyColumn: "ID",
                keyValue: new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"),
                column: "Acronym",
                value: "netflix");
        }
    }
}

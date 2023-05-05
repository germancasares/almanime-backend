using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNetflixCreationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fansubs",
                keyColumn: "ID",
                keyValue: new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"),
                column: "CreationDate",
                value: new DateTime(1997, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fansubs",
                keyColumn: "ID",
                keyValue: new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"),
                column: "CreationDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class AddPublishPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"),
                column: "Grant",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("c7d17f4c-57ca-4b3a-8029-ef14cbb5aaf0"),
                column: "Grant",
                value: 20);

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Grant", "ModificationDate" },
                values: new object[] { new Guid("510f97bb-172c-4189-a31b-d7e39bd1ce71"), 11, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("510f97bb-172c-4189-a31b-d7e39bd1ce71"));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"),
                column: "Grant",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("c7d17f4c-57ca-4b3a-8029-ef14cbb5aaf0"),
                column: "Grant",
                value: 0);
        }
    }
}

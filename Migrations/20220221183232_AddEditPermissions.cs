using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddEditPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"),
                column: "Grant",
                value: 1);

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Grant", "ModificationDate" },
                values: new object[] { new Guid("c7d17f4c-57ca-4b3a-8029-ef14cbb5aaf0"), 0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("c7d17f4c-57ca-4b3a-8029-ef14cbb5aaf0"));

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"),
                column: "Grant",
                value: 0);
        }
    }
}

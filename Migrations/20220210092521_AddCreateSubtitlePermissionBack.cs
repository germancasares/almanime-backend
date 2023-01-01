using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddCreateSubtitlePermissionBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Grant", "ModificationDate" },
                values: new object[] { new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"), 0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"));
        }
    }
}

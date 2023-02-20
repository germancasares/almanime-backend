using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class AddUnpublishPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Grant", "ModificationDate" },
                values: new object[] { new Guid("e8a5f5ed-def3-4140-a226-5d93dfc9ed15"), 12, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("e8a5f5ed-def3-4140-a226-5d93dfc9ed15"));
        }
    }
}

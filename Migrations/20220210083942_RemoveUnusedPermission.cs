using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class RemoveUnusedPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"));

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Permission",
                newName: "Grant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grant",
                table: "Permission",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "CreationDate", "ModificationDate", "Name" },
                values: new object[] { new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0 });
        }
    }
}

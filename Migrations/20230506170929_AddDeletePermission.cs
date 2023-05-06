using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Grant", "ModificationDate" },
                values: new object[] { new Guid("d77db126-4d92-41c0-b65f-7d76309596f1"), 13, null });

            migrationBuilder.InsertData(
                table: "FansubRolePermission",
                columns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                values: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("d77db126-4d92-41c0-b65f-7d76309596f1") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FansubRolePermission",
                keyColumns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                keyValues: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("d77db126-4d92-41c0-b65f-7d76309596f1") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "ID",
                keyValue: new Guid("d77db126-4d92-41c0-b65f-7d76309596f1"));
        }
    }
}

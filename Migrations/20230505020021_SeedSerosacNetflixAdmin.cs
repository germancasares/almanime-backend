using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class SeedSerosacNetflixAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Fansubs",
                columns: new[] { "ID", "Acronym", "ModificationDate", "Name", "Webpage" },
                values: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), "netflix", null, "Netflix", "https://www.netflix.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Auth0ID", "ModificationDate", "Name" },
                values: new object[] { new Guid("110ca42f-c97e-4007-7f09-08db44647523"), "google-oauth2|114846925867300920237", null, "Serosac" });

            migrationBuilder.InsertData(
                table: "FansubRoles",
                columns: new[] { "FansubID", "ID", "ModificationDate", "Name" },
                values: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "FansubRolePermission",
                columns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                values: new object[,]
                {
                    { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f") },
                    { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("510f97bb-172c-4189-a31b-d7e39bd1ce71") },
                    { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("c7d17f4c-57ca-4b3a-8029-ef14cbb5aaf0") },
                    { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("e8a5f5ed-def3-4140-a226-5d93dfc9ed15") }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "ID", "FansubID", "ModificationDate", "RoleID", "UserID" },
                values: new object[] { new Guid("e196cf16-d5b5-4d2a-a653-e2b157403254"), new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), null, new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("110ca42f-c97e-4007-7f09-08db44647523") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FansubRolePermission",
                keyColumns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                keyValues: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("3332c912-2e46-48ee-86e8-8299dcf1127f") });

            migrationBuilder.DeleteData(
                table: "FansubRolePermission",
                keyColumns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                keyValues: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("510f97bb-172c-4189-a31b-d7e39bd1ce71") });

            migrationBuilder.DeleteData(
                table: "FansubRolePermission",
                keyColumns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                keyValues: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("c7d17f4c-57ca-4b3a-8029-ef14cbb5aaf0") });

            migrationBuilder.DeleteData(
                table: "FansubRolePermission",
                keyColumns: new[] { "FansubRolesFansubID", "FansubRolesID", "PermissionsID" },
                keyValues: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84"), new Guid("e8a5f5ed-def3-4140-a226-5d93dfc9ed15") });

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "ID",
                keyValue: new Guid("e196cf16-d5b5-4d2a-a653-e2b157403254"));

            migrationBuilder.DeleteData(
                table: "FansubRoles",
                keyColumns: new[] { "FansubID", "ID" },
                keyValues: new object[] { new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"), new Guid("2d2f1b59-f44f-44f9-b3a9-a8700606fe84") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("110ca42f-c97e-4007-7f09-08db44647523"));

            migrationBuilder.DeleteData(
                table: "Fansubs",
                keyColumn: "ID",
                keyValue: new Guid("69d1f290-80f4-48cb-8c19-90195ea7bf4a"));
        }
    }
}

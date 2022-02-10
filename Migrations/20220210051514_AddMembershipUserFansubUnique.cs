using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddMembershipUserFansubUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memberships_UserID",
                table: "Memberships");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserID_FansubID",
                table: "Memberships",
                columns: new[] { "UserID", "FansubID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Memberships_UserID_FansubID",
                table: "Memberships");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserID",
                table: "Memberships",
                column: "UserID");
        }
    }
}

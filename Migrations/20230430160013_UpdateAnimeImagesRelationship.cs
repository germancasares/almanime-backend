using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimeImagesRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Memberships_FansubID",
                table: "Memberships",
                column: "FansubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Fansubs_FansubID",
                table: "Memberships",
                column: "FansubID",
                principalTable: "Fansubs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Fansubs_FansubID",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_FansubID",
                table: "Memberships");
        }
    }
}

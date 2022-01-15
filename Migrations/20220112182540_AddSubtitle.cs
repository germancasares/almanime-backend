using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Almanime.Migrations
{
    public partial class AddSubtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subtitles",
                columns: table => new
                {
                    EpisodeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberFansubID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitles", x => new { x.EpisodeID, x.MemberID });
                    table.ForeignKey(
                        name: "FK_Subtitles_Episodes_EpisodeID",
                        column: x => x.EpisodeID,
                        principalTable: "Episodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subtitles_Members_MemberFansubID_MemberUserID",
                        columns: x => new { x.MemberFansubID, x.MemberUserID },
                        principalTable: "Members",
                        principalColumns: new[] { "FansubID", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_MemberFansubID_MemberUserID",
                table: "Subtitles",
                columns: new[] { "MemberFansubID", "MemberUserID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subtitles");
        }
    }
}

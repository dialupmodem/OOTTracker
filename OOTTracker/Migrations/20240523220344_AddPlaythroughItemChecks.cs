using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaythroughItemChecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaythroughItemChecks",
                columns: table => new
                {
                    PlaythroughItemCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaythroughId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Spoiler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obtained = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaythroughItemChecks", x => x.PlaythroughItemCheckId);
                    table.ForeignKey(
                        name: "FK_PlaythroughItemChecks_ItemChecks_ItemCheckId",
                        column: x => x.ItemCheckId,
                        principalTable: "ItemChecks",
                        principalColumn: "ItemCheckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaythroughItemChecks_Playthroughs_PlaythroughId",
                        column: x => x.PlaythroughId,
                        principalTable: "Playthroughs",
                        principalColumn: "PlaythroughId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaythroughItemChecks_ItemCheckId",
                table: "PlaythroughItemChecks",
                column: "ItemCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaythroughItemChecks_PlaythroughId",
                table: "PlaythroughItemChecks",
                column: "PlaythroughId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaythroughItemChecks");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddSpoilerMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpoilerMappings",
                columns: table => new
                {
                    SpoilerMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpoilerMappings", x => x.SpoilerMappingId);
                    table.ForeignKey(
                        name: "FK_SpoilerMappings_ItemChecks_ItemCheckId",
                        column: x => x.ItemCheckId,
                        principalTable: "ItemChecks",
                        principalColumn: "ItemCheckId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpoilerMappings_ItemCheckId",
                table: "SpoilerMappings",
                column: "ItemCheckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpoilerMappings");
        }
    }
}

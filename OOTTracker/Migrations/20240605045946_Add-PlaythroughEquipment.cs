using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaythroughEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaythroughEquipment",
                columns: table => new
                {
                    PlaythroughEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlaythroughId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Obtained = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaythroughEquipment", x => x.PlaythroughEquipmentId);
                    table.ForeignKey(
                        name: "FK_PlaythroughEquipment_InventoryEquipment_InventoryEquipmentId",
                        column: x => x.InventoryEquipmentId,
                        principalTable: "InventoryEquipment",
                        principalColumn: "InventoryEquipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaythroughEquipment_Playthroughs_PlaythroughId",
                        column: x => x.PlaythroughId,
                        principalTable: "Playthroughs",
                        principalColumn: "PlaythroughId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaythroughEquipment_InventoryEquipmentId",
                table: "PlaythroughEquipment",
                column: "InventoryEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaythroughEquipment_PlaythroughId",
                table: "PlaythroughEquipment",
                column: "PlaythroughId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaythroughEquipment");
        }
    }
}

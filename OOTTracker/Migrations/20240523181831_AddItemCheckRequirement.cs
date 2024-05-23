using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddItemCheckRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemCheckRequirements",
                columns: table => new
                {
                    ItemCheckRequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InventoryEquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCheckRequirements", x => x.ItemCheckRequirementId);
                    table.ForeignKey(
                        name: "FK_ItemCheckRequirements_InventoryEquipment_InventoryEquipmentId",
                        column: x => x.InventoryEquipmentId,
                        principalTable: "InventoryEquipment",
                        principalColumn: "InventoryEquipmentId");
                    table.ForeignKey(
                        name: "FK_ItemCheckRequirements_ItemChecks_ItemCheckId",
                        column: x => x.ItemCheckId,
                        principalTable: "ItemChecks",
                        principalColumn: "ItemCheckId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCheckRequirements_InventoryEquipmentId",
                table: "ItemCheckRequirements",
                column: "InventoryEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCheckRequirements_ItemCheckId",
                table: "ItemCheckRequirements",
                column: "ItemCheckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCheckRequirements");
        }
    }
}

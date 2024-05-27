using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class FKTweaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectables_CollectableTypes_CollectableTypeId",
                table: "Collectables");

            migrationBuilder.DropForeignKey(
                name: "FK_Collectables_Locations_LocationId",
                table: "Collectables");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCheckRequirements_InventoryEquipment_InventoryEquipmentId",
                table: "ItemCheckRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCheckRequirements_ItemChecks_ItemCheckId",
                table: "ItemCheckRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemChecks_ItemAgeRequirements_ItemAgeRequirementId",
                table: "ItemChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemChecks_ItemCheckTypes_ItemCheckTypeId",
                table: "ItemChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemChecks_Locations_LocationId",
                table: "ItemChecks");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectables_CollectableTypes_CollectableTypeId",
                table: "Collectables",
                column: "CollectableTypeId",
                principalTable: "CollectableTypes",
                principalColumn: "CollectableTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collectables_Locations_LocationId",
                table: "Collectables",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCheckRequirements_InventoryEquipment_InventoryEquipmentId",
                table: "ItemCheckRequirements",
                column: "InventoryEquipmentId",
                principalTable: "InventoryEquipment",
                principalColumn: "InventoryEquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCheckRequirements_ItemChecks_ItemCheckId",
                table: "ItemCheckRequirements",
                column: "ItemCheckId",
                principalTable: "ItemChecks",
                principalColumn: "ItemCheckId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemChecks_ItemAgeRequirements_ItemAgeRequirementId",
                table: "ItemChecks",
                column: "ItemAgeRequirementId",
                principalTable: "ItemAgeRequirements",
                principalColumn: "ItemAgeRequirementId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemChecks_ItemCheckTypes_ItemCheckTypeId",
                table: "ItemChecks",
                column: "ItemCheckTypeId",
                principalTable: "ItemCheckTypes",
                principalColumn: "ItemCheckTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemChecks_Locations_LocationId",
                table: "ItemChecks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectables_CollectableTypes_CollectableTypeId",
                table: "Collectables");

            migrationBuilder.DropForeignKey(
                name: "FK_Collectables_Locations_LocationId",
                table: "Collectables");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCheckRequirements_InventoryEquipment_InventoryEquipmentId",
                table: "ItemCheckRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCheckRequirements_ItemChecks_ItemCheckId",
                table: "ItemCheckRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemChecks_ItemAgeRequirements_ItemAgeRequirementId",
                table: "ItemChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemChecks_ItemCheckTypes_ItemCheckTypeId",
                table: "ItemChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemChecks_Locations_LocationId",
                table: "ItemChecks");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectables_CollectableTypes_CollectableTypeId",
                table: "Collectables",
                column: "CollectableTypeId",
                principalTable: "CollectableTypes",
                principalColumn: "CollectableTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectables_Locations_LocationId",
                table: "Collectables",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCheckRequirements_InventoryEquipment_InventoryEquipmentId",
                table: "ItemCheckRequirements",
                column: "InventoryEquipmentId",
                principalTable: "InventoryEquipment",
                principalColumn: "InventoryEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCheckRequirements_ItemChecks_ItemCheckId",
                table: "ItemCheckRequirements",
                column: "ItemCheckId",
                principalTable: "ItemChecks",
                principalColumn: "ItemCheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemChecks_ItemAgeRequirements_ItemAgeRequirementId",
                table: "ItemChecks",
                column: "ItemAgeRequirementId",
                principalTable: "ItemAgeRequirements",
                principalColumn: "ItemAgeRequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemChecks_ItemCheckTypes_ItemCheckTypeId",
                table: "ItemChecks",
                column: "ItemCheckTypeId",
                principalTable: "ItemCheckTypes",
                principalColumn: "ItemCheckTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemChecks_Locations_LocationId",
                table: "ItemChecks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");
        }
    }
}

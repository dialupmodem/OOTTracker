using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryEquipmentCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InventoryEquipmentCategoryId",
                table: "InventoryEquipment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventoryEquipmentCategories",
                columns: table => new
                {
                    InventoryEquipmentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryEquipmentCategories", x => x.InventoryEquipmentCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEquipment_InventoryEquipmentCategoryId",
                table: "InventoryEquipment",
                column: "InventoryEquipmentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryEquipment_InventoryEquipmentCategories_InventoryEquipmentCategoryId",
                table: "InventoryEquipment",
                column: "InventoryEquipmentCategoryId",
                principalTable: "InventoryEquipmentCategories",
                principalColumn: "InventoryEquipmentCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryEquipment_InventoryEquipmentCategories_InventoryEquipmentCategoryId",
                table: "InventoryEquipment");

            migrationBuilder.DropTable(
                name: "InventoryEquipmentCategories");

            migrationBuilder.DropIndex(
                name: "IX_InventoryEquipment_InventoryEquipmentCategoryId",
                table: "InventoryEquipment");

            migrationBuilder.DropColumn(
                name: "InventoryEquipmentCategoryId",
                table: "InventoryEquipment");
        }
    }
}

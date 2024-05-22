using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddItemChecksItemCheckTypesItemAgeRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemAgeRequirements",
                columns: table => new
                {
                    ItemAgeRequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAgeRequirements", x => x.ItemAgeRequirementId);
                });

            migrationBuilder.CreateTable(
                name: "ItemCheckTypes",
                columns: table => new
                {
                    ItemCheckTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCheckTypes", x => x.ItemCheckTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ItemChecks",
                columns: table => new
                {
                    ItemCheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemCheckTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemAgeRequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemChecks", x => x.ItemCheckId);
                    table.ForeignKey(
                        name: "FK_ItemChecks_ItemAgeRequirements_ItemAgeRequirementId",
                        column: x => x.ItemAgeRequirementId,
                        principalTable: "ItemAgeRequirements",
                        principalColumn: "ItemAgeRequirementId");
                    table.ForeignKey(
                        name: "FK_ItemChecks_ItemCheckTypes_ItemCheckTypeId",
                        column: x => x.ItemCheckTypeId,
                        principalTable: "ItemCheckTypes",
                        principalColumn: "ItemCheckTypeId");
                    table.ForeignKey(
                        name: "FK_ItemChecks_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemChecks_ItemAgeRequirementId",
                table: "ItemChecks",
                column: "ItemAgeRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemChecks_ItemCheckTypeId",
                table: "ItemChecks",
                column: "ItemCheckTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemChecks_LocationId",
                table: "ItemChecks",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemChecks");

            migrationBuilder.DropTable(
                name: "ItemAgeRequirements");

            migrationBuilder.DropTable(
                name: "ItemCheckTypes");
        }
    }
}

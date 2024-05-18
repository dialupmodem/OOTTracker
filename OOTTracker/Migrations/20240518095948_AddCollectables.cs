using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OOTTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collectables",
                columns: table => new
                {
                    CollectableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectableTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectables", x => x.CollectableId);
                    table.ForeignKey(
                        name: "FK_Collectables_CollectableTypes_CollectableTypeId",
                        column: x => x.CollectableTypeId,
                        principalTable: "CollectableTypes",
                        principalColumn: "CollectableTypeId");
                    table.ForeignKey(
                        name: "FK_Collectables_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collectables_CollectableTypeId",
                table: "Collectables",
                column: "CollectableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Collectables_LocationId",
                table: "Collectables",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collectables");
        }
    }
}

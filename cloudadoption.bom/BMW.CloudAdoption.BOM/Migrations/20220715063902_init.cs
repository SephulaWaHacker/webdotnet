using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMW.CloudAdoption.BOM.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bom");

            migrationBuilder.CreateTable(
                name: "bill_of_materials",
                schema: "bom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bill_of_materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "part_families",
                schema: "bom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bom_part_family",
                schema: "bom",
                columns: table => new
                {
                    BomId = table.Column<int>(type: "int", nullable: false),
                    PartFamilyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bom_part_family", x => new { x.BomId, x.PartFamilyId });
                    table.ForeignKey(
                        name: "FK_bom_part_family_bill_of_materials_BomId",
                        column: x => x.BomId,
                        principalSchema: "bom",
                        principalTable: "bill_of_materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bom_part_family_part_families_PartFamilyId",
                        column: x => x.PartFamilyId,
                        principalSchema: "bom",
                        principalTable: "part_families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "part_ids",
                schema: "bom",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartFamilyId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part_ids", x => new { x.Id, x.PartFamilyId });
                    table.ForeignKey(
                        name: "FK_part_ids_part_families_PartFamilyId",
                        column: x => x.PartFamilyId,
                        principalSchema: "bom",
                        principalTable: "part_families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bom_part_family_PartFamilyId",
                schema: "bom",
                table: "bom_part_family",
                column: "PartFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_part_ids_PartFamilyId",
                schema: "bom",
                table: "part_ids",
                column: "PartFamilyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bom_part_family",
                schema: "bom");

            migrationBuilder.DropTable(
                name: "part_ids",
                schema: "bom");

            migrationBuilder.DropTable(
                name: "bill_of_materials",
                schema: "bom");

            migrationBuilder.DropTable(
                name: "part_families",
                schema: "bom");
        }
    }
}

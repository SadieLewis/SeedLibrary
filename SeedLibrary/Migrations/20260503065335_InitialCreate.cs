using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeedLibrary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommonName",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonName", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PlantingDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    EndMonth = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantingDate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SourceName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variety",
                columns: table => new
                {
                    VarietyName = table.Column<string>(type: "TEXT", nullable: false),
                    Depth = table.Column<string>(type: "TEXT", nullable: true),
                    CommonNameName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variety", x => x.VarietyName);
                    table.ForeignKey(
                        name: "FK_Variety_CommonName_CommonNameName",
                        column: x => x.CommonNameName,
                        principalTable: "CommonName",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "SeedPacket",
                columns: table => new
                {
                    SeedId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    VarietyName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedPacket", x => x.SeedId);
                    table.ForeignKey(
                        name: "FK_SeedPacket_Variety_VarietyName",
                        column: x => x.VarietyName,
                        principalTable: "Variety",
                        principalColumn: "VarietyName");
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeedId = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => new { x.SourceId, x.SeedId, x.Year });
                    table.ForeignKey(
                        name: "FK_Donation_SeedPacket_SeedId",
                        column: x => x.SeedId,
                        principalTable: "SeedPacket",
                        principalColumn: "SeedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donation_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Growing",
                columns: table => new
                {
                    PlantingDatesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SeedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Growing", x => new { x.PlantingDatesId, x.SeedId });
                    table.ForeignKey(
                        name: "FK_Growing_PlantingDate_PlantingDatesId",
                        column: x => x.PlantingDatesId,
                        principalTable: "PlantingDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Growing_SeedPacket_SeedId",
                        column: x => x.SeedId,
                        principalTable: "SeedPacket",
                        principalColumn: "SeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donation_SeedId",
                table: "Donation",
                column: "SeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Growing_SeedId",
                table: "Growing",
                column: "SeedId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedPacket_VarietyName",
                table: "SeedPacket",
                column: "VarietyName");

            migrationBuilder.CreateIndex(
                name: "IX_Variety_CommonNameName",
                table: "Variety",
                column: "CommonNameName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "Growing");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "PlantingDate");

            migrationBuilder.DropTable(
                name: "SeedPacket");

            migrationBuilder.DropTable(
                name: "Variety");

            migrationBuilder.DropTable(
                name: "CommonName");
        }
    }
}

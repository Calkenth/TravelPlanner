using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPlanner.API.Migrations
{
    public partial class AdjustClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "FromTo",
                columns: table => new
                {
                    FromToID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCityCityID = table.Column<int>(nullable: true),
                    ToCityCityID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FromTo", x => x.FromToID);
                    table.ForeignKey(
                        name: "FK_FromTo_Cities_FromCityCityID",
                        column: x => x.FromCityCityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FromTo_Cities_ToCityCityID",
                        column: x => x.ToCityCityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    TravelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromToID = table.Column<int>(nullable: false),
                    Departure_Time = table.Column<DateTime>(nullable: false),
                    Arrival_Time = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.TravelID);
                    table.ForeignKey(
                        name: "FK_Travels_FromTo_FromToID",
                        column: x => x.FromToID,
                        principalTable: "FromTo",
                        principalColumn: "FromToID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FromTo_FromCityCityID",
                table: "FromTo",
                column: "FromCityCityID");

            migrationBuilder.CreateIndex(
                name: "IX_FromTo_ToCityCityID",
                table: "FromTo",
                column: "ToCityCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_FromToID",
                table: "Travels",
                column: "FromToID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "FromTo");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}

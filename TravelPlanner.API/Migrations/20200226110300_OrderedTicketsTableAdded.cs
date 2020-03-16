using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPlanner.API.Migrations
{
    public partial class OrderedTicketsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderedTickets",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    FromWhere = table.Column<string>(nullable: true),
                    DepartureTime = table.Column<string>(nullable: true),
                    ToWhere = table.Column<string>(nullable: true),
                    ArrivalTime = table.Column<string>(nullable: true),
                    Changes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedTickets", x => x.OrderID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedTickets");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPlanner.API.Migrations
{
    public partial class OrderedTicketsTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Changes",
                table: "OrderedTickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Changes",
                table: "OrderedTickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

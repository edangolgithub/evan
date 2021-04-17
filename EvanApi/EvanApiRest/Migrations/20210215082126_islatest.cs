using Microsoft.EntityFrameworkCore.Migrations;

namespace EvanApi1.Migrations
{
    public partial class islatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "islatest",
                table: "Transaction",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "islatest",
                table: "Transaction");
        }
    }
}

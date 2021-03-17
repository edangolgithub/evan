using Microsoft.EntityFrameworkCore.Migrations;

namespace EvanApi.Migrations
{
    public partial class interest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Transaction");

            migrationBuilder.AddColumn<double>(
                name: "balance",
                table: "Transaction",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "created",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "interest",
                table: "Transaction",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "id",
                keyValue: "1",
                columns: new[] { "balance", "created" },
                values: new object[] { 100.0, "2021-01-01" });

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "id",
                keyValue: "2",
                columns: new[] { "balance", "created" },
                values: new object[] { 100.0, "2021-01-01" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "created",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "interest",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "Transaction",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "id",
                keyValue: "1",
                column: "date",
                value: "2021-01-01");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "id",
                keyValue: "2",
                column: "date",
                value: "2021-01-01");
        }
    }
}

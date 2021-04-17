using Microsoft.EntityFrameworkCore.Migrations;

namespace EvanApi1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(767)", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    accountid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(767)", nullable: false),
                    accounttypeid = table.Column<string>(type: "text", nullable: true),
                    accounttype = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(767)", nullable: false),
                    accountid = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<string>(type: "text", nullable: true),
                    accounttypeid = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    entry = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "id", "accountid", "address", "email", "name", "phone" },
                values: new object[,]
                {
                    { "1", "1", "Khusibu", "dangolevan@gmail.com", "Evan Dangol", "123456789" },
                    { "2", "2", "Kilagal", "bso@gmail.com", "Bso Amatya", "234567891" }
                });

            migrationBuilder.InsertData(
                table: "AccountType",
                columns: new[] { "id", "accounttype", "accounttypeid" },
                values: new object[,]
                {
                    { "1", "Daily", "1" },
                    { "2", "Month", "2" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "id", "accountid", "accounttypeid", "amount", "date", "entry", "type" },
                values: new object[,]
                {
                    { "1", "1", "1", "100", "2021-01-01", "Debit", null },
                    { "2", "2", "2", "100", "2021-01-01", "Debit", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}

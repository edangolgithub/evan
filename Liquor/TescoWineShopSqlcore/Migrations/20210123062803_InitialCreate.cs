using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TescoWineShopSqlcore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountClasses",
                columns: table => new
                {
                    AccountClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsSystemAccount = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClasses", x => x.AccountClassId);
                });

            migrationBuilder.CreateTable(
                name: "AccountGroups",
                columns: table => new
                {
                    AccountGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AccountGroupName = table.Column<string>(type: "text", nullable: true),
                    AccountClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountGroups", x => x.AccountGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Administration",
                columns: table => new
                {
                    Administrationid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "text", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administration", x => x.Administrationid);
                });

            migrationBuilder.CreateTable(
                name: "BeverageCategory",
                columns: table => new
                {
                    BeverageCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BeverageCategoryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageCategory", x => x.BeverageCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    CustomerAddress = table.Column<string>(type: "text", nullable: true),
                    CustomerPhone = table.Column<string>(type: "text", nullable: true),
                    CustomerEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYear",
                columns: table => new
                {
                    FiscalYearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FiscalYearDescription = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYear", x => x.FiscalYearId);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SupplierName = table.Column<string>(type: "text", nullable: true),
                    SupplierAddress = table.Column<string>(type: "text", nullable: true),
                    SupplierPhone = table.Column<string>(type: "text", nullable: true),
                    SupplierCity = table.Column<string>(type: "text", nullable: true),
                    PanNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserInRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Vat",
                columns: table => new
                {
                    VatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VatAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vat", x => x.VatId);
                });

            migrationBuilder.CreateTable(
                name: "Beverage",
                columns: table => new
                {
                    BeverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(767)", nullable: true),
                    DrinkType = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ShowInChart = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPopular = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BeverageCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverage", x => x.BeverageId);
                    table.ForeignKey(
                        name: "FK_Beverage_BeverageCategory_BeverageCategoryId",
                        column: x => x.BeverageCategoryId,
                        principalTable: "BeverageCategory",
                        principalColumn: "BeverageCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrder",
                columns: table => new
                {
                    SaleOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TotalAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Due = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    AmountAfterDiscount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrder", x => x.SaleOrderId);
                    table.ForeignKey(
                        name: "FK_SaleOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CompanyAddress = table.Column<string>(type: "text", nullable: true),
                    CompanyCity = table.Column<string>(type: "text", nullable: true),
                    CompanyPhone = table.Column<string>(type: "text", nullable: true),
                    CompanyPanNumber = table.Column<string>(type: "text", nullable: true),
                    CompanyType = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FiscalYearId = table.Column<int>(type: "int", nullable: true),
                    CompanyPassword = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Company_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "FiscalYearId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Total = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TaxableAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Due = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Menus = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    UserPhone = table.Column<string>(type: "text", nullable: true),
                    UserAddress = table.Column<string>(type: "text", nullable: true),
                    UserTole = table.Column<string>(type: "text", nullable: true),
                    UserCity = table.Column<string>(type: "text", nullable: true),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    UserCreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserRole_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesReturn",
                columns: table => new
                {
                    SalesReturnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReturnAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    BeverageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturn", x => x.SalesReturnId);
                    table.ForeignKey(
                        name: "FK_SalesReturn_Beverage_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverage",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SaleOrderId = table.Column<int>(type: "int", nullable: true),
                    BeverageId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Due = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Paid = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Profit = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    DuePaying = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sale_Beverage_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverage",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_SaleOrder_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrder",
                        principalColumn: "SaleOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccount",
                columns: table => new
                {
                    LedgerAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AccountClassId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ParentLedgerAccountId = table.Column<int>(type: "int", nullable: true),
                    IsSystemLedger = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ShowInChart = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccountGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccount", x => x.LedgerAccountId);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_AccountClasses_AccountClassId",
                        column: x => x.AccountClassId,
                        principalTable: "AccountClasses",
                        principalColumn: "AccountClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_AccountGroups_AccountGroupId",
                        column: x => x.AccountGroupId,
                        principalTable: "AccountGroups",
                        principalColumn: "AccountGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_LedgerAccount_ParentLedgerAccountId",
                        column: x => x.ParentLedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "LedgerAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BeverageId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    LineTotalAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MetricQuantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    VatAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Metric = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchase_Beverage_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverage",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LedgerTransaction",
                columns: table => new
                {
                    LedgerTransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VoucherNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FiscalYearId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTransaction", x => x.LedgerTransactionId);
                    table.ForeignKey(
                        name: "FK_LedgerTransaction_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "FiscalYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LedgerTransaction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DuePay",
                columns: table => new
                {
                    DuePayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SaleOrderId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    SaleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuePay", x => x.DuePayId);
                    table.ForeignKey(
                        name: "FK_DuePay_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DuePay_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DuePay_SaleOrder_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrder",
                        principalColumn: "SaleOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccountBalance",
                columns: table => new
                {
                    LedgerAccountBalanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LedgerAccountId = table.Column<int>(type: "int", nullable: false),
                    PeriodYear = table.Column<int>(type: "int", nullable: false),
                    BeginingBalance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo1 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo2 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo3 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo4 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo5 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo6 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo7 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo8 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo9 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo10 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo11 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Saldo12 = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccountBalance", x => x.LedgerAccountBalanceId);
                    table.ForeignKey(
                        name: "FK_LedgerAccountBalance_LedgerAccount_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "LedgerAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ledgergenerals",
                columns: table => new
                {
                    LedgergeneralId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LedgerTransactionId = table.Column<int>(type: "int", nullable: false),
                    JournalEntryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LedgerAccountId = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: true),
                    ChequeNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgergenerals", x => x.LedgergeneralId);
                    table.ForeignKey(
                        name: "FK_Ledgergenerals_LedgerAccount_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "LedgerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ledgergenerals_LedgerTransaction_LedgerTransactionId",
                        column: x => x.LedgerTransactionId,
                        principalTable: "LedgerTransaction",
                        principalColumn: "LedgerTransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerTransactionDetail",
                columns: table => new
                {
                    LedgerTransactionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LedgerTransactionId = table.Column<int>(type: "int", nullable: false),
                    LedgerAccountId = table.Column<int>(type: "int", nullable: false),
                    Seq = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTransactionDetail", x => x.LedgerTransactionDetailId);
                    table.ForeignKey(
                        name: "FK_LedgerTransactionDetail_LedgerAccount_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "LedgerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerTransactionDetail_LedgerTransaction_LedgerTransactionId",
                        column: x => x.LedgerTransactionId,
                        principalTable: "LedgerTransaction",
                        principalColumn: "LedgerTransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountClasses",
                columns: new[] { "AccountClassId", "Description", "IsSystemAccount", "Name" },
                values: new object[,]
                {
                    { 1, null, false, "Asset" },
                    { 2, null, false, "Expense" },
                    { 3, null, false, "Revenue" },
                    { 4, null, false, "Liabilities" },
                    { 5, null, false, "Equity" }
                });

            migrationBuilder.InsertData(
                table: "AccountGroups",
                columns: new[] { "AccountGroupId", "AccountClassId", "AccountGroupName" },
                values: new object[] { 1, 1, "FixedAsset" });

            migrationBuilder.InsertData(
                table: "Administration",
                columns: new[] { "Administrationid", "Key", "value" },
                values: new object[,]
                {
                    { 14, "BarCodeFolder", "/home/ec2-user/.local/share\\WineShop\\BarCode" },
                    { 13, "Currency", "1" },
                    { 11, "AutomateLedgerPost", "1" },
                    { 10, "DiscountStyle", "Percent" },
                    { 9, "FiscalYear", "2075/2076" },
                    { 8, "Vat", "13" },
                    { 12, "UseBarCode", "0" },
                    { 6, "JsonFolder", "/home/ec2-user/.local/share\\WineShop\\Json" },
                    { 5, "Tables", "50" },
                    { 4, "DocumentFolder", "/home/ec2-user/.local/share\\WineShop\\Documents" },
                    { 3, "ImageFolder", "/home/ec2-user/.local/share\\WineShop\\Images" },
                    { 7, "VatEnabled", "0" },
                    { 2, "ServiceCharge", "10" },
                    { 1, "TaxRate", "13" }
                });

            migrationBuilder.InsertData(
                table: "BeverageCategory",
                columns: new[] { "BeverageCategoryId", "BeverageCategoryName" },
                values: new object[,]
                {
                    { 1, "Whisky" },
                    { 2, "Wine" },
                    { 3, "Beer" },
                    { 4, "Soft Drink" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyAddress", "CompanyCity", "CompanyName", "CompanyPanNumber", "CompanyPassword", "CompanyPhone", "CompanyType", "FiscalYearId", "IsActive" },
                values: new object[] { 1, "Reliable Colony, Bhainsepati", "Lalitpur", "Tesco Wine Store", "12345", null, "12345", null, null, false });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "UserInRole" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 9 },
                    { 6, 6 },
                    { 7, 8 },
                    { 8, 2 },
                    { 9, 7 },
                    { 10, 5 }
                });

            migrationBuilder.InsertData(
                table: "Vat",
                columns: new[] { "VatId", "VatAmount" },
                values: new object[] { 1, 13m });

            migrationBuilder.InsertData(
                table: "LedgerAccount",
                columns: new[] { "LedgerAccountId", "AccountClassId", "AccountGroupId", "AccountName", "CompanyId", "Description", "IsSystemLedger", "ParentLedgerAccountId", "ShowInChart", "SortOrder" },
                values: new object[,]
                {
                    { 1, 1, 1, "Cash", 1, null, true, null, true, 0 },
                    { 2, 3, 1, "Sales", 1, null, true, null, true, 0 },
                    { 3, 2, 1, "Purchase", 1, null, true, null, true, 0 },
                    { 4, 1, 1, "Debitors", 1, null, false, null, true, 0 },
                    { 5, 4, 1, "Creditors", 1, null, true, null, true, 0 },
                    { 7, 1, 1, "Bank", 1, null, true, null, true, 0 },
                    { 9, 1, 1, "Accounts Receivable", 1, null, true, null, true, 0 },
                    { 10, 4, 1, "Accounts Payable", 1, null, true, null, true, 0 },
                    { 11, 2, 1, "Salaries", 1, null, false, null, true, 0 },
                    { 12, 2, 1, "Rent Expense", 1, null, false, null, true, 0 },
                    { 13, 2, 1, "Advertising Expenses", 1, null, false, null, true, 0 },
                    { 14, 1, 1, "FixedAsset", 1, null, false, null, true, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Menus", "Password", "UserAddress", "UserCity", "UserCreatedOn", "UserEmail", "UserName", "UserPhone", "UserRoleId", "UserTole" },
                values: new object[,]
                {
                    { 1, null, "dMRo3oqVLOs=", "Dathu Sadak", "Kathmandu", new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "dangolevan@gmail.com", "admin", "9849178036", 2, "Khusibu" },
                    { 2, null, "R2LOIQb+UyM=", "Dathu Sadak", "Kathmandu", new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "ram@gmail.com", "a", "9849178036", 2, "Khusibu" },
                    { 3, null, "R2LOIQb+UyM=", "Dathu Sadak", "Kathmandu", new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "dangolevan@gmail.com", "b", "9849178036", 3, "Khusibu" },
                    { 4, null, "R2LOIQb+UyM=", "Dathu Sadak", "Kathmandu", new DateTime(2021, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "dangolevan@gmail.com", "c", "9849178036", 4, "Khusibu" }
                });

            migrationBuilder.InsertData(
                table: "LedgerAccount",
                columns: new[] { "LedgerAccountId", "AccountClassId", "AccountGroupId", "AccountName", "CompanyId", "Description", "IsSystemLedger", "ParentLedgerAccountId", "ShowInChart", "SortOrder" },
                values: new object[] { 15, 1, 1, "Ram", 1, null, true, 4, true, 0 });

            migrationBuilder.InsertData(
                table: "LedgerAccount",
                columns: new[] { "LedgerAccountId", "AccountClassId", "AccountGroupId", "AccountName", "CompanyId", "Description", "IsSystemLedger", "ParentLedgerAccountId", "ShowInChart", "SortOrder" },
                values: new object[] { 6, 4, 1, "Tesco Suppliers", 1, null, true, 5, true, 0 });

            migrationBuilder.InsertData(
                table: "LedgerAccount",
                columns: new[] { "LedgerAccountId", "AccountClassId", "AccountGroupId", "AccountName", "CompanyId", "Description", "IsSystemLedger", "ParentLedgerAccountId", "ShowInChart", "SortOrder" },
                values: new object[] { 8, 1, 1, "Tesco Bank", 1, null, true, 7, true, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Beverage_BeverageCategoryId",
                table: "Beverage",
                column: "BeverageCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beverage_Name",
                table: "Beverage",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_FiscalYearId",
                table: "Company",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_DuePay_CustomerId",
                table: "DuePay",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DuePay_SaleId",
                table: "DuePay",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_DuePay_SaleOrderId",
                table: "DuePay",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SupplierId",
                table: "Invoice",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_AccountClassId",
                table: "LedgerAccount",
                column: "AccountClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_AccountGroupId",
                table: "LedgerAccount",
                column: "AccountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_CompanyId",
                table: "LedgerAccount",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_ParentLedgerAccountId",
                table: "LedgerAccount",
                column: "ParentLedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccountBalance_LedgerAccountId",
                table: "LedgerAccountBalance",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgergenerals_LedgerAccountId",
                table: "Ledgergenerals",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgergenerals_LedgerTransactionId",
                table: "Ledgergenerals",
                column: "LedgerTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_FiscalYearId",
                table: "LedgerTransaction",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransaction_UserId",
                table: "LedgerTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransactionDetail_LedgerAccountId",
                table: "LedgerTransactionDetail",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTransactionDetail_LedgerTransactionId",
                table: "LedgerTransactionDetail",
                column: "LedgerTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_BeverageId",
                table: "Purchase",
                column: "BeverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_InvoiceId",
                table: "Purchase",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_SupplierId",
                table: "Purchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_BeverageId",
                table: "Sale",
                column: "BeverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SaleOrderId",
                table: "Sale",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrder_CustomerId",
                table: "SaleOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_BeverageId",
                table: "SalesReturn",
                column: "BeverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administration");

            migrationBuilder.DropTable(
                name: "DuePay");

            migrationBuilder.DropTable(
                name: "LedgerAccountBalance");

            migrationBuilder.DropTable(
                name: "Ledgergenerals");

            migrationBuilder.DropTable(
                name: "LedgerTransactionDetail");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "SalesReturn");

            migrationBuilder.DropTable(
                name: "Vat");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "LedgerAccount");

            migrationBuilder.DropTable(
                name: "LedgerTransaction");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Beverage");

            migrationBuilder.DropTable(
                name: "SaleOrder");

            migrationBuilder.DropTable(
                name: "AccountClasses");

            migrationBuilder.DropTable(
                name: "AccountGroups");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "BeverageCategory");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "FiscalYear");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}

namespace TescoWineShopSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountClasses",
                c => new
                    {
                        AccountClassId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(),
                        IsSystemAccount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountClassId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.LedgerAccounts",
                c => new
                    {
                        LedgerAccountId = c.Int(nullable: false, identity: true),
                        AccountName = c.String(maxLength: 50),
                        Description = c.String(),
                        AccountClassId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        ParentLedgerAccountId = c.Int(),
                        IsSystemLedger = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        ShowInChart = c.Boolean(nullable: false),
                        AccountGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.LedgerAccountId)
                .ForeignKey("dbo.AccountClasses", t => t.AccountClassId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.LedgerAccounts", t => t.ParentLedgerAccountId)
                .ForeignKey("dbo.AccountGroups", t => t.AccountGroupId)
                .Index(t => t.AccountName, unique: true)
                .Index(t => t.AccountClassId)
                .Index(t => t.CompanyId)
                .Index(t => t.ParentLedgerAccountId)
                .Index(t => t.AccountGroupId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyAddress = c.String(),
                        CompanyCity = c.String(),
                        CompanyPhone = c.String(),
                        CompanyPanNumber = c.String(),
                        CompanyType = c.String(),
                        FiscalYearId = c.Int(),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.FiscalYears", t => t.FiscalYearId)
                .Index(t => t.FiscalYearId);
            
            CreateTable(
                "dbo.FiscalYears",
                c => new
                    {
                        FiscalYearId = c.Int(nullable: false, identity: true),
                        FiscalYearDescription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FiscalYearId);
            
            CreateTable(
                "dbo.LedgerTransactions",
                c => new
                    {
                        LedgerTransactionId = c.Int(nullable: false, identity: true),
                        VoucherNumber = c.String(maxLength: 100),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        UserId = c.Int(),
                        FiscalYearId = c.Int(),
                    })
                .PrimaryKey(t => t.LedgerTransactionId)
                .ForeignKey("dbo.FiscalYears", t => t.FiscalYearId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FiscalYearId);
            
            CreateTable(
                "dbo.LedgerGenerals",
                c => new
                    {
                        LedgergeneralId = c.Int(nullable: false, identity: true),
                        LedgerTransactionId = c.Int(nullable: false),
                        JournalEntryDate = c.DateTime(nullable: false),
                        LedgerAccountId = c.Int(nullable: false),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClosingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankName = c.String(),
                        ChequeNumber = c.String(),
                    })
                .PrimaryKey(t => t.LedgergeneralId)
                .ForeignKey("dbo.LedgerAccounts", t => t.LedgerAccountId, cascadeDelete: true)
                .ForeignKey("dbo.LedgerTransactions", t => t.LedgerTransactionId, cascadeDelete: true)
                .Index(t => t.LedgerTransactionId)
                .Index(t => t.LedgerAccountId);
            
            CreateTable(
                "dbo.LedgerTransactionDetails",
                c => new
                    {
                        LedgerTransactionDetailId = c.Int(nullable: false, identity: true),
                        LedgerTransactionId = c.Int(nullable: false),
                        LedgerAccountId = c.Int(nullable: false),
                        Seq = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LedgerTransactionDetailId)
                .ForeignKey("dbo.LedgerAccounts", t => t.LedgerAccountId, cascadeDelete: true)
                .ForeignKey("dbo.LedgerTransactions", t => t.LedgerTransactionId, cascadeDelete: true)
                .Index(t => t.LedgerTransactionId)
                .Index(t => t.LedgerAccountId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(),
                        Menus = c.String(),
                        UserEmail = c.String(),
                        UserPhone = c.String(),
                        UserAddress = c.String(),
                        UserTole = c.String(),
                        UserCity = c.String(),
                        UserRoleId = c.Int(nullable: false),
                        UserCreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserInRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.AccountGroups",
                c => new
                    {
                        AccountGroupId = c.Int(nullable: false, identity: true),
                        AccountGroupName = c.String(),
                        AccountClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountGroupId);
            
            CreateTable(
                "dbo.Administrations",
                c => new
                    {
                        Administrationid = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        value = c.String(),
                    })
                .PrimaryKey(t => t.Administrationid);
            
            CreateTable(
                "dbo.BeverageCategories",
                c => new
                    {
                        BeverageCategoryId = c.Int(nullable: false, identity: true),
                        BeverageCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.BeverageCategoryId);
            
            CreateTable(
                "dbo.Beverages",
                c => new
                    {
                        BeverageId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        DrinkType = c.Int(),
                        Volume = c.Int(),
                        Country = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Image = c.String(),
                        ShowInChart = c.Boolean(nullable: false),
                        IsPopular = c.Boolean(nullable: false),
                        BeverageCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BeverageId)
                .ForeignKey("dbo.BeverageCategories", t => t.BeverageCategoryId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.BeverageCategoryId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        BeverageId = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LineTotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        MetricQuantity = c.Int(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        Vat = c.Decimal(precision: 18, scale: 2),
                        VatAmount = c.Decimal(precision: 18, scale: 2),
                        InvoiceId = c.Int(nullable: false),
                        Metric = c.Int(),
                        Supplier_SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Beverages", t => t.BeverageId, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId)
                .Index(t => t.BeverageId)
                .Index(t => t.InvoiceId)
                .Index(t => t.Supplier_SupplierId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxableAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Due = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierId = c.Int(nullable: false),
                        Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceNumber = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => new { t.InvoiceNumber, t.SupplierId }, unique: true, name: "IX_FirstAndSecond");
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        SupplierAddress = c.String(),
                        SupplierPhone = c.String(),
                        SupplierCity = c.String(),
                        PanNumber = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        SaleOrderId = c.Int(),
                        BeverageId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        Due = c.Decimal(precision: 18, scale: 2),
                        Paid = c.Decimal(precision: 18, scale: 2),
                        Profit = c.Decimal(precision: 18, scale: 2),
                        CustomerId = c.Int(),
                        DuePaying = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Beverages", t => t.BeverageId, cascadeDelete: true)
                .ForeignKey("dbo.SaleOrders", t => t.SaleOrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.SaleOrderId)
                .Index(t => t.BeverageId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerAddress = c.String(),
                        CustomerPhone = c.String(),
                        CustomerEmail = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.DuePays",
                c => new
                    {
                        DuePayId = c.Int(nullable: false, identity: true),
                        SaleOrderId = c.Int(nullable: false),
                        Date = c.DateTime(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_CustomerId = c.Int(),
                        Sale_SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.DuePayId)
                .ForeignKey("dbo.SaleOrders", t => t.SaleOrderId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .ForeignKey("dbo.Sales", t => t.Sale_SaleId)
                .Index(t => t.SaleOrderId)
                .Index(t => t.Customer_CustomerId)
                .Index(t => t.Sale_SaleId);
            
            CreateTable(
                "dbo.SaleOrders",
                c => new
                    {
                        SaleOrderId = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleDate = c.DateTime(nullable: false),
                        Due = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(),
                        AmountAfterDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaleOrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.LedgerAccountBalances",
                c => new
                    {
                        LedgerAccountBalanceId = c.Int(nullable: false, identity: true),
                        LedgerAccountId = c.Int(nullable: false),
                        PeriodYear = c.Int(nullable: false),
                        BeginingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo4 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo6 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo7 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo8 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo9 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo10 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo11 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Saldo12 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LedgerAccountBalanceId)
                .ForeignKey("dbo.LedgerAccounts", t => t.LedgerAccountId, cascadeDelete: true)
                .Index(t => t.LedgerAccountId);
            
            CreateTable(
                "dbo.SalesReturns",
                c => new
                    {
                        SalesReturnId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ReturnAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BeverageId = c.Int(),
                    })
                .PrimaryKey(t => t.SalesReturnId)
                .ForeignKey("dbo.Beverages", t => t.BeverageId)
                .Index(t => t.BeverageId);
            
            CreateTable(
                "dbo.Vats",
                c => new
                    {
                        VatId = c.Int(nullable: false, identity: true),
                        VatAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.VatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesReturns", "BeverageId", "dbo.Beverages");
            DropForeignKey("dbo.LedgerAccountBalances", "LedgerAccountId", "dbo.LedgerAccounts");
            DropForeignKey("dbo.DuePays", "Sale_SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DuePays", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DuePays", "SaleOrderId", "dbo.SaleOrders");
            DropForeignKey("dbo.Sales", "SaleOrderId", "dbo.SaleOrders");
            DropForeignKey("dbo.SaleOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Sales", "BeverageId", "dbo.Beverages");
            DropForeignKey("dbo.Invoices", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Purchases", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Purchases", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Purchases", "BeverageId", "dbo.Beverages");
            DropForeignKey("dbo.Beverages", "BeverageCategoryId", "dbo.BeverageCategories");
            DropForeignKey("dbo.LedgerAccounts", "AccountGroupId", "dbo.AccountGroups");
            DropForeignKey("dbo.LedgerAccounts", "ParentLedgerAccountId", "dbo.LedgerAccounts");
            DropForeignKey("dbo.LedgerAccounts", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Companies", "FiscalYearId", "dbo.FiscalYears");
            DropForeignKey("dbo.LedgerTransactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.LedgerTransactionDetails", "LedgerTransactionId", "dbo.LedgerTransactions");
            DropForeignKey("dbo.LedgerTransactionDetails", "LedgerAccountId", "dbo.LedgerAccounts");
            DropForeignKey("dbo.LedgerGenerals", "LedgerTransactionId", "dbo.LedgerTransactions");
            DropForeignKey("dbo.LedgerGenerals", "LedgerAccountId", "dbo.LedgerAccounts");
            DropForeignKey("dbo.LedgerTransactions", "FiscalYearId", "dbo.FiscalYears");
            DropForeignKey("dbo.LedgerAccounts", "AccountClassId", "dbo.AccountClasses");
            DropIndex("dbo.SalesReturns", new[] { "BeverageId" });
            DropIndex("dbo.LedgerAccountBalances", new[] { "LedgerAccountId" });
            DropIndex("dbo.SaleOrders", new[] { "CustomerId" });
            DropIndex("dbo.DuePays", new[] { "Sale_SaleId" });
            DropIndex("dbo.DuePays", new[] { "Customer_CustomerId" });
            DropIndex("dbo.DuePays", new[] { "SaleOrderId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Sales", new[] { "BeverageId" });
            DropIndex("dbo.Sales", new[] { "SaleOrderId" });
            DropIndex("dbo.Invoices", "IX_FirstAndSecond");
            DropIndex("dbo.Purchases", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.Purchases", new[] { "InvoiceId" });
            DropIndex("dbo.Purchases", new[] { "BeverageId" });
            DropIndex("dbo.Beverages", new[] { "BeverageCategoryId" });
            DropIndex("dbo.Beverages", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.LedgerTransactionDetails", new[] { "LedgerAccountId" });
            DropIndex("dbo.LedgerTransactionDetails", new[] { "LedgerTransactionId" });
            DropIndex("dbo.LedgerGenerals", new[] { "LedgerAccountId" });
            DropIndex("dbo.LedgerGenerals", new[] { "LedgerTransactionId" });
            DropIndex("dbo.LedgerTransactions", new[] { "FiscalYearId" });
            DropIndex("dbo.LedgerTransactions", new[] { "UserId" });
            DropIndex("dbo.Companies", new[] { "FiscalYearId" });
            DropIndex("dbo.LedgerAccounts", new[] { "AccountGroupId" });
            DropIndex("dbo.LedgerAccounts", new[] { "ParentLedgerAccountId" });
            DropIndex("dbo.LedgerAccounts", new[] { "CompanyId" });
            DropIndex("dbo.LedgerAccounts", new[] { "AccountClassId" });
            DropIndex("dbo.LedgerAccounts", new[] { "AccountName" });
            DropIndex("dbo.AccountClasses", new[] { "Name" });
            DropTable("dbo.Vats");
            DropTable("dbo.SalesReturns");
            DropTable("dbo.LedgerAccountBalances");
            DropTable("dbo.SaleOrders");
            DropTable("dbo.DuePays");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Invoices");
            DropTable("dbo.Purchases");
            DropTable("dbo.Beverages");
            DropTable("dbo.BeverageCategories");
            DropTable("dbo.Administrations");
            DropTable("dbo.AccountGroups");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.LedgerTransactionDetails");
            DropTable("dbo.LedgerGenerals");
            DropTable("dbo.LedgerTransactions");
            DropTable("dbo.FiscalYears");
            DropTable("dbo.Companies");
            DropTable("dbo.LedgerAccounts");
            DropTable("dbo.AccountClasses");
        }
    }
}

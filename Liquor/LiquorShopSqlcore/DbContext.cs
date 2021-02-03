using Microsoft.EntityFrameworkCore;
//using MySQL.EntityFrameworkCore.Extensions;

namespace TescoWineShopSql
{
    public class WineContext : DbContext
    {
        public WineContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
               optionsBuilder.UseSqlServer("workstation id=wineshop.mssql.somee.com;packet size=4096;user id=edangol_SQLLogin_1;pwd=qpnpz5zqqj;data source=wineshop.mssql.somee.com;persist security info=False;initial catalog=wineshop");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Beverage>().ToTable("Beverage").HasIndex(e=>e.Name).IsUnique()
           ;
            modelBuilder.Entity<BeverageCategory>().ToTable("BeverageCategory");
            modelBuilder.Entity<Sale>().ToTable("Sale");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Purchase>().ToTable("Purchase");
            modelBuilder.Entity<DuePay>().ToTable("DuePay");
            modelBuilder.Entity<SalesReturn>().ToTable("SalesReturn");
            modelBuilder.Entity<Vat>().ToTable("Vat");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<BeverageCategory>().ToTable("BeverageCategory");
            modelBuilder.Entity<LedgerTransaction>().ToTable("LedgerTransaction");
            modelBuilder.Entity<LedgerAccount>().ToTable("LedgerAccount");
            modelBuilder.Entity<LedgerTransactionDetail>().ToTable("LedgerTransactionDetail");
            modelBuilder.Entity<LedgerAccountBalance>().ToTable("LedgerAccountBalance");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<FiscalYear>().ToTable("FiscalYear");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<SaleOrder>().ToTable("SaleOrder");
            modelBuilder.Entity<Administration>().ToTable("Administration");
            DatabaseSeeder.Seed(modelBuilder); 
        }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<BeverageCategory> BeverageCategories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<DuePay> DuePays { get; set; }
        public DbSet<SalesReturn> SalesReturns { get; set; }
        public DbSet<Vat> Vats { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<LedgerTransaction> LedgerTransactions { get; set; }
        public DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public DbSet<AccountClass> AccountClasses { get; set; }
        public DbSet<LedgerTransactionDetail> LedgerTransactionDetails { get; set; }
        public DbSet<LedgerGeneral> Ledgergenerals { get; set; }
        public DbSet<LedgerAccountBalance> LedgerAccountBalances { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<Administration> Administrations { get; set; }
    }
}

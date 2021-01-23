using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
  public class WineContext:DbContext
    {
        public WineContext():base("WineContext")
        {
            Database.SetInitializer<WineContext>(new CreateDatabaseIfNotExists<WineContext>());
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

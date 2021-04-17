using EvanApi1.models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
namespace EvanApi1.Data
{
    public class CoopContext : DbContext
    {
        public CoopContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=evandb.cjiabok62vq8.us-east-1.rds.amazonaws.com,port=3306;database=evan;user=mysqluser;password=mysqlpassword");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<AccountType>().ToTable("AccountType");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            DatabaseSeeder.Seed(modelBuilder); 
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
    }
}

using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class PurchaseService : DbService, IPurchaseService
    {
        public Task<int> DeleteBeverageAsync(Beverage Beverage)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePurchaseAsync(Purchase Purchase)
        {
            if (Purchase != null)
            {
                Context.Purchases.Remove(Purchase);

            }
            return await Context.SaveChangesAsync();
        }

        public Task<int> DeletePurchaseAsync(Invoice Invoice)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSupplierAsync(Supplier Supplier)
        {
            throw new NotImplementedException();
        }

        public Task<Beverage> FindBeverageByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> FindInvoiceByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Purchase> FindPurchaseByIDAsync(int id)
        {
            return await Context.Purchases.Where(a => a.PurchaseId == id).SingleOrDefaultAsync();
        }

        public Task<Supplier> FindSupplierByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Beverage>> GetAllBeveragesAsync()
        {

            try
            {
                return await Context.Beverages.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await Context.Invoices.ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesAsync()
        {
            return await Context.Purchases.ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            try
            {
                return await Context.Suppliers.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int SaveBeverageAsync(Beverage Beverage)
        {
            throw new NotImplementedException();
        }

        public int SavePurchase(Purchase Purchase)
        {
            if (Purchase == null)
            {
                return 0;
            }
            if (Purchase.PurchaseId < 1)
            {
                Context.Purchases.Add(Purchase);

            }
            return Context.SaveChanges();

        }

        public int SaveInvoice(Invoice Invoice)
        {
            if (Invoice == null)
            {
                return 0;
            }
            if (Invoice.InvoiceId < 1)
            {
                Context.Invoices.Add(Invoice);

            }
            return Context.SaveChanges();

        }

        public Task<int> SaveSupplierAsync(Supplier Supplier)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Purchase>> FindPurchasesByDateAsync(DateTime Start, DateTime End)
        {
            return await Context.Purchases.Where(a => DbFunctions.TruncateTime(a.PurchaseDate) >= Start.Date && DbFunctions.TruncateTime(a.PurchaseDate) <= End.Date)
                .ToListAsync();
        }

        public async Task<int> GetBeverageStockAsync(Beverage Beverage)
        {
            var purchase = await Context.Purchases.Where(a => a.BeverageId == Beverage.BeverageId).Select(a => a.Quantity).DefaultIfEmpty().SumAsync();
            var sale = await Context.Sales.Where(a => a.BeverageId == Beverage.BeverageId).Select(a => a.Quantity).DefaultIfEmpty().SumAsync();
            var total = purchase - sale;
            return (total);
        }

        public async Task<IEnumerable<Purchase>> FindPurchasesByBeverageAsync(Beverage Beverage)
        {
            return await Context.Purchases.Where(a => a.Beverage.BeverageId == Beverage.BeverageId).ToListAsync();
        }

        public Task<IEnumerable<Purchase>> FindPurchaseTotalByBeverageAsync(Beverage Beverage)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalPurchaseAsync(Beverage Beverage)
        {
            return await Context.Purchases
                         .Where(a => a.BeverageId == Beverage.BeverageId).DefaultIfEmpty().Select(a => (a.Quantity)).SumAsync();

        }

        public async Task<IEnumerable<Purchase>> FindPurchasesByInvoiceAsync(int InvoiceNumber)
        {
            return await Context.Purchases
                         .Where(a => a.Invoice.InvoiceNumber == InvoiceNumber).ToListAsync();
        }

        public async Task<Invoice> FindInvoiceByInvoiceNumberAsync(int id)
        {
            return await Context.Invoices.Include("Purchases").Where(a => a.InvoiceNumber == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Purchase>> FindPurchasesByBeverageAndDateAsync(DateTime Start, DateTime End, Beverage Beverage = null)
        {
            if (Beverage == null)
                return await Context.Purchases
              .Where(a => DbFunctions.TruncateTime(a.PurchaseDate) >= Start.Date
               && DbFunctions.TruncateTime(a.PurchaseDate) <= End.Date)
              .ToListAsync();
            return await Context.Purchases
                .Where(a => a.Beverage.BeverageId == Beverage.BeverageId)
                .Where(a => DbFunctions.TruncateTime(a.PurchaseDate) >= Start.Date
                 && DbFunctions.TruncateTime(a.PurchaseDate) <= End.Date)
                .ToListAsync();
        }

        public Task<IEnumerable<Beverage>> GetAllBeveragesForChartAsync()
        {
            throw new NotImplementedException();
        }

        public int SavePurchases(Invoice Invoice)
        {


            if (Invoice == null)
            {
                return 0;
            }
            using (DbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    if (Invoice.InvoiceId < 1)
                    {
                        Context.Invoices.Add(Invoice);

                    }
                    var count = Context.SaveChanges();
                    transaction.Commit();
                    return count;
                }

                catch (Exception ex)
                {

                    transaction.Rollback();
                    if(ex.InnerException.InnerException.Message.Contains("IX_FirstAndSecond"))
                    {
                        MessageBox.Show("Purchase Already exist");
                        return 0;
                    }
                    throw ex;
                }
            }
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllBanksAsync()
        {
            try
            {
                return await Context.LedgerAccounts.Where(a => a.ParentLedgerAccountId == 7).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<decimal> GetInvoiceTotalAmountAsync(int InvoiceNumber)
        {
            return await Context.Invoices.Where(a => a.InvoiceNumber == InvoiceNumber).SumAsync(a => a.Total);
        }

        public async Task<decimal> GetPurchaseTotalAmountAsync(DateTime start, DateTime end)
        {
            return await Context.Purchases.Where(a =>DbFunctions.TruncateTime(a.PurchaseDate)>=start &&
            DbFunctions.TruncateTime(a.PurchaseDate) <= end)
            .SumAsync(a => a.LineTotalAmount);
        }

        public Task<List<Beverage>> GetAllBeveragesByCategoryAsync(int categoryid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BeverageCategory>> GetAllBeverageCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Beverage>> GetAllPopularBeveragesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

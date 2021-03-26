using Rms.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface IPurchaseService:IBeverageService,ISupplierService, IInvoiceService
    {
        Task<Purchase> FindPurchaseByIDAsync(int id);
        Task<IEnumerable<Purchase>> FindPurchasesByBeverageAsync(Beverage Beverage);
        Task<IEnumerable<Purchase>> FindPurchaseTotalByBeverageAsync(Beverage Beverage);
        Task<IEnumerable<Purchase>> FindPurchasesByDateAsync(DateTime Start,DateTime End);
        Task<IEnumerable<Purchase>> FindPurchasesByBeverageAndDateAsync(DateTime Start, DateTime End,Beverage Beverage);
        Task<IEnumerable<Purchase>> FindPurchasesByInvoiceAsync(int InvoiceNumber);
        Task<decimal> GetInvoiceTotalAmountAsync(int InvoiceNumber);
        Task<decimal> GetPurchaseTotalAmountAsync(DateTime start, DateTime end);
        Task<IEnumerable<Purchase>> GetAllPurchasesAsync();
        Task<IEnumerable<LedgerAccount>> GetAllBanksAsync();
        int SavePurchase(Purchase Purchase);
        Task<int> DeletePurchaseAsync(Purchase Purchase);
        Task<int> GetTotalPurchaseAsync(Beverage Beverage);
        Task<int> GetBeverageStockAsync(Beverage Beverage);
        int SavePurchases(Invoice Invoice);
        void CommitTransaction();



    }
}

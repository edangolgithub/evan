using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface ISalesService
    {
        Task<IEnumerable<Beverage>> GetAllBeveragesForChartAsync();
        Task<IEnumerable<Beverage>> GetAllBeveragesAsync();
        Task<IEnumerable<Beverage>> GetAllBeveragesByDrinkTypeAsync(Drinktype DType);
        Task<IEnumerable<Beverage>> GetAllBeveragesByName(string BeverageName);
        Task<Sale> FindSaleByIDAsync(int id);
        Task<IEnumerable<Sale>> FindSalesByBeverageAsync(Beverage Beverage);
        Task<IEnumerable<Sale>> FindSalesTotalByBeverageAsync(Beverage Beverage);
        Task<IEnumerable<Sale>> FindSalesByDateAsync(DateTime Start, DateTime End);
        Task<IEnumerable<Sale>> FindSalesByBeverageAndDateAsync(DateTime Start, DateTime End, Beverage Beverage);
        Task<IEnumerable<BeverageCategory>> GetAllBeverageCategoriesAsync();
        Task<List<Beverage>> GetAllPopularBeveragesAsync();
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<LedgerAccount>> GetAllCustomerAccountsAsync();
        Task<IEnumerable<LedgerAccount>> GetAllBanksAsync();
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<IEnumerable<SaleOrder>> GetAllSalesOrderAsync();
        int SaveSale(Sale Sale);
        int SaveSaleOrder(SaleOrder SaleOrder, string Mode, LedgerAccount SelectedCustomer,LedgerAccount SelectedBankAccount, string ChequeNumber);
        Task<int> DeleteSaleAsync(Sale Sale);
        Task<int> GetTotalSaleAsync(Beverage Beverage);
        Task<int> GetBeverageStockAsync(Beverage Beverage);
        Task<List<Beverage>> GetAllBeveragesByCategoryAsync(int categoryid);
    }
}

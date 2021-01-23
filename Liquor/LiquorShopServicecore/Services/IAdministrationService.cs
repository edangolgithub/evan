using Rms.Classes;
using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
    public interface IAdministrationService
    {
        Administration FindAdministrationByID(int id);
        IEnumerable<Administration> GetAllAdministrations();
        Task<IEnumerable<Administration>> GetAllAdministrationsAsync();
        int SaveAdministration(Administration Administration);
        int DeleteAdministration(Administration Administration);
        Task<int> SaveBeverageCategoryAsync();
        //Task<IEnumerable<Item>> GetAllItemsForChartSettingsAsync();
        
        Task<IEnumerable<LedgerAccount>> GetAllLedgerAccountsForChartSettingsAsync();    

        void ResetAdministration();
        Task<IEnumerable<Company>> GetAllCompaniesAsync();

        Task<Company> GetSelectedCompanyAsync();
        Task<Company> GetCurrentCompanyAsync(string company,string password);
        int SaveCompany(Company Company,FiscalYear fy);

        Task<User> FindUserByUserNameAndPassword(string u, string p);
        bool DeleteAndRefreshDatabase();
       
        WineContext GetCurrentContext();
        Task<IEnumerable<BeverageCategory>> getAllBeverageCategoriesAsync();
        Task<IEnumerable<Beverage>> GetAllBeveragesAsync();
        //int SaveBeverageAsync(Beverage thisitem, Beverage flag1);
        //int SaveBeverageAsync(IGrouping<string, Rms.Classes.MenuItem> bevcat);
        Task<int> SaveBeverageAsync(List<MenuItem> MenuItems);
        int DeleteCompany(Company selectedCompany);
    }
}

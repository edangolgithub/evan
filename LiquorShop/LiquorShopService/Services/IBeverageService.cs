using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface IBeverageService
    {
        Task<Beverage> FindBeverageByIDAsync(int id);
        Task<IEnumerable<Beverage>> GetAllBeveragesAsync();
        Task<IEnumerable<Beverage>> GetAllBeveragesForChartAsync();
        int SaveBeverageAsync(Beverage Beverage);
        Task<int> DeleteBeverageAsync(Beverage Beverage);
        Task<List<Beverage>> GetAllBeveragesByCategoryAsync(int categoryid);
        Task<IEnumerable<BeverageCategory>> GetAllBeverageCategoriesAsync();
        Task<List<Beverage>> GetAllPopularBeveragesAsync();
    }
}

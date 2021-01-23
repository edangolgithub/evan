using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class BeverageService : DbService,IBeverageService
    {
        public async Task<int> DeleteBeverageAsync(Beverage Beverage)
        {
               Context.Beverages.Remove(Beverage);
            return await Context.SaveChangesAsync();
        }

        public Task<Beverage> FindBeverageByIDAsync(int id)
        {
            return Context.Beverages.FindAsync(id);
        }

        public async Task<IEnumerable<Beverage>> GetAllBeveragesAsync()
        {
            return await Context.Beverages.OrderBy(a=>a.Name).ToListAsync();
        }

        public async Task<IEnumerable<Beverage>> GetAllBeveragesForChartAsync()
        {
            return await Context.Beverages.Where(a => a.ShowInChart).ToListAsync();
        }

        public  int SaveBeverageAsync(Beverage Beverage)
        {
           if(Beverage.BeverageId<1)
            {
                Context.Beverages.Add(Beverage);
            }
            return  Context.SaveChanges();
        }
        public async Task<List<Beverage>> GetAllPopularBeveragesAsync()
        {
            return await Context.Beverages.Where(a => a.IsPopular).ToListAsync();
        }

        public async Task<IEnumerable<BeverageCategory>> GetAllBeverageCategoriesAsync()
        {
            return await Context.BeverageCategories.ToListAsync();
        }

        public async Task<List<Beverage>> GetAllBeveragesByCategoryAsync(int categoryid)
        {
            return await Context.Beverages.Where(a => a.BeverageCategoryId == categoryid).ToListAsync();
        }
    }
}

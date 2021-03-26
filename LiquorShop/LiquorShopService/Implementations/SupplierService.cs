using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquorShopService.Services;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class SupplierService : DbService,ISupplierService
    {
        public async Task<int> DeleteSupplierAsync(Supplier Supplier)
        {
            Context.Suppliers.Remove(Supplier);
            return await Context.SaveChangesAsync();
        }

        public async Task<Supplier> FindSupplierByIDAsync(int id)
        {
            return await Context.Suppliers.FindAsync(id);
        }

        async public Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await Context.Suppliers.ToListAsync();
        }
        async public Task<int> SaveSupplierAsync(Supplier Supplier)
        {
            if (Supplier.SupplierId < 1)
            {
                Context.Suppliers.Add(Supplier);
                await Context.SaveChangesAsync();
                Context.LedgerAccounts.Add(new LedgerAccount { AccountClassId = 4, AccountName = Supplier.SupplierName, ParentLedgerAccountId = 5, ShowInChart = true,CompanyId=1 });
                return await Context.SaveChangesAsync();
            }
            else
            {
                Context.Entry(Supplier).State = EntityState.Modified;
                return await Context.SaveChangesAsync();
            }
            


        }
    }
}

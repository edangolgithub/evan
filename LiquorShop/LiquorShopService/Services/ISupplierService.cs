using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface ISupplierService
    {
        Task<Supplier> FindSupplierByIDAsync(int id);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<int> SaveSupplierAsync(Supplier Supplier);
        Task<int> DeleteSupplierAsync(Supplier Supplier);
    }
}

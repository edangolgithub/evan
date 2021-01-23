using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface ICustomerService
    {
        Task<Customer> FindCustomerByIDAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> SaveCustomerAsync(Customer Customer);
        Task<int> DeleteCustomerAsync(Customer Customer);
    }
}

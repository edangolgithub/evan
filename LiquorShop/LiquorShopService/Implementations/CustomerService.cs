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
    public class CustomerService : DbService,ICustomerService
    {
        public async Task<int> DeleteCustomerAsync(Customer Customer)
        {
            Context.Customers.Remove(Customer);
            return await Context.SaveChangesAsync();
        }

        public async Task<Customer> FindCustomerByIDAsync(int id)
        {
            return await Context.Customers.FindAsync(id);
        }

        async public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await Context.Customers.ToListAsync();
        }
        async public Task<int> SaveCustomerAsync(Customer Customer)
        {
            if (Customer.CustomerId < 1)
            {
                Context.Customers.Add(Customer);
                Context.LedgerAccounts.Add(new LedgerAccount { AccountClassId = 1, AccountName = Customer.CustomerName, ParentLedgerAccountId = 4, ShowInChart = true, CompanyId = 1 });
                return await Context.SaveChangesAsync();

            }
            else
            {
                Context.Entry(Customer).State = EntityState.Modified;
            }
            
            return await Context.SaveChangesAsync();
          
        }
    }
}

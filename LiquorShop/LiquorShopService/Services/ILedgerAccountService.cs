using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface ILedgerAccountService
    {

        IEnumerable<AccountClass> GetAllAccountClasses();
        int InsertAccountClass(AccountClass AccountClass);
        int DeleteAccountClass(AccountClass AccountClass);
        int SaveAccountClass(AccountClass AccountClass);
        IEnumerable<LedgerAccount> GetAllLedgerAccounts();

        LedgerAccount GetLedgerAccountByName(string AccountName);
        int InsertLedgerAccount(LedgerAccount LedgerAccount);
        int DeleteLedgerAccount(LedgerAccount LedgerAccount);
        int SaveLedgerAccount(LedgerAccount LedgerAccount);


        Task<IEnumerable<AccountClass>> GetAllAccountClassesAsync();
      
        Task<int> DeleteAccountClassAsync(AccountClass AccountClass);
        Task<int> SaveAccountClassAsync(AccountClass AccountClass);


        Task<IEnumerable<LedgerAccount>> GetAllLedgerAccountsAsync();
        Task<IEnumerable<LedgerAccount>> GetAllLedgerAccountsForChartAsync();

        Task<int> DeleteLedgerAccountAsync(LedgerAccount LedgerAccount);
        Task<int> SaveLedgerAccountAsync(LedgerAccount LedgerAccount);
    }
}

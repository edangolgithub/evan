using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface IAccountingService:ILedgerAccountService,ILedgerTransactionService
    {
       int SaveAll();
        Task<int> SaveAllAsync();

        Task<User> CreateFakeUserAsync();
      
    }
}

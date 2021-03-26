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

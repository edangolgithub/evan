using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShopService.Services
{
   public interface ILedgerTransactionService
    {
        IEnumerable<LedgerGeneral> GetAllLedgerGenerals();
        int InsertLedgerGeneral(LedgerGeneral Ledgergeneral);
        int DeleteLedgerGeneral(LedgerGeneral Ledgergeneral);
        int SaveLedgerGeneral(LedgerGeneral Ledgergeneral);
        IEnumerable<LedgerTransaction> GetAllLedgerTransactions();
        int InsertLedgerTransaction(LedgerTransaction LedgerTransaction);
        int DeleteLedgerTransaction(LedgerTransaction LedgerTransaction);
        int SaveLedgerTransaction(LedgerTransaction LedgerTransaction);
        IEnumerable<LedgerTransactionDetail> GetAllLedgerTransactionDetails();
        int InsertLedgerTransactionDetail(LedgerTransactionDetail LedgerTransactionDetail);
        int DeleteLedgerTransactionDetail(LedgerTransactionDetail LedgerTransactionDetail);
        int SaveLedgerTransactionDetail(LedgerTransactionDetail LedgerTransactionDetail);


        Task<IEnumerable<LedgerGeneral>> GetAllLedgerGeneralsAsync();
        
        Task<int> DeleteLedgerGeneralAsync(LedgerGeneral Ledgergeneral);
        Task<int> SaveLedgerGeneralAsync(LedgerGeneral Ledgergeneral);
       Task<IEnumerable<LedgerTransaction>> GetAllLedgerTransactionsAsync();
      
        Task<int> DeleteLedgerTransactionAsync(LedgerTransaction LedgerTransaction);
        Task<int> SaveLedgerTransactionAsync(LedgerTransaction LedgerTransaction);
       Task<IEnumerable<LedgerTransactionDetail>> GetAllLedgerTransactionDetailsAsync();
      
        Task<int> DeleteLedgerTransactionDetailAsync(LedgerTransactionDetail LedgerTransactionDetail);
        Task<int> SaveLedgerTransactionDetailAsync(LedgerTransactionDetail LedgerTransactionDetail);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rms.Data;
using System.Data.Entity;
using LiquorShopService.Services;
using TescoWineShopSql;

namespace LiquorShopService.Implementations
{
    public class AccountingService :DbService, IAccountingService
    {
        public DateTime FiscalYear { get; set; }
        public void ResolveFiscalYear()
        {
            FiscalYear = new DateTime(2018, 04, 01);
        }
    
       public AccountingService()
        {
          Task.Run(()=> ResolveFiscalYear());
        }
        public int DeleteAccountClass(AccountClass AccountClass)
        {
            Context.AccountClasses.Remove(AccountClass);
            return SaveAll();
        }

        public int DeleteLedgerAccount(LedgerAccount LedgerAccount)
        {
            Context.LedgerAccounts.Remove(LedgerAccount);
            return SaveAll();
        }

        public int DeleteLedgerGeneral(LedgerGeneral Ledgergeneral)
        {
            Context.Ledgergenerals.Remove(Ledgergeneral);
            return SaveAll();
        }

        public int DeleteLedgerTransaction(LedgerTransaction LedgerTransaction)
        {
            Context.LedgerTransactions.Remove(LedgerTransaction);
            return SaveAll();
        }

        public int DeleteLedgerTransactionDetail(LedgerTransactionDetail LedgerTransactionDetail)
        {
            Context.LedgerTransactionDetails.Remove(LedgerTransactionDetail);
            return SaveAll();
        }

        public IEnumerable<AccountClass> GetAllAccountClasses()
        {
            return Context.AccountClasses.ToList();
        }

        public IEnumerable<LedgerAccount> GetAllLedgerAccounts()
        {
            return Context.LedgerAccounts.ToList();
        }

        public IEnumerable<LedgerGeneral> GetAllLedgerGenerals()
        {
            return Context.Ledgergenerals.Where(a=>a.JournalEntryDate>=FiscalYear.Date).ToList();
        }

        public IEnumerable<LedgerTransactionDetail> GetAllLedgerTransactionDetails()
        {
            return Context.LedgerTransactionDetails.Include(async=>async.LedgerTransaction).Where(a => a.LedgerTransaction.Date >= FiscalYear.Date).ToList();
        }

        public IEnumerable<LedgerTransaction> GetAllLedgerTransactions()
        {
            return Context.LedgerTransactions.Where(a => a.Date>= FiscalYear.Date).ToList();
        }

        public int InsertAccountClass(AccountClass AccountClass)
        {
            Context.AccountClasses.Add(AccountClass);
            return SaveAll();
        }

        public int InsertLedgerAccount(LedgerAccount LedgerAccount)
        {
            Context.LedgerAccounts.Add(LedgerAccount);
            return SaveAll();
        }

        public int InsertLedgerGeneral(LedgerGeneral Ledgergeneral)
        {
            Context.Ledgergenerals.Add(Ledgergeneral);
            return SaveAll();
        }

        public int InsertLedgerTransaction(LedgerTransaction LedgerTransaction)
        {
            Context.LedgerTransactions.Add(LedgerTransaction);
            return SaveAll();
        }

        public int InsertLedgerTransactionDetail(LedgerTransactionDetail LedgerTransactionDetail)
        {
            Context.LedgerTransactionDetails.Add(LedgerTransactionDetail);
            return SaveAll();
        }

        public int SaveAccountClass(AccountClass AccountClass)
        {
           if(AccountClass.AccountClassId<1)
            {
                Context.AccountClasses.Add(AccountClass);
            }
            else
            {
                Context.Entry(AccountClass).State = System.Data.Entity.EntityState.Modified;
            }
            return SaveAll();
        }

        public int SaveLedgerAccount(LedgerAccount LedgerAccount)
        {
            if (LedgerAccount.AccountClassId < 1)
            {
                Context.LedgerAccounts.Add(LedgerAccount);
            }
            else
            {
                Context.Entry(LedgerAccount).State = System.Data.Entity.EntityState.Modified;
            }
            return SaveAll();
        }

        public int SaveLedgerGeneral(LedgerGeneral Ledgergeneral)
        {
            if (Ledgergeneral.LedgergeneralId < 1)
            {
                Context.Ledgergenerals.Add(Ledgergeneral);
            }
            else
            {
                Context.Entry(Ledgergeneral).State = System.Data.Entity.EntityState.Modified;
            }
            return SaveAll();
        }

        public int SaveLedgerTransaction(LedgerTransaction LedgerTransaction)
        {
            if (LedgerTransaction.LedgerTransactionId < 1)
            {
                Context.LedgerTransactions.Add(LedgerTransaction);
            }
            else
            {
                Context.Entry(LedgerTransaction).State = System.Data.Entity.EntityState.Modified;
            }
            return SaveAll();
        }

        public int SaveLedgerTransactionDetail(LedgerTransactionDetail LedgerTransactionDetail)
        {
            if (LedgerTransactionDetail.LedgerTransactionDetailId < 1)
            {
                Context.LedgerTransactionDetails.Add(LedgerTransactionDetail);
            }
            else
            {
                Context.Entry(LedgerTransactionDetail).State = System.Data.Entity.EntityState.Modified;
            }
            return SaveAll();
        }

      public int SaveAll()
        {
            return Context.SaveChanges();
        }

        public   async Task<int> SaveAllAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountClass>> GetAllAccountClassesAsync()
        {
            return await Context.AccountClasses.ToListAsync();
        }
        public async Task<int> DeleteAccountClassAsync(AccountClass AccountClass)
        {
            Context.AccountClasses.Remove(AccountClass);
            return await SaveAllAsync();
        }

        public async Task<int> SaveAccountClassAsync(AccountClass AccountClass)
        {
           if(AccountClass.AccountClassId<1)
            {
                Context.AccountClasses.Add(AccountClass);
            }
            else
            {
                Context.Entry(AccountClass).State = EntityState.Modified;
            }
            return await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllLedgerAccountsAsync()
        {
            return await Context.LedgerAccounts.ToListAsync();
        }

      
        public async Task<int> DeleteLedgerAccountAsync(LedgerAccount LedgerAccount)
        {
            Context.LedgerAccounts.Remove(LedgerAccount);
            return await SaveAllAsync();
        }

        public async Task<int> SaveLedgerAccountAsync(LedgerAccount LedgerAccount)
        {
            if (LedgerAccount.LedgerAccountId < 1)
            {
                Context.LedgerAccounts.Add(LedgerAccount);
            }
            else
            {
                Context.Entry(LedgerAccount).State = EntityState.Modified;
            }
            return await SaveAllAsync();
        }

        public async Task<IEnumerable<LedgerGeneral>> GetAllLedgerGeneralsAsync()
        {
            return await Context.Ledgergenerals.Where(a => a.JournalEntryDate >= FiscalYear.Date).ToListAsync();
        }



        public async Task<int> DeleteLedgerGeneralAsync(LedgerGeneral Ledgergeneral)
        {
            Context.Ledgergenerals.Remove(Ledgergeneral);
            return await SaveAllAsync();
        }

        public async Task<int> SaveLedgerGeneralAsync(LedgerGeneral Ledgergeneral)
        {
            if (Ledgergeneral.LedgergeneralId < 1)
            {
                Context.Ledgergenerals.Add(Ledgergeneral);
            }
            else
            {
                Context.Entry(Ledgergeneral).State = EntityState.Modified;
            }
            return await SaveAllAsync();
        }

        public async Task<IEnumerable<LedgerTransaction>> GetAllLedgerTransactionsAsync()
        {
            return await Context.LedgerTransactions.Where(a => a.Date >= FiscalYear.Date).ToListAsync();
        }


        public async Task<int> DeleteLedgerTransactionAsync(LedgerTransaction LedgerTransaction)
        {
            Context.LedgerTransactions.Remove(LedgerTransaction);
            return await SaveAllAsync();
        }

        public async Task<int> SaveLedgerTransactionAsync(LedgerTransaction LedgerTransaction)
        {
            if (LedgerTransaction.LedgerTransactionId < 1)
            {
                Context.LedgerTransactions.Add(LedgerTransaction);
            }
            else
            {
                Context.Entry(LedgerTransaction).State = EntityState.Modified;
            }
            return await SaveAllAsync();
        }

        public async Task<IEnumerable<LedgerTransactionDetail>> GetAllLedgerTransactionDetailsAsync()
        {
            return await Context.LedgerTransactionDetails.Include(async => async.LedgerTransaction).Where(a => a.LedgerTransaction.Date >= FiscalYear.Date).ToListAsync();
        }


        public async Task<int> DeleteLedgerTransactionDetailAsync(LedgerTransactionDetail LedgerTransactionDetail)
        {
            Context.LedgerTransactionDetails.Remove(LedgerTransactionDetail);
            return await SaveAllAsync();
        }

        public Task<int> SaveLedgerTransactionDetailAsync(LedgerTransactionDetail LedgerTransactionDetail)
        {
            if(LedgerTransactionDetail.LedgerTransactionDetailId<1)
            {
                Context.LedgerTransactionDetails.Add(LedgerTransactionDetail);
            }
            else
            {
                Context.Entry(LedgerTransactionDetail).State = EntityState.Modified;
            }
            return SaveAllAsync();
        }

        public async Task<IEnumerable<LedgerAccount>> GetAllLedgerAccountsForChartAsync()
        {
            return await Context.LedgerAccounts.Where(a=>a.ShowInChart==true).ToListAsync();
        }
        public async Task<User> CreateFakeUserAsync()
        {
            return await Context.Users.FirstOrDefaultAsync();
        }

        public LedgerAccount GetLedgerAccountByName(string AccountName)
        {
            return Context.LedgerAccounts.Where(a => a.AccountName == AccountName).FirstOrDefault();
        }
    }
}

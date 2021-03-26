using Rms.Data;
using Rms.Reports;
using Rms.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace Rms.Classes
{
   public class LedgerData
    {
        RmsContext context = new RmsContext();
        public ObservableCollection<Ledgergeneral> v { get; set; }
        ObservableCollection<LedgerAccount> acs;//= new ObservableCollection<Data.LedgerAccount>(context.LedgerAccounts.ToList());
        
        public LedgerTAccount GetLedgerData(int LedgerTransactionId,RmsContext context)
        {
            LedgerTAccount Lta = new LedgerTAccount();
         acs = new ObservableCollection<Data.LedgerAccount>(context.LedgerAccounts.ToList());
             v = new ObservableCollection<Ledgergeneral>
                (context.Ledgergenerals.Where(a=>a.LedgerTransactionId==LedgerTransactionId)
                .ToList());
            foreach (var item in v)
            {
                if(item.Debit>0)
                {
                    Lta.DebitAccounts.Add(item.LedgerAccount);
                }
                if (item.Credit>  0)
                {
                    Lta.CreditAccounts.Add(item.LedgerAccount);
                }
            }
            return Lta;
        }

        public void GetSingleLedger(int LedgerAccountId)
        {
            acs= new ObservableCollection<Data.LedgerAccount>(context.LedgerAccounts.ToList());
            var lg = context.Ledgergenerals.Where(a => a.LedgerAccountId == LedgerAccountId)
                .Join(context.LedgerTransactions, a => a.LedgerTransactionId, b => b.LedgerTransactionId,
                 (a, b) => new{ a, b }).Select(x=>x.b.LedgerGenerals);
            ObservableCollection<Ledgergeneral> glist = new ObservableCollection<Ledgergeneral>();
            foreach (var item in lg)
            {
                foreach (var i in item)
                {
                    glist.Add(i);
                }
            }
            List<TAccountLedger> ls = new List<TAccountLedger>();
            List<Ledgergeneral> gl = new List<Data.Ledgergeneral>();
            foreach (var item in glist)
            {
                if(item.LedgerAccountId!=LedgerAccountId)
                {
                    gl.Add(item);
                }
            }
            foreach (var item in gl)
            {
                TAccountLedger tas = new Reports.TAccountLedger();
                if (item.Credit > 0)
                {
                    tas.DebitSideAccount = item.LedgerAccount.AccountName;
                    tas.DebitAmount = item.Credit;
                }
                if(item.Debit>0)
                {
                    tas.CreditSideAccount = item.LedgerAccount.AccountName;
                    tas.CreditAmount = item.Debit;
                }
                tas.Date = item.JournalEntryDate.ToShortDateString();
                tas.Account = acs.Where(a => a.LedgerAccountId == LedgerAccountId).Select(a => a.AccountName).FirstOrDefault();
                ls.Add(tas);
            }
            TAccountsLedgerReport rpt = new TAccountsLedgerReport();
            rpt.SetDataSource(ls);
            w ww = new Views.w();
            ww.Show();
        }
    }

    

    public class LedgerTAccount
    {
        public int LedgerTaccountId { get; set; }
      
        public List<LedgerAccount> DebitAccounts { get; set; }
        public List<LedgerAccount> CreditAccounts { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public LedgerTAccount()
        {
            DebitAccounts = new List<Data.LedgerAccount>();
            CreditAccounts = new List<Data.LedgerAccount>();
        }
    }
}

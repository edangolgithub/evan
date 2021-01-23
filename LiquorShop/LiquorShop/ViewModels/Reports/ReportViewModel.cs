using Rms.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;
using Rms.Reports;
using Rms.Views;
using System.Globalization;

namespace Rms.ViewModels
{
   public class ReportViewModel:ViewModelBase
    {
       // Reports.Report report = new Reports.Report();
        private string _ReportAvailable;

        public string ReportAvailable
        {
            get { return _ReportAvailable; }
            set
            {
                _ReportAvailable = value;

                NotifyPropertyChanged("ReportAvailable");
            }
        }
        public ReportViewModel():base()
        {
            
            JournalDate = DateTime.Today;
            ReportAvailable = "View Report";
            Task.Run(() => this.Init()).ContinueWith((a) => ReportAvailable = "View Report").Wait();
        }
        private void Init()
        {
            try
            {
                context = new RmsContext();
                LedgerTransactions = new ObservableCollection<LedgerTransaction>(context.LedgerTransactions.ToList());
                LedgerAccounts = new ObservableCollection<LedgerAccount>();
                LedgerAccounts.Add(new Data.LedgerAccount { AccountName = "Initializing Data..." });
                LedgerAccounts = new ObservableCollection<LedgerAccount>(context.LedgerAccounts.ToListAsync().Result);
                LedgerTransactionDetails = new ObservableCollection<LedgerTransactionDetail>(GetLedgerTransactionDetailAsync().Result);
                Ledgergenerals = new ObservableCollection<Ledgergeneral>(GetLedgersAsync().Result);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        LedgerAccount _SelectedLedgerAccount;
        public LedgerAccount SelectedLedgerAccount
        {
            get
            {
                return _SelectedLedgerAccount;
            }
            set
            {
                if (_SelectedLedgerAccount != value)
                {
                    _SelectedLedgerAccount = value;
                    notify("SelectedLedgerAccount");
                }
            }
        }
        #region report

        public IEnumerable<LedgerJournalVm> GetReport(int LedgerAccountId)
        {
            //IEnumerable<LedgerJournalVm> v;
            //v = GetLedgerReport(LedgerAccountId);
            //if (v.Count() > 0)
            //{
            //    Reports.Ledger r = new Reports.Ledger();
            //    r.SetDataSource(v);
            //    GeneralReport rp = new GeneralReport(r);
            //    rp.Show();
            //}
            //else
            //{
            //    Application.Current.Shutdown();
            //}

          var reports=  GetLedgerReport(LedgerAccountId);
            return reports;
        }
        public IEnumerable<LedgerJournalVm> GetLedgerReport(int LedgerAccountId)
        {
            ResolveBalance("ds", "sds", 1, 1);

            return Ledgergenerals.Where(a => a.LedgerAccountId == LedgerAccountId)
                .Where(a => a.JournalEntryDate >= DateTime.Parse("10/11/2017",DateTimeFormatInfo.InvariantInfo) && a.JournalEntryDate <= DateTime.Parse("12/20/2018", DateTimeFormatInfo.InvariantInfo))
                .OrderBy(a => a.JournalEntryDate)
                .Select(a => new LedgerJournalVm
                {

                    TransactionId = a.LedgerTransactionId,
                    Date = a.JournalEntryDate.Date,
                    LedgerAccount = a.LedgerAccount.AccountName,
                    Debit = a.Debit,//== 0 ? "" : a.Debit.ToString(),
                    Credit = a.Credit,// == 0 ? "" : a.Credit.ToString(),
                    Particulars = LedgerTransactions.Where(b => b.LedgerTransactionId == a.LedgerTransactionId).Select(b => b.Description).FirstOrDefault(),
                    OtherLedgerAccount = ResolveOpp(a.LedgerTransactionId, a.LedgerAccountId),
                    StartDate = DateTime.Parse("10/11/2017", DateTimeFormatInfo.InvariantInfo),
                    EndDate = DateTime.Parse("12/21/2018", DateTimeFormatInfo.InvariantInfo),
                    // Balance=ResolveBalance("10/11/2017", "21/12/2017", a.LedgerTransactionId, a.LedgerAccountId)

                }).ToList();
        }
        private string ResolveBalance(string StartDate, string EndDate, int transactionId, int ledgerAccountId)
        {

            var v = Ledgergenerals
                .Where(a => a.JournalEntryDate >= DateTime.Parse("10/11/2017") && a.JournalEntryDate <= DateTime.Parse("10/12/2017"))
               .Where(a => a.LedgerAccountId == 1);

            List<Ledgergeneral> ldd = new List<Ledgergeneral>();

            List<OppAccount> oppacounts = new List<ViewModels.OppAccount>();
            foreach (var item in v)
            {
                ldd.Add(item);
            }
            decimal creditsum = ldd.Sum(a => a.Credit);
            decimal debitsum = ldd.Sum(a => a.Debit);
            string balance = "";
            if (creditsum > debitsum)
            {
                balance = "cBalance b/d" + (creditsum - debitsum).ToString();
            }
            if (debitsum > creditsum)
            {
                balance = "cBalance b/d" + (debitsum - creditsum).ToString();
            }
            return balance;
        }

        public string ResolveOpp(int transactionId, int ledgerAccountId)
        {
            var v = context.LedgerTransactions.Where(a => a.LedgerTransactionId == transactionId);
            List<Ledgergeneral> ld = new List<Ledgergeneral>();
            List<Ledgergeneral> ldd = new List<Ledgergeneral>();
            Ledgergeneral thisledger = new LedgerGeneral();
            List<OppAccount> oppacounts = new List<ViewModels.OppAccount>();
            foreach (var item in v)
            {
                foreach (var i in item.LedgerGenerals)
                {
                    ldd.Add(i);
                    if (i.LedgerAccountId == ledgerAccountId)
                    {
                        thisledger = i;
                        continue;
                    }
                    ld.Add(i);
                }
            }
            StringBuilder sb = new StringBuilder();
            Ledgergeneral maxdebit = new LedgerGeneral();
            Ledgergeneral maxcredit = new LedgerGeneral();
            maxdebit = ldd.Where(a => a.Debit == ldd.Max(b => b.Debit)).FirstOrDefault();
            maxcredit = ldd.Where(a => a.Credit == ldd.Max(b => b.Credit)).FirstOrDefault();
            bool debitbigger = false;
            if (maxdebit.Debit > maxcredit.Credit)
            {
                debitbigger = true;
            }
            else if (maxcredit.Credit > maxdebit.Debit)
            {
                debitbigger = false;
            }


            if (thisledger.Credit > 0)
            {
                if (thisledger == maxcredit)
                {
                    if (debitbigger == false)
                    {
                        foreach (var item in ld)
                        {
                            if (item.Credit > 0)
                            {
                                sb.AppendLine(item.Credit.ToString());
                            }
                            if (item.Debit > 0)
                            {
                                sb.AppendLine(item.Debit.ToString());
                            }

                        }
                    }
                    else
                    {
                        sb.AppendLine(thisledger.Credit.ToString());
                        return sb.ToString();
                    }
                }
                else
                {
                    sb.AppendLine(thisledger.Credit.ToString());
                    return sb.ToString();
                }

            }
            else if (thisledger.Debit > 0)
            {
                if (thisledger == maxdebit)
                {
                    if (debitbigger == true)
                    {
                        foreach (var item in ld)
                        {
                            if (item.Credit > 0)
                            {
                                sb.AppendLine(item.Credit.ToString());
                            }
                            if (item.Debit > 0)
                            {
                                sb.AppendLine(item.Debit.ToString());
                            }

                        }
                    }
                    else
                    {
                        sb.AppendLine(thisledger.Debit.ToString());
                        return sb.ToString();
                    }
                }
                else
                {
                    sb.AppendLine(thisledger.Debit.ToString());
                    return sb.ToString();
                }

            }

            return sb.ToString();
        }
        #endregion
    }
}

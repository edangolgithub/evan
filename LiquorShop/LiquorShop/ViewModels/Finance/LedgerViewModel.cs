using EvanDangol.Reporting;
using Rms.Classes;
using Rms.Data;
using Rms.Reports;
using LiquorShopService.Services;
//using Rms.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TescoWineShopSql;

namespace Rms.ViewModels.Accounts
{
  public partial class FinanceViewModel 
    {
        string _s;
        public string s
        {
            get
            {
                return _s;
            }
            set
            {
                if (_s != value)
                {
                    _s = value;
                    notify("s");
                }
            }
        }

     
        public void initledgervm()
        {
           
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;           
          
            IsBalanceAvailable = false;
            
        }


        ObservableCollection<LedgerJournalVm> _JournalVms;
        public ObservableCollection<LedgerJournalVm> JournalVms
        {
            get
            {
                return _JournalVms;
            }
            set
            {
                if (_JournalVms != value)
                {
                    _JournalVms = value;
                    notify("JournalVms");
                }
            }
        }


        private string _LedgerName;
        public string LedgerName
        {
            get { return _LedgerName; }
            set
            {
                SetProperty<string>(ref _LedgerName, value);
            }
        }
        public IEnumerable<LedgerJournalVm> GetLedgerReport(int LedgerAccountId = 1)
        {
            var ledger = Ledgergenerals.Where(a => a.LedgerAccountId == LedgerAccountId).FirstOrDefault();
            if (ledger == null)
            {
                LedgerName = "No Ledger Found";
                return null;
            }
            else
            {
                LedgerName = Ledgergenerals.Where(a => a.LedgerAccountId == LedgerAccountId).FirstOrDefault().LedgerAccount.AccountName + "  Ledger";
            }
                var data = Ledgergenerals.Where(a => a.LedgerAccountId == LedgerAccountId)
                .Where(a => a.JournalEntryDate.Date >= FromDate && a.JournalEntryDate.Date <= ToDate)
                .OrderBy(a => a.JournalEntryDate.Date)
                .Select(a => new LedgerJournalVm
                {

                    TransactionId = a.LedgerTransactionId,
                    Date = a.JournalEntryDate.Date,
                    LedgerAccount = a.LedgerAccount.AccountName,
                    DebitString = a.Debit > 0 ? ResolveOpp(a.LedgerTransactionId, a.LedgerAccountId) : "",
                    CreditString = a.Credit > 0 ? ResolveOpp(a.LedgerTransactionId, a.LedgerAccountId) : "",
                    Particulars = LedgerTransactions.Where(b => b.LedgerTransactionId == a.LedgerTransactionId).Select(b => b.Description).FirstOrDefault(),
                    // OtherLedgerAccount = ResolveOpp(a.LedgerTransactionId, a.LedgerAccountId),
                    Debit=a.Debit,
                    Credit=a.Credit,
                    StartDate = FromDate,
                    EndDate = ToDate,
                    // Balance=ResolveBalance("10/11/2017", "21/12/2017", a.LedgerTransactionId, a.LedgerAccountId)
                    Balance = GetBalance(a)
                }).ToList();


            data.Insert(0, new LedgerJournalVm { Balance = ResolveBalanceBroughtDown(LedgerAccountId) });


           // data.Insert(0, new LedgerJournalVm {  Balance = ResolveBalanceBroughtDown(LedgerAccountId) });


            JournalVms = new ObservableCollection<LedgerJournalVm>(data);

           
            CalculateTotals();
            return JournalVms;
        }

        ObservableCollection<BalanceViewModel> _BalanceViewModels;
        public ObservableCollection<BalanceViewModel> BalanceViewModels
        {
            get
            {
                return _BalanceViewModels;
            }
            set
            {
                if (_BalanceViewModels != value)
                {
                    _BalanceViewModels = value;
                    notify("BalanceViewModels");
                }
            }
        }
        private void GetDebitCreditBalance(int ledgerAccountId, out decimal debitbalance, out decimal creditbalance)
        {
            debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == ledgerAccountId)
                .Where(b => b.JournalEntryDate.Date < FromDate).Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == ledgerAccountId)
                     .Where(b => b.JournalEntryDate.Date < FromDate).Sum(b => b.Credit);

        }
        bool _IsBalanceAvailable;
        public bool IsBalanceAvailable
        {
            get
            {
                return _IsBalanceAvailable;
            }
            set
            {
                if (_IsBalanceAvailable != value)
                {
                    _IsBalanceAvailable = value;
                    notify("IsBalanceAvailable");
                }
            }
        }

        public void CalculateTotals()
        {
            BalanceViewModels = new ObservableCollection<BalanceViewModel>();
            decimal debitbalance = 0;
            decimal creditbalance = 0;
            var balance = 0m;
            string suffix = "";
            debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                 .Where(b => b.JournalEntryDate.Date <= ToDate).Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                     .Where(b => b.JournalEntryDate.Date <= ToDate).Sum(b => b.Credit);

            var debitbalancetotal = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                  .Sum(b => b.Debit);
            var creditbalancetotal = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                       .Sum(b => b.Credit);
            if (debitbalance > creditbalance)
            {
                balance = debitbalance - creditbalance;
                suffix = "Dr";
            }
            if (debitbalance < creditbalance)
            {
                balance = creditbalance - debitbalance;
                suffix = "Cr";
            }
            var balancetotal = 0m;
            if (debitbalancetotal > creditbalancetotal)
            {
                balancetotal = debitbalancetotal;

            }
            if (debitbalancetotal < creditbalancetotal)
            {
                balancetotal = creditbalancetotal;

            }
            BalanceViewModel balancevm1 = new BalanceViewModel
            {
                BalanceId = 1,
                TotalString = "Total             " + balancetotal.ToString("C")
            };
            BalanceViewModel balancevm2 = new BalanceViewModel
            {
                BalanceId = 2,

                TotalString = " Balance C/D      " + balance.ToString("C") + suffix
            };
            BalanceViewModels.Add(balancevm1);
            BalanceViewModels.Add(balancevm2);
            if (BalanceViewModels.Count() > 0)
            {
                IsBalanceAvailable = true;
            }
            else
            {
                IsBalanceAvailable = false;
            }
        }
        private string ResolveBalanceBroughtDown(int ledgerAccountId)
        {
            decimal debitbalance, creditbalance;
            GetDebitCreditBalance(ledgerAccountId, out debitbalance, out creditbalance);
            decimal balance = 0;
            string suffix = "";
            if (debitbalance > creditbalance)
            {
                balance = debitbalance - creditbalance;
                suffix = "Dr";
            }
            if (debitbalance < creditbalance)
            {
                balance = creditbalance - debitbalance;
                suffix = "Cr";
            }

            return "Balance B/F      " + balance.ToString() + suffix;
        }


        #region ExcelExportCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="ExcelExportCommand" />
        /// </summary>
        private RelayCommand _ExcelExportCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand ExcelExportCommand
        {
            get
            {
                if (_ExcelExportCommand == null)
                { _ExcelExportCommand = new RelayCommand(ExcelExportCommand_Execute, ExcelExportCommand_CanExecute); }

                return _ExcelExportCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="ExcelExportCommand" />
        /// </summary>
        private void ExcelExportCommand_Execute(object obj)
        {
            if (JournalVms == null)
            {

                MessageBox.Show("Nothing To Export");
                return;

            }
            if (JournalVms.Count < 0)
            {
                MessageBox.Show("Nothing To Export");
                return;
            }
            List<string> s = new List<string>();
            ExportToExcel<ExportToExcelData, List<ExportToExcelData>> ex = new ExportToExcel<ExportToExcelData, List<ExportToExcelData>>();
            List<ExportToExcelData> datas = new List<ExportToExcelData>();
            datas = JournalVms.Select(

                a => new ExportToExcelData
                {
                    Date =a.Date>DateTime.Today.AddYears(-100)? a.Date.ToShortDateString():"",
                    Particulars = a.Particulars,
                    //Debit =string.IsNullOrEmpty(a.DebitString)?"": a.DebitString.Substring(a.DebitString.IndexOf("-")),
                    //Credit =string.IsNullOrEmpty(a.CreditString)?"": a.CreditString.Substring(a.DebitString.IndexOf("-")),
                    Debit=a.Debit==0?"": a.Debit.ToString(),
                    Credit=a.Credit==0?"": a.Credit.ToString(),
                    Balance = a.Balance
                }).ToList();

            foreach (var item in BalanceViewModels)
            {
                datas.Add(new ExportToExcelData { Balance = item.TotalString });
            }
            ex.dataToPrint = datas;
            ex.GenerateReport();
        }

        /// <summary>
        /// Determines if <see cref="ExcelExportCommand" /> is allowed to execute
        /// </summary>
        private Boolean ExcelExportCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


     
        private string GetBalance(LedgerGeneral a)
        {
            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == a.LedgerAccountId).Where(b => b.JournalEntryDate <= a.JournalEntryDate).Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == a.LedgerAccountId).Where(b => b.JournalEntryDate <= a.JournalEntryDate).Sum(b => b.Credit);
            decimal balance = 0;
            string suffix = "";
            if (debitbalance > creditbalance)
            {
                balance = debitbalance - creditbalance;
                suffix = "Dr";
            }
            if (debitbalance < creditbalance)
            {
                balance = creditbalance - debitbalance;
                suffix = "Cr";
            }
            return balance.ToString() + " " + suffix;
        }



        //private string ResolveBalance(string StartDate, string EndDate, int transactionId, int ledgerAccountId)
        //{

        //    var v = Ledgergenerals
        //        .Where(a => a.JournalEntryDate >= DateTime.Parse("10/11/2017") && a.JournalEntryDate <= DateTime.Parse("10/12/2017"))
        //       .Where(a => a.LedgerAccountId == 1);

        //    List<Ledgergeneral> ldd = new List<Ledgergeneral>();

        //    List<OppAccount> oppacounts = new List<ViewModels.OppAccount>();
        //    foreach (var item in v)
        //    {
        //        ldd.Add(item);
        //    }
        //    decimal creditsum = ldd.Sum(a => a.Credit);
        //    decimal debitsum = ldd.Sum(a => a.Debit);
        //    string balance = "";
        //    if (creditsum > debitsum)
        //    {
        //        balance = "cBalance b/d" + (creditsum - debitsum).ToString();
        //    }
        //    if (debitsum > creditsum)
        //    {
        //        balance = "cBalance b/d" + (debitsum - creditsum).ToString();
        //    }
        //    return balance;
        //}

        public string ResolveOpp(int transactionId, int ledgerAccountId)
        {
            StringBuilder sb = new StringBuilder();
            LedgerGeneral thisledger = new LedgerGeneral();
            var v = LedgerTransactions.Where(a => a.LedgerTransactionId == transactionId);


            List<LedgerGeneral> ld = new List<LedgerGeneral>();
            List<LedgerGeneral> ldd = new List<LedgerGeneral>();

            //List<OppAccount> oppacounts = new List<ViewModels.OppAccount>();
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
            if (ldd.Count() > 2)
            {
                LedgerGeneral maxdebit = new LedgerGeneral();
                LedgerGeneral maxcredit = new LedgerGeneral();
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
                                    var name = item.LedgerAccount.AccountName;
                                    if (name.Length > 8)
                                    {
                                        name = name.Substring(0, 8);
                                    }
                                    sb.Append(name + "                   ");
                                    sb.AppendLine(item.Credit.ToString());
                                }
                                if (item.Debit > 0)
                                {
                                    var name = item.LedgerAccount.AccountName;
                                    if (name.Length > 8)
                                    {
                                        name = name.Substring(0, 8);
                                    }
                                    sb.Append(name + "                   ");
                                    sb.AppendLine(item.Debit.ToString());
                                }

                            }
                        }
                        else
                        {
                            sb.Append(maxdebit.LedgerAccount.AccountName + "                   ");
                            sb.AppendLine(thisledger.Credit.ToString());
                            return sb.ToString();
                        }
                    }
                    else
                    {
                        sb.Append(maxdebit.LedgerAccount.AccountName + "                   ");
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
                                    var name = item.LedgerAccount.AccountName;
                                    if (name.Length > 8)
                                    {
                                        name = name.Substring(0, 8);
                                    }
                                    sb.Append(name + "                   ");
                                    sb.AppendLine(item.Credit.ToString());
                                }
                                if (item.Debit > 0)
                                {
                                    var name = item.LedgerAccount.AccountName;
                                    if (name.Length > 8)
                                    {
                                        name = name.Substring(0, 8);
                                    }
                                    sb.Append(name + "                   ");
                                    sb.AppendLine(item.Debit.ToString());
                                }

                            }
                        }
                        else
                        {
                            sb.Append(maxcredit.LedgerAccount.AccountName + "                   ");
                            sb.AppendLine(thisledger.Debit.ToString());
                            return sb.ToString();
                        }
                    }
                    else
                    {
                        sb.Append(maxcredit.LedgerAccount.AccountName + "                   ");
                        sb.AppendLine(thisledger.Debit.ToString());
                        return sb.ToString();
                    }

                }
            }
            else
            {
                LedgerGeneral oppositeaccount = new LedgerGeneral();
                foreach (var item in ldd)
                {


                    if (item.LedgerAccountId != ledgerAccountId)
                    {
                        oppositeaccount = item;
                        break;
                    }

                }


                sb.Append(oppositeaccount.LedgerAccount.AccountName + "                   ");
                if (thisledger.Debit > 0)
                {
                    sb.AppendLine(thisledger.Debit.ToString());
                }
                if (thisledger.Credit > 0)
                {
                    sb.AppendLine(thisledger.Credit.ToString());

                }
            }


            return sb.ToString().Trim();
        }


        #region GetLedgerCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetLedgerCommand" />
        /// </summary>
        private RelayCommand _GetLedgerCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetLedgerCommand
        {
            get
            {
                if (_GetLedgerCommand == null)
                { _GetLedgerCommand = new RelayCommand(GetLedgerCommand_Execute, GetLedgerCommand_CanExecute); }

                return _GetLedgerCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetLedgerCommand" />
        /// </summary>
        private void GetLedgerCommand_Execute(object obj)
        {
            GetLedgerReport(SelectedLedgerAccount.LedgerAccountId);
        }

        /// <summary>
        /// Determines if <see cref="GetLedgerCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetLedgerCommand_CanExecute(object obj)
        {
            return SelectedLedgerAccount != null;
        }

        #endregion


    }
}

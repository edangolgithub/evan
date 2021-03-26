using TescoWineShopSql;
using Rms.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SAPBusinessObjects.WPF.Viewer;
using Rms.Classes;
using LiquorShopService.Services;
using System.Threading;
using WpfApp1;
using Rms;
using LiquorShop.Reports;
using Unity;

namespace LiquorShop.ViewModels.Reports
{
    class GeneralLedgerViewModel : ViewModelBase
    {
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

        // IEnumerable<LedgerJournalVm> _Report;



        private ObservableCollection<LedgerJournalVm> _Report;
        public ObservableCollection<LedgerJournalVm> Report
        {
            get { return _Report; }
            set
            {
                SetProperty<ObservableCollection<LedgerJournalVm>>(ref _Report, value);
            }
        }


        private Ledger _LedgerReport;
        public Ledger LedgerReport
        {
            get { return _LedgerReport; }
            set
            {
                SetProperty<Ledger>(ref _LedgerReport, value);
            }
        }



        private bool _IsReportLoaded;
        public bool IsReportLoaded
        {
            get { return _IsReportLoaded; }
            set
            {
                SetProperty<bool>(ref _IsReportLoaded, value);
            }
        }
        internal void GetLedgerReport(CrystalReportsViewer reportViewr)
        {
            try
            {
                CultureInfo info = new CultureInfo("ne-NP");
                info.NumberFormat.CurrencySymbol = "Rs";
                info.DateTimeFormat = new DateTimeFormatInfo();
                info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
                info.DateTimeFormat.AMDesignator = "AM";
                info.DateTimeFormat.PMDesignator = "PM";
                System.Threading.Thread.CurrentThread.CurrentCulture = info;
                Thread.CurrentThread.CurrentUICulture = info;
                IsReportLoaded = false;
                IsReportNotLoaded = true;
                Report = new ObservableCollection<LedgerJournalVm>(
                    Task.Run(() => GetLedgerReport(SelectedLedgerAccount.LedgerAccountId)).Result);
                // Report =new ObservableCollection<LedgerJournalVm>( GetLedgerReport(SelectedLedgerAccount.LedgerAccountId));
                if (Report.Count() < 1)
                {
                    throw new Exception("No Ledger For " + SelectedLedgerAccount.AccountName + " Found");
                }
                else
                {
                    Task.Run(() =>
                    {
                        LedgerReport = new LiquorShop.Reports.Ledger();
                        LedgerReport.SetDataSource(Report);

                    }).ContinueWith(a =>
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            reportViewr.ViewerCore.ReportSource = LedgerReport;
                        });
                    }).ContinueWith(a => { IsReportLoaded = true; IsReportNotLoaded = false; });




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public IEnumerable<LedgerJournalVm> Report
        //{
        //    get
        //    {
        //        return _Report;
        //    }
        //    set
        //    {
        //        if (_Report != value)
        //        {
        //            _Report = value;
        //            notify("Report");
        //        }
        //    }
        //}

        private ObservableCollection<LedgerAccount> _LedgerAccounts;

        public ObservableCollection<LedgerAccount> LedgerAccounts
        {
            get { return _LedgerAccounts; }
            set
            {
                _LedgerAccounts = value;
                notify("LedgerAccounts");
            }
        }

        internal void GetLedgerReport()
        {

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
        public ObservableCollection<LedgerGeneral> Ledgergenerals { get; set; }
        public ObservableCollection<LedgerTransaction> LedgerTransactions { get; set; }
        //public ObservableCollection<LedgerTransactionDetail> LedgerTransactionDetails { get; set; }

        [Dependency]
        public IAccountingService Service { get; set; }
        public GeneralLedgerViewModel(IAccountingService Service)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            IsDataLoaded = false;
            IsDataNotLoaded = true;
        }

        private async void Init()
        {
            LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsAsync());
            LedgerTransactions = new ObservableCollection<LedgerTransaction>(await Service.GetAllLedgerTransactionsAsync());
            //LedgerTransactionDetails = new ObservableCollection<LedgerTransactionDetail>(await Service.GetAllLedgerTransactionDetailsAsync());
            Ledgergenerals = new ObservableCollection<LedgerGeneral>(await Service.GetAllLedgerGeneralsAsync());
            IsDataNotLoaded = false; IsDataLoaded = true;
        }

        DateTime _FromDate;
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    notify("FromDate");
                }
            }
        }
        DateTime _ToDate;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    notify("ToDate");
                }
            }
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

        public ObservableCollection<BalanceViewModel> BalanceViewModels { get; private set; }

        /// <summary>
        /// Implements the execution of <see cref="GetLedgerCommand" />
        /// </summary>
        private void GetLedgerCommand_Execute(object obj)
        {
            //GetLedgerReport(SelectedLedgerAccount.LedgerAccountId);
        }

        /// <summary>
        /// Determines if <see cref="GetLedgerCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetLedgerCommand_CanExecute(object obj)
        {
            return SelectedLedgerAccount != null && FromDate <= ToDate;
        }

        #endregion

        public IEnumerable<LedgerJournalVm> GetLedgerReport(int LedgerAccountId = 1)
        {

            CultureInfo info = new CultureInfo("ne-NP");
            info.NumberFormat.CurrencySymbol = "Rs";
            info.DateTimeFormat = new DateTimeFormatInfo();
            info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            info.DateTimeFormat.AMDesignator = "AM";
            info.DateTimeFormat.PMDesignator = "PM";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;

            var data = Ledgergenerals.Where(a => a.LedgerAccountId == LedgerAccountId)
                .Where(a => a.JournalEntryDate.Date >= FromDate.Date && a.JournalEntryDate.Date <= ToDate.Date)
                .OrderBy(a => a.JournalEntryDate.Date)
                .Select(a => new LedgerJournalVm
                {

                    TransactionId = a.LedgerTransactionId,
                    Date = a.JournalEntryDate.Date,
                    LedgerAccount = a.LedgerAccount.AccountName,
                    Debit = a.Debit,//== 0 ? "" : a.Debit.ToString(),
                    Credit = a.Credit,// == 0 ? "" : a.Credit.ToString(),
                    Particulars = LedgerTransactions.Where(b => b.LedgerTransactionId == a.LedgerTransactionId).Select(b => b.Description).FirstOrDefault(),
                    OtherLedgerAccount = ResolveOpp(a.LedgerTransactionId, a.LedgerAccountId),
                    StartDate = FromDate.Date,
                    EndDate = ToDate.Date,
                    Balance = GetBalance(a),

                }).ToList();

            data.Insert(0, new LedgerJournalVm
            {
                Date = DateTime.Parse("01/01/2001"),
                StartDate = FromDate,
                EndDate = ToDate,
                LedgerAccount = SelectedLedgerAccount.AccountName,
                Balance = ResolveBalanceBroughtDown(LedgerAccountId)

            });

            JournalVms = new ObservableCollection<LedgerJournalVm>(data);
            JournalVms.Add(new Rms.Reports.LedgerJournalVm { Date = ToDate, BalanceCarriedDown = ResolveBalanceCarriedDown() });
            return JournalVms;
        }

        private string ResolveBalanceCarriedDown()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in CalculateTotals())
            {
                sb.AppendLine(item.TotalString);
            }
            return sb.ToString();
        }

        public ObservableCollection<BalanceViewModel> CalculateTotals()
        {
            BalanceViewModels = new ObservableCollection<BalanceViewModel>();
            decimal debitbalance = 0;
            decimal creditbalance = 0;
            var balance = 0m;
            string suffix = "";
            debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                 .Where(b => b.JournalEntryDate.Date <= ToDate.Date).Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                     .Where(b => b.JournalEntryDate.Date <= ToDate.Date).Sum(b => b.Credit);

            var debitbalancetotal = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                  .Sum(b => b.Debit);
            var creditbalancetotal = Ledgergenerals.Where(b => b.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                       .Sum(b => b.Credit);
            if (debitbalance > creditbalance)
            {
                balance = debitbalance - creditbalance;
                suffix = "dr";
            }
            if (debitbalance < creditbalance)
            {
                balance = creditbalance - debitbalance;
                suffix = "cr";
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
                TotalString = "Total        " + balancetotal.ToString("C")
            };
            BalanceViewModel balancevm2 = new BalanceViewModel
            {
                BalanceId = 2,

                TotalString = " Balance c/d   " + balance.ToString("C") + suffix
            };
            BalanceViewModels.Add(balancevm1);
            BalanceViewModels.Add(balancevm2);
            return BalanceViewModels;
        }
        private string GetBalance(LedgerGeneral a)
        {
            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == a.LedgerAccountId).Where(b => b.JournalEntryDate <= a.JournalEntryDate).Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == a.LedgerAccountId).Where(b => b.JournalEntryDate <= a.JournalEntryDate).Sum(b => b.Credit);
            decimal balance = 0;
            string suffix = "";
            if (debitbalance > creditbalance)
            {
                balance = debitbalance - creditbalance;
                suffix = "dr";
            }
            if (debitbalance < creditbalance)
            {
                balance = creditbalance - debitbalance;
                suffix = "cr";
            }
            return balance.ToString("C") + " " + suffix;
        }

        private void GetDebitCreditBalance(int ledgerAccountId, out decimal debitbalance, out decimal creditbalance)
        {
            debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == ledgerAccountId)
                .Where(b => b.JournalEntryDate.Date < FromDate.Date).Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == ledgerAccountId)
                     .Where(b => b.JournalEntryDate.Date < FromDate.Date).Sum(b => b.Credit);

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
                suffix = "dr";
            }
            if (debitbalance < creditbalance)
            {
                balance = creditbalance - debitbalance;
                suffix = "cr";
            }

            return "Balance b/f \n" + balance.ToString("C") + suffix;
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
            var v = LedgerTransactions.Where(a => a.LedgerTransactionId == transactionId);
            List<LedgerGeneral> ld = new List<LedgerGeneral>();
            List<LedgerGeneral> ldd = new List<LedgerGeneral>();
            LedgerGeneral thisledger = new LedgerGeneral();
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
            StringBuilder sb = new StringBuilder();
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




        #region common



        private bool _IsDataLoaded;
        public bool IsDataLoaded
        {
            get { return _IsDataLoaded; }
            set
            {
                SetProperty<bool>(ref _IsDataLoaded, value);
            }
        }
        bool _IsDataNotLoaded;
        public bool IsDataNotLoaded
        {
            get
            {
                return _IsDataNotLoaded;
            }
            set
            {
                if (_IsDataNotLoaded != value)
                {
                    _IsDataNotLoaded = value;
                    notify("IsDataNotLoaded");
                }
            }
        }

        #region LoadedCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="LoadedCommand" />
        /// </summary>
        private RelayCommand _LoadedCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                if (_LoadedCommand == null)
                { _LoadedCommand = new RelayCommand(LoadedCommand_Execute, LoadedCommand_CanExecute); }

                return _LoadedCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="LoadedCommand" />
        /// </summary>
        private async void LoadedCommand_Execute(object obj)
        {
            

            await Task.Run(() => Init()).ContinueWith(a => { });



        }

        /// <summary>
        /// Determines if <see cref="LoadedCommand" /> is allowed to execute
        /// </summary>
        private Boolean LoadedCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
        #endregion




        private bool _IsReportNotLoaded;
        public bool IsReportNotLoaded
        {
            get { return _IsReportNotLoaded; }
            set
            {
                SetProperty<bool>(ref _IsReportNotLoaded, value);
            }
        }


    }
}

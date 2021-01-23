using Rms.Classes;
using TescoWineShopSql;
using Rms.Reports;
using LiquorShopService.Services;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LiquorShop.Classes;
using LiquorShop.Reports;
using Rms;

namespace LiquorShop.ViewModels.Reports
{
    public class TrialBalanceReportViewModel : ViewModelBase
    {
        public TrialBalanceReportViewModel(IAccountingService Service)
        {
            this.Service = Service;
            IsDataLoaded = true;
            // TrailBalanceDate = DateTime.Today;
        }
        IAccountingService Service;


        private DateTime _TrailBalanceDate;
        public DateTime TrailBalanceDate
        {
            get { return _TrailBalanceDate; }
            set
            {
                SetProperty(ref _TrailBalanceDate, value);
            }
        }
        private bool _IsReportNotLoaded;
        public bool IsReportNotLoaded
        {
            get { return _IsReportNotLoaded; }
            set
            {
                SetProperty<bool>(ref _IsReportNotLoaded, value);
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
            IsDataLoaded = false;
            IsDataNotLoaded = true;
            FromTrialBalanceDate = DateTime.Today;
            ToTrialBalanceDate = DateTime.Today;
            await Task.Run(() => Init()).ContinueWith(a =>
            {
                IsDataNotLoaded = false; IsDataLoaded = true;
                TrialBalanceDate = DateTime.Today;
            });



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

        private async void Init()
        {

            IsReportLoaded = true;
            report = new TrialBalanceReport();
            LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsAsync());
            Ledgergenerals = new ObservableCollection<LedgerGeneral>(await Service.GetAllLedgerGeneralsAsync());


        }
        TrialBalanceReport report;



        #region GetTrialBalanceReportCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetTrialBalanceReportCommand" />
        /// </summary>
        private RelayCommand _GetTrialBalanceReportCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetTrialBalanceReportCommand
        {
            get
            {
                if (_GetTrialBalanceReportCommand == null)
                { _GetTrialBalanceReportCommand = new RelayCommand(GetTrialBalanceReportCommand_Execute, GetTrialBalanceReportCommand_CanExecute); }

                return _GetTrialBalanceReportCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetTrialBalanceReportCommand" />
        /// </summary>
        private async void GetTrialBalanceReportCommand_Execute(object obj)
        {
            IsReportLoaded = false;
            IsReportNotLoaded = true;


            await Task.Run(() => GetTrialBalance(TrialBalanceDate));
            await System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate

                {

                    CrystalReportsViewer ReportViewer = (CrystalReportsViewer)obj;
                    IsReportLoaded = true;
                    IsReportNotLoaded = false;
                    if (Tbms.Count < 1)
                    {
                        MessageBox.Show("No Trial Balance For That Date");
                        return;
                    }
                    else
                    {
                        report.SetDataSource(Tbms);
                        ReportViewer.ViewerCore.ReportSource = report;
                    }

                });
        }

        /// <summary>
        /// Determines if <see cref="GetTrialBalanceReportCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetTrialBalanceReportCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        private ObservableCollection<TrialBalanceExport> _Reports;
        public ObservableCollection<TrialBalanceExport> Reports
        {
            get { return _Reports; }
            set
            {
                SetProperty<ObservableCollection<TrialBalanceExport>>(ref _Reports, value);
            }
        }



        public decimal TotalRevenueBalance { get; set; }
        public decimal TotalExpenseBalance { get; set; }

        private void GetDebitCreditTotal(LedgerAccount LedgerAccount, out decimal debitbalance, out decimal creditbalance)
        {
            debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                          //.Where(a => a.JournalEntryDate.Date <= IncomeStatementDate.Date)
                          .Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
               //.Where(a => a.JournalEntryDate.Date <= IncomeStatementDate.Date)
               .Sum(b => b.Credit);
        }

        ObservableCollection<TrialBalanceVm> _Tbms;
        public ObservableCollection<TrialBalanceVm> Tbms
        {
            get
            {
                return _Tbms;
            }
            set
            {
                if (_Tbms != value)
                {
                    _Tbms = value;
                    notify("Tbms");
                }
            }
        }


        private DateTime _TrialBalanceDate;
        public DateTime TrialBalanceDate
        {
            get { return _TrialBalanceDate; }
            set
            {
                SetProperty<DateTime>(ref _TrialBalanceDate, value);
            }
        }
        public void GetTrialBalance()
        {

            Tbms = new ObservableCollection<TrialBalanceVm>();
            TotalDebit = 0;
            TotalCredit = 0;




            foreach (var item in LedgerAccounts)
            {
                var v = GetBalanceForTrialBalance(item);
                if (v != null)
                    Tbms.Add(v);
                //Reports.Add(new UserControls.Accounts.TrialBalanceExport { Account=v.LedgerAccountName, Debit=v.DebitSide,Credit=v.CreditSide })
            }
            TotalRows = new ObservableCollection<Rms.Classes.TrialBalanceVm>(
                new List<TrialBalanceVm> { new
                TrialBalanceVm { LedgerAccountName = "Total", DebitSide = TotalDebit, CreditSide = TotalCredit }});
        }
        public void GetTrialBalance(DateTime Date)
        {
            CultureInfo info = new CultureInfo("en-EN");
            info.NumberFormat.CurrencySymbol = "Rs";
            info.DateTimeFormat = new DateTimeFormatInfo();
            info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            info.DateTimeFormat.AMDesignator = "AM";
            info.DateTimeFormat.PMDesignator = "PM";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;
            Tbms = new ObservableCollection<Rms.Classes.TrialBalanceVm>();
            TotalDebit = 0;
            TotalCredit = 0;
            #region grouping
            List<LedgerAccount> acountlistwithparentacount = new List<LedgerAccount>();
            foreach (var item in LedgerAccounts.Where(a => a.parentLedgerAccount != null))
            {
                acountlistwithparentacount.Add(item);
            }
            List<LedgerAccount> accountlistwithoutparentaccount = new List<LedgerAccount>();
            accountlistwithoutparentaccount = LedgerAccounts.Except(acountlistwithparentacount).ToList();

            var groups = acountlistwithparentacount.GroupBy(a => a.parentLedgerAccount);
            #endregion
            foreach (var i in groups)
            {
                Tbms.Add(new TrialBalanceVm { LedgerAccountName = i.Key.AccountName, IsParentAccount = true });
                foreach (var item in LedgerAccounts.Where(a => a.parentLedgerAccount == i.Key))
                {
                    var v = GetBalanceForTrialBalance(item, Date);
                    if (v != null)
                        Tbms.Add(v);
                }
            }


            foreach (var item in accountlistwithoutparentaccount)
            {
                var v = GetBalanceForTrialBalance(item, Date);
                if (v != null)
                {
                    v.IsParentAccount = true;
                    Tbms.Add(v);
                }
            }

            TotalRows = new ObservableCollection<Rms.Classes.TrialBalanceVm>(
                new List<TrialBalanceVm> { new
                TrialBalanceVm { LedgerAccountName = "Total", DebitSide = TotalDebit, CreditSide = TotalCredit }});
        }
        private ObservableCollection<TrialBalanceVm> _TotalRows;
        public ObservableCollection<TrialBalanceVm> TotalRows
        {
            get { return _TotalRows; }
            set
            {
                SetProperty<ObservableCollection<TrialBalanceVm>>(ref _TotalRows, value);
            }
        }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }

        private TrialBalanceVm GetBalanceForTrialBalance(LedgerAccount LedgerAccount, DateTime Date)
        {

            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                   .Where(a => a.JournalEntryDate.Date <= ToTrialBalanceDate.Date && a.JournalEntryDate.Date >= FromTrialBalanceDate)
                                   .Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                     .Where(a => a.JournalEntryDate.Date <= ToTrialBalanceDate.Date && a.JournalEntryDate.Date >= FromTrialBalanceDate)

                .Sum(b => b.Credit);
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            string suffix = "";
            TrialBalanceVm tbm = new Rms.Classes.TrialBalanceVm();
            tbm.LedgerAccountName = LedgerAccount.AccountName;

            //if (debitbalance > creditbalance)
            //{
            if (LedgerAccount.AccountClassId == 1 || LedgerAccount.AccountClassId == 2)
            {
                balance = debitbalance - creditbalance;
                tbm.DebitSide = balance;
                TotalDebit += balance;
                suffix = "Dr";
            }
            else
            {
                balance = creditbalance - debitbalance;
                tbm.CreditSide = balance;
                TotalCredit += balance;
                suffix = "Cr";
            }
            tbm.Date = TrialBalanceDate.Date;
            //  return balance.ToString() + " " + suffix;
            suffix = suffix + "q";
            return tbm;
        }
        private DateTime _ToTrialBalanceDate;
        public DateTime ToTrialBalanceDate
        {
            get { return _ToTrialBalanceDate; }
            set
            {
                SetProperty<DateTime>(ref _ToTrialBalanceDate, value);
            }
        }
        private DateTime _FromTrialBalanceDate;
        public DateTime FromTrialBalanceDate
        {
            get { return _FromTrialBalanceDate; }
            set
            {
                SetProperty<DateTime>(ref _FromTrialBalanceDate, value);
            }
        }
        private TrialBalanceVm GetBalanceForTrialBalance(LedgerAccount LedgerAccount)
        {

            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                  .Where(a => a.JournalEntryDate.Date <= ToTrialBalanceDate.Date && a.JournalEntryDate.Date >= FromTrialBalanceDate)
                .Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                  .Where(a => a.JournalEntryDate.Date <= ToTrialBalanceDate.Date && a.JournalEntryDate.Date >= FromTrialBalanceDate)
                .Sum(b => b.Credit);
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            string suffix = "";
            TrialBalanceVm tbm = new Rms.Classes.TrialBalanceVm();
            tbm.LedgerAccountName = LedgerAccount.AccountName;

            //if (debitbalance > creditbalance)
            //{
            if (LedgerAccount.AccountClassId == 1 || LedgerAccount.AccountClassId == 2)
            {
                balance = debitbalance - creditbalance;
                tbm.DebitSide = balance;
                TotalDebit += balance;
                suffix = "Dr";
            }
            else
            {
                balance = creditbalance - debitbalance;
                tbm.CreditSide = balance;
                TotalCredit += balance;
                suffix = "Cr";
            }

            //  return balance.ToString() + " " + suffix;
            suffix = suffix + "q";
            return tbm;
        }


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
        ObservableCollection<LedgerGeneral> _Ledgergenerals;
        public ObservableCollection<LedgerGeneral> Ledgergenerals
        {
            get
            {
                return _Ledgergenerals;
            }
            set
            {
                if (_Ledgergenerals != value)
                {
                    _Ledgergenerals = value;
                    notify("Ledgergenerals");
                }
            }
        }
    }
}
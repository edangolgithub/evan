using Rms.Reports;
using LiquorShopService.Services;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
//using Rms.UserControls.Accounts;
using System.Threading;
using Rms.Classes;
using Rms.Data;
using System.Globalization;
using System.Windows;
using TescoWineShopSql;
using LiquorShop.Classes;
using LiquorShop.Reports;
using Rms;

namespace LiquorShop.ViewModels.Reports
{
    public class IncomeStatementReportViewModel : ViewModelBase
    {
        public IncomeStatementReportViewModel(IAccountingService Service)
        {
            this.Service = Service;
            IsDataLoaded = true;
        }
        IAccountingService Service;
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
            IncomeStatementDate = DateTime.Today;
            await Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false; IsDataLoaded = true; });
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
            IncomeStatementDate1 = DateTime.Today;
            report = new IncomeStatementReport();
            LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsAsync());
            Ledgergenerals = new ObservableCollection<LedgerGeneral>(await Service.GetAllLedgerGeneralsAsync());
        }
        IncomeStatementReport report;
        #region GetIncomeStatementReportCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="GetIncomeStatementReportCommand" />
        /// </summary>
        private RelayCommand _GetIncomeStatementReportCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetIncomeStatementReportCommand
        {
            get
            {
                if (_GetIncomeStatementReportCommand == null)
                { _GetIncomeStatementReportCommand = new RelayCommand(GetIncomeStatementReportCommand_Execute, GetIncomeStatementReportCommand_CanExecute); }
                return _GetIncomeStatementReportCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="GetIncomeStatementReportCommand" />
        /// </summary>
        private async void GetIncomeStatementReportCommand_Execute(object obj)
        {
            IsReportLoaded = false;
            IsReportNotLoaded = true;
            Reports = new ObservableCollection<IncomeStatementExport>();
            await Task.Run(() => GetIncomeStatement());
            await System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate
                {
                    if (Reports.Count > 0)
                    {
                        CrystalReportsViewer ReportViewer = (CrystalReportsViewer)obj;
                        report.SetDataSource(Reports);
                        ReportViewer.ViewerCore.ReportSource = report;
                    }
                    else
                    {
                        MessageBox.Show("No Report");
                    }
                    IsReportLoaded = true;
                    IsReportNotLoaded = false;
                });
        }
        /// <summary>
        /// Determines if <see cref="GetIncomeStatementReportCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetIncomeStatementReportCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion
        private ObservableCollection<IncomeStatementExport> _Reports;
        public ObservableCollection<IncomeStatementExport> Reports
        {
            get { return _Reports; }
            set
            {
                SetProperty<ObservableCollection<IncomeStatementExport>>(ref _Reports, value);
            }
        }
        private ObservableCollection<RevenueVm> _Revenues;
        public ObservableCollection<RevenueVm> Revenues
        {
            get { return _Revenues; }
            set
            {
                SetProperty<ObservableCollection<RevenueVm>>(ref _Revenues, value);
            }
        }
        private ObservableCollection<RevenueVm> _TotalRevenueRows;
        public ObservableCollection<RevenueVm> TotalRevenueRows
        {
            get { return _TotalRevenueRows; }
            set
            {
                SetProperty<ObservableCollection<RevenueVm>>(ref _TotalRevenueRows, value);
            }
        }
        private ObservableCollection<ExpenseVm> _TotalExpenseRows;
        public ObservableCollection<ExpenseVm> TotalExpenseRows
        {
            get { return _TotalExpenseRows; }
            set
            {
                SetProperty<ObservableCollection<ExpenseVm>>(ref _TotalExpenseRows, value);
            }
        }
        private ObservableCollection<ExpenseVm> _Expenses;
        public ObservableCollection<ExpenseVm> Expenses
        {
            get { return _Expenses; }
            set
            {
                SetProperty<ObservableCollection<ExpenseVm>>(ref _Expenses, value);
            }
        }
        private ObservableCollection<NetIncome> _NetIncomes;
        public ObservableCollection<NetIncome> NetIncomes
        {
            get { return _NetIncomes; }
            set
            {
                SetProperty<ObservableCollection<NetIncome>>(ref _NetIncomes, value);
            }
        }
        private DateTime _IncomeStatementDate;
        public DateTime IncomeStatementDate
        {
            get { return _IncomeStatementDate; }
            set
            {
                SetProperty<DateTime>(ref _IncomeStatementDate, value);
            }
        }

        DateTime _IncomeStatementDate1;
        public DateTime IncomeStatementDate1
        {
            get
            {
                return _IncomeStatementDate1;
            }
            set
            {
                if (_IncomeStatementDate1 != value)
                {
                    _IncomeStatementDate1 = value;
                    notify("IncomeStatementDate1");
                }
            }
        }

        public ObservableCollection<IncomeStatementExport> GetIncomeStatement()
        {
            CultureInfo info = new CultureInfo("ne-NP");
            info.NumberFormat.CurrencySymbol = "Rs";
            info.DateTimeFormat = new DateTimeFormatInfo();
            info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            info.DateTimeFormat.AMDesignator = "AM";
            info.DateTimeFormat.PMDesignator = "PM";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
            Revenues = new ObservableCollection<RevenueVm>();
            Expenses = new ObservableCollection<ExpenseVm>();
            TotalExpenseBalance = 0;
            TotalRevenueBalance = 0;
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
            Reports.Add(new IncomeStatementExport { AccountType = "Revenue", Date = IncomeStatementDate.Date.ToLongDateString() });

            foreach (var i in groups)
            {
                decimal revenuegroupbalance = 0;
                
                foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 3 && a.parentLedgerAccount == i.Key))
                {
                    decimal debitbalance, creditbalance;
                    GetDebitCreditTotal(item, out debitbalance, out creditbalance);
                    var v = GetRevenues(item, debitbalance, creditbalance);
                    if (v != null)
                    {
                        revenuegroupbalance += v.Amount;
                    }
                }
               
                if (revenuegroupbalance > 0)
                {
                    Reports.Add(new IncomeStatementExport { Account = i.Key.AccountName, Balance = revenuegroupbalance.ToString("C") });
                    //  Revenues.Add(new RevenueVm { LedgerAccountName = i.Key.AccountName, Amount = revenuegroupbalance });
                }
               
            }
            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 3))
            {
                decimal debitbalance, creditbalance;
                GetDebitCreditTotal(item, out debitbalance, out creditbalance);
                var v = GetRevenues(item, debitbalance, creditbalance);
                if (v != null)
                    Reports.Add(new IncomeStatementExport { Account = v.LedgerAccountName, Balance = v.Amount.ToString("C") });
            }
            Reports.Add(new IncomeStatementExport { TotalHeading = "TotalRevenue", Total = TotalRevenueBalance.ToString("C") });
            //TotalRevenueRows = new ObservableCollection<RevenueVm>(new List<RevenueVm>
            //{ new RevenueVm { LedgerAccountName="Total Revenues", Amount=TotalRevenueBalance } });
            Reports.Add(new IncomeStatementExport { AccountType = "Expense" });



            foreach (var i in groups)
            {
                
                decimal expensegroupbalance = 0;
               
                foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 2 && a.parentLedgerAccount == i.Key))
                {
                    decimal debitbalance, creditbalance;
                    GetDebitCreditTotal(item, out debitbalance, out creditbalance);
                    var v = GetExpenses(item, debitbalance, creditbalance);
                    if (v != null)
                    {
                        expensegroupbalance += v.Amount;
                    }
                }
              
                if (expensegroupbalance > 0)
                {
                    Reports.Add(new IncomeStatementExport { Account = i.Key.AccountName, Balance = expensegroupbalance.ToString("C") });
                    // Expenses.Add(new ExpenseVm { LedgerAccountName = i.Key.AccountName, Amount = expensegroupbalance });
                }
            }


            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 2))
            {
                decimal debitbalance, creditbalance;
                GetDebitCreditTotal(item, out debitbalance, out creditbalance);
                var v = GetExpenses(item, debitbalance, creditbalance);
                if (v != null)
                    Reports.Add(new IncomeStatementExport { Account = v.LedgerAccountName, Balance = v.Amount.ToString("C") });
            }
            Reports.Add(new IncomeStatementExport { TotalHeading = "Total Expense", Total = TotalExpenseBalance.ToString("C") });
            //TotalExpenseRows = new ObservableCollection<ExpenseVm>(new List<ExpenseVm>
            //{ new ExpenseVm { LedgerAccountName="Total Expenses", Amount=TotalExpenseBalance } });
            Reports.Add(new IncomeStatementExport { TotalHeading = "Net Income", Total = GetNetIncome().ToString("C") });
            //NetIncomes = new ObservableCollection<Rms.Classes.NetIncome>(new List<NetIncome>
            //{ new NetIncome { LedgerAccountName="Total", Amount=GetNetIncome() } });
            return Reports;
        }
        public decimal TotalRevenueBalance { get; set; }
        public decimal TotalExpenseBalance { get; set; }
        private ExpenseVm GetExpenses(LedgerAccount LedgerAccount, decimal debitbalance, decimal creditbalance)
        {
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            ExpenseVm evm = new ExpenseVm();
            evm.LedgerAccountName = LedgerAccount.AccountName;
            balance = debitbalance - creditbalance;
            evm.Amount = balance;
            TotalExpenseBalance += balance;
            return evm;
        }
        private void GetDebitCreditTotal(LedgerAccount LedgerAccount, out decimal debitbalance, out decimal creditbalance)
        {

            debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                 .Where(a => a.JournalEntryDate.Date >= IncomeStatementDate.Date && a.JournalEntryDate.Date <= IncomeStatementDate1.Date)
                .Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                .Where(a => a.JournalEntryDate.Date >= IncomeStatementDate.Date && a.JournalEntryDate.Date <= IncomeStatementDate1.Date)
                .Sum(b => b.Credit);
        }
        private RevenueVm GetRevenues(LedgerAccount LedgerAccount, decimal debitbalance, decimal creditbalance)
        {
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            RevenueVm rvm = new RevenueVm();
            rvm.LedgerAccountName = LedgerAccount.AccountName;
            balance = creditbalance - debitbalance;
            rvm.Amount = balance;
            TotalRevenueBalance += balance;
            return rvm;
        }
        public decimal GetNetIncome()
        {
            NetIncome = TotalRevenueBalance - TotalExpenseBalance;
            return NetIncome;
        }
        private Decimal _NetIncome;
        public Decimal NetIncome
        {
            get { return _NetIncome; }
            set
            {
                SetProperty<Decimal>(ref _NetIncome, value);
            }
        }
        private ObservableCollection<LedgerAccount> _LedgerAccounts;
        public ObservableCollection<LedgerAccount> LedgerAccounts
        {
            get { return _LedgerAccounts; }
            set
            {
                _LedgerAccounts = value;
                NotifyPropertyChanged("LedgerAccounts");
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

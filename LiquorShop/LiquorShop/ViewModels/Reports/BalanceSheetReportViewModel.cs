using Rms.Classes;
using Rms.Data;
using Rms.Reports;
using LiquorShopService.Services;
//using Rms.UserControls.Accounts;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TescoWineShopSql;
using LiquorShop.Classes;
using LiquorShop.Reports;
using Rms;

namespace LiquorShop.ViewModels.Reports
{
   public class BalanceSheetReportViewModel:ViewModelBase
    {
        IAccountingService Service;
        public BalanceSheetReportViewModel(IAccountingService Service)
        {
            this.Service = Service;
        }
        
        private ObservableCollection<AssetsVm> _Assets;
        public ObservableCollection<AssetsVm> Assets
        {
            get { return _Assets; }
            set
            {
                SetProperty(ref _Assets, value);
            }
        }


        private ObservableCollection<LiabilitiesVm> _Liabilities;
        public ObservableCollection<LiabilitiesVm> Liabilities
        {
            get { return _Liabilities; }
            set
            {
                SetProperty(ref _Liabilities, value);
            }
        }


        private ObservableCollection<EquityVm> _Equities;
        public ObservableCollection<EquityVm> Equities
        {
            get { return _Equities; }
            set
            {
                SetProperty(ref _Equities, value);
            }
        }


        private ObservableCollection<AssetsVm> _TotalAssetsRows;
        public ObservableCollection<AssetsVm> TotalAssetsRows
        {
            get { return _TotalAssetsRows; }
            set
            {
                SetProperty(ref _TotalAssetsRows, value);
            }
        }


        private ObservableCollection<LiabilitiesVm> _TotalLiabilitiesRows;
        public ObservableCollection<LiabilitiesVm> TotalLiabilitiesRows
        {
            get { return _TotalLiabilitiesRows; }
            set
            {
                SetProperty(ref _TotalLiabilitiesRows, value);
            }
        }


        private ObservableCollection<EquityVm> _TotalEquitiesRows;
        public ObservableCollection<EquityVm> TotalEquitiesRows
        {
            get { return _TotalEquitiesRows; }
            set
            {
                SetProperty(ref _TotalEquitiesRows, value);
            }
        }



        private ObservableCollection<LiabilitiesAndEquity> _LiabilitiesAndEquity;
        public ObservableCollection<LiabilitiesAndEquity> LiabilitiesAndEquity
        {
            get { return _LiabilitiesAndEquity; }
            set
            {
                SetProperty(ref _LiabilitiesAndEquity, value);
            }
        }

     


        public decimal TotalAssetsBalance { get; set; }
        public decimal TotalLiabilitiesBalance { get; set; }
        public decimal TotalEquityBalance { get; set; }
       

        public void GetBalanceSheet()
        {
            CultureInfo info = new CultureInfo("ne-NP");
            info.NumberFormat.CurrencySymbol = "Rs";
            info.DateTimeFormat = new DateTimeFormatInfo();
            info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            info.DateTimeFormat.AMDesignator = "AM";
            info.DateTimeFormat.PMDesignator = "PM";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
            Assets = new ObservableCollection<AssetsVm>();
            Liabilities = new ObservableCollection<LiabilitiesVm>();
            Equities = new ObservableCollection<Rms.Classes.EquityVm>();
            LiabilitiesAndEquity = new ObservableCollection<Rms.Classes.LiabilitiesAndEquity>();
            TotalAssetsBalance = 0;
            TotalLiabilitiesBalance = 0;
            TotalEquityBalance = 0;
            Reports.Add(new BalanceSheetExport { AccountType = "Assets", Date=BalanceSheetDate.Date.ToLongDateString() });


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

            #region foreach
            foreach (var item in groups)
            {
                decimal assetgroupbalance = 0;
               
                foreach (var i in LedgerAccounts.Where(a => a.parentLedgerAccount == item.Key && a.AccountClassId == 1))
                {
                    var v = GetAssets(i);
                    if (v != null)
                    {
                        assetgroupbalance += v.Amount;
                    }

                }
                if (assetgroupbalance > 0)
                {
                    Reports.Add(new BalanceSheetExport { Account = item.Key.AccountName, Balance = assetgroupbalance.ToString("C") });
                   // Assets.Add(new AssetsVm { LedgerAccountName = item.Key.AccountName, Amount = assetgroupbalance });
                }      

            }
            #endregion
            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 1))
            {
                var v = GetAssets(item);
                if (v != null)
                {
                    
                    Reports.Add(new BalanceSheetExport { Account=v.LedgerAccountName,Balance=v.Amount.ToString("C") });
                }
            }
            Reports.Add(new BalanceSheetExport { TotalHeading = "Total Assets", Total = TotalAssetsBalance.ToString("C") });
            Reports.Add(new BalanceSheetExport { AccountType = "Liabilities" });
            #region foreach
            foreach (var item in groups)
            {
               
                decimal liabilitiesgroupbalance = 0;
              
               
                foreach (var i in LedgerAccounts.Where(a => a.AccountClassId == 4 && a.parentLedgerAccount == item.Key))
                {
                    var v = GetLiabilities(i);
                    if (v != null)
                    {
                        liabilitiesgroupbalance += v.Amount;
                    }
                }
                if (liabilitiesgroupbalance > 0)
                {
                    Reports.Add(new BalanceSheetExport { Account = item.Key.AccountName, Balance = liabilitiesgroupbalance.ToString("C") });

                    // Liabilities.Add(new LiabilitiesVm { LedgerAccountName = item.Key.AccountName, Amount = liabilitiesgroupbalance });
                }
               

            }
            #endregion
            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 4))
            {
                var v = GetLiabilities(item);
                if (v != null)
                {
                    //Liabilities.Add(v);
                    Reports.Add(new BalanceSheetExport { Account = v.LedgerAccountName, Balance = v.Amount.ToString("C") });
                }
            }
            Reports.Add(new BalanceSheetExport { TotalHeading = "Total Liabilities", Total = TotalLiabilitiesBalance.ToString("C") });
            Reports.Add(new BalanceSheetExport { AccountType = "Equity" });

            #region foreach
            foreach (var item in groups)
            {
               
                decimal equitygroupbalance = 0;
               
                foreach (var i in LedgerAccounts.Where(a => a.AccountClassId == 5 && a.parentLedgerAccount == item.Key))
                {
                    var v = GetEquities(i);
                    if (v != null)
                    {
                        equitygroupbalance += v.Amount;
                    }
                }
                if (equitygroupbalance > 0)
                {
                    Reports.Add(new BalanceSheetExport { Account = item.Key.AccountName, Balance = equitygroupbalance.ToString("C") });

                }

            }
            #endregion
            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 5))
            {
                var v = GetEquities(item);
                if (v != null)
                {
                    //Equities.Add(v);
                    Reports.Add(new BalanceSheetExport { Account = v.LedgerAccountName, Balance = v.Amount.ToString("C") });
                }
            }
            GetIncomeStatement();
            var lande = TotalEquityBalance + GetNetIncome() + TotalLiabilitiesBalance;
            var totalquity = TotalEquityBalance + GetNetIncome();
            Reports.Add(new BalanceSheetExport { Account = "Net Income", Balance = GetNetIncome().ToString("C")});
            Reports.Add(new BalanceSheetExport { TotalHeading = "Total Equities", Total = totalquity.ToString("C") });
          
            Reports.Add(new BalanceSheetExport {  });
           
            Reports.Add(new BalanceSheetExport { TotalHeading = "Liabilities And Equity", Total=lande.ToString("C") });
           
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

        private EquityVm GetEquities(LedgerAccount LedgerAccount)
        {
            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                .Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                .Sum(b => b.Credit);
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            EquityVm evm = new EquityVm();
            evm.LedgerAccountName = LedgerAccount.AccountName;
            balance = creditbalance-debitbalance  ;
            evm.Amount = balance;
            TotalEquityBalance += balance;
            
            return evm;
        }

        private LiabilitiesVm GetLiabilities(LedgerAccount LedgerAccount)
        {

            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                 .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                .Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                 .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                .Sum(b => b.Credit);
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            LiabilitiesVm lvm = new LiabilitiesVm();
            lvm.LedgerAccountName = LedgerAccount.AccountName;
            balance =  creditbalance- debitbalance;
            lvm.Amount = balance;
            TotalLiabilitiesBalance += balance;
            return lvm;
        }

        private AssetsVm GetAssets(LedgerAccount LedgerAccount)
        {
            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                 .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                .Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                 .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                .Sum(b => b.Credit);
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            AssetsVm avm = new AssetsVm();
            avm.LedgerAccountName = LedgerAccount.AccountName;
            balance =   debitbalance- creditbalance;
            avm.Amount = balance;
            TotalAssetsBalance += balance;
            return avm;
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


        private ObservableCollection<ExpenseVm> _Expenses;
        public ObservableCollection<ExpenseVm> Expenses
        {
            get { return _Expenses; }
            set
            {
                SetProperty<ObservableCollection<ExpenseVm>>(ref _Expenses, value);
            }
        }
        public void GetIncomeStatement()
        {
            Revenues = new ObservableCollection<RevenueVm>();
            Expenses = new ObservableCollection<ExpenseVm>();

            TotalExpenseBalance = 0;
            TotalRevenueBalance = 0;
            foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 3))
            {
                decimal debitbalance, creditbalance;
                GetDebitCreditTotal(item, out debitbalance, out creditbalance);
                var v = GetRevenues(item, debitbalance, creditbalance);
                if (v != null)
                    Revenues.Add(v);
            }
            foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 2))
            {
                decimal debitbalance, creditbalance;
                GetDebitCreditTotal(item, out debitbalance, out creditbalance);
                var v = GetExpenses(item, debitbalance, creditbalance);
                if (v != null)
                    Expenses.Add(v);
            }
            TotalRevenueRows = new ObservableCollection<RevenueVm>(new List<RevenueVm>
            { new RevenueVm { LedgerAccountName="Total Revenues", Amount=TotalRevenueBalance } });
            TotalExpenseRows = new ObservableCollection<ExpenseVm>(new List<ExpenseVm>
            { new ExpenseVm { LedgerAccountName="Total Expenses", Amount=TotalExpenseBalance } });
            NetIncomes = new ObservableCollection<Rms.Classes.NetIncome>(new List<NetIncome>
            { new NetIncome { LedgerAccountName="Total", Amount=GetNetIncome() } });
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
                          .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                          .Sum(b => b.Debit);
            creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
               .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
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
            BalanceSheetDate = DateTime.Today;
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


        private ObservableCollection<BalanceSheetExport> _Reports;
        public ObservableCollection<BalanceSheetExport> Reports
        {
            get { return _Reports; }
            set
            {
                SetProperty<ObservableCollection<BalanceSheetExport>>(ref _Reports, value);
            }
        }
        private async void Init()
        {
            IsReportLoaded = true;
            report = new BalanceSheetReport();
            LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsAsync());
            Ledgergenerals = new ObservableCollection<LedgerGeneral>(await Service.GetAllLedgerGeneralsAsync());
        }


        #region GetBalanceSheetReportCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetBalanceSheetReportCommand" />
        /// </summary>
        private RelayCommand _GetBalanceSheetReportCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetBalanceSheetReportCommand
        {
            get
            {
                if (_GetBalanceSheetReportCommand == null)
                { _GetBalanceSheetReportCommand = new RelayCommand(GetBalanceSheetReportCommand_Execute, GetBalanceSheetReportCommand_CanExecute); }

                return _GetBalanceSheetReportCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetBalanceSheetReportCommand" />
        /// </summary>
        private async void GetBalanceSheetReportCommand_Execute(object obj)
        {
            IsReportLoaded = false;
            IsReportNotLoaded = true;
            Reports = new ObservableCollection<BalanceSheetExport>();
            await Task.Run(
                
                
                ()=> GetBalanceSheet());
            await System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (ThreadStart)delegate
                 {
                  
                    CrystalReportsViewer ReportViewer = (CrystalReportsViewer)obj;
                    report.SetDataSource(Reports);
                    ReportViewer.ViewerCore.ReportSource = report;
                    IsReportLoaded = true;
                    IsReportNotLoaded = false;
                });
        }

        /// <summary>
        /// Determines if <see cref="GetBalanceSheetReportCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetBalanceSheetReportCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        private DateTime _BalanceSheetDate;
        public DateTime BalanceSheetDate
        {
            get { return _BalanceSheetDate; }
            set
            {
                SetProperty<DateTime>(ref _BalanceSheetDate, value);
            }
        }


        private BalanceSheetReport _report;
        public BalanceSheetReport report
        {
            get { return _report; }
            set
            {
                SetProperty<BalanceSheetReport>(ref _report, value);
            }
        }
    }
    }


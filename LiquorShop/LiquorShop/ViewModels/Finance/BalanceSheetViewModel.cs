using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Rms.Classes;
using Rms.Data;
using System.Windows;
using TescoWineShopSql;
using System.Data.Entity;


namespace Rms.ViewModels.Accounts
{
    public partial class FinanceViewModel
    {


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


        private DateTime _BalanceSheetDate;
        public DateTime BalanceSheetDate
        {
            get { return _BalanceSheetDate; }
            set
            {
                SetProperty<DateTime>(ref _BalanceSheetDate, value);
            }
        }
        #region GetBalanceSheetCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetBalanceSheetCommand" />
        /// </summary>
        private RelayCommand _GetBalanceSheetCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetBalanceSheetCommand
        {
            get
            {
                if (_GetBalanceSheetCommand == null)
                { _GetBalanceSheetCommand = new RelayCommand(GetBalanceSheetCommand_Execute, GetBalanceSheetCommand_CanExecute); }

                return _GetBalanceSheetCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetBalanceSheetCommand" />
        /// </summary>
        private void GetBalanceSheetCommand_Execute(object obj)
        {
            GetBalanceSheet();
        }

        /// <summary>
        /// Determines if <see cref="GetBalanceSheetCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetBalanceSheetCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        public decimal TotalAssetsBalance { get; set; }
        public decimal TotalLiabilitiesBalance { get; set; }
        public decimal TotalEquityBalance { get; set; }


        public void GetBalanceSheet()
        {
            try
            {
                Assets = new ObservableCollection<AssetsVm>();
                Liabilities = new ObservableCollection<LiabilitiesVm>();
                Equities = new ObservableCollection<Rms.Classes.EquityVm>();
                LiabilitiesAndEquity = new ObservableCollection<Rms.Classes.LiabilitiesAndEquity>();
                TotalAssetsBalance = 0;
                TotalLiabilitiesBalance = 0;
                TotalEquityBalance = 0;

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
                foreach (var item in groups)
                {
                    decimal assetgroupbalance = 0;
                    decimal liabilitiesgroupbalance = 0;
                    decimal equitygroupbalance = 0;
                    foreach (var i in LedgerAccounts.Where(a => a.parentLedgerAccount == item.Key && a.AccountClassId == 1))
                    {
                        var v = GetAssets(i);
                        if (v != null)
                        {
                            assetgroupbalance += v.Amount;
                        }

                    }
                    if (assetgroupbalance != 0)
                    {
                        Assets.Add(new AssetsVm { LedgerAccountName = item.Key.AccountName, Amount = assetgroupbalance });
                    }
                    foreach (var i in LedgerAccounts.Where(a => a.AccountClassId == 4 && a.parentLedgerAccount == item.Key))
                    {
                        var v = GetLiabilities(i);
                        if (v != null)
                        {
                            liabilitiesgroupbalance += v.Amount;
                        }
                    }
                    if (liabilitiesgroupbalance !=0)
                    {
                        Liabilities.Add(new LiabilitiesVm { LedgerAccountName = item.Key.AccountName, Amount = liabilitiesgroupbalance });
                    }
                    foreach (var i in LedgerAccounts.Where(a => a.AccountClassId == 5 && a.parentLedgerAccount == item.Key))
                    {
                        var v = GetEquities(i);
                        if (v != null)
                        {
                            equitygroupbalance += v.Amount;
                        }
                    }
                    if (equitygroupbalance!= 0)
                    {
                        Equities.Add(new EquityVm { LedgerAccountName = item.Key.AccountName, Amount = equitygroupbalance });
                    }

                }

                foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 1))
                {
                    var v = GetAssets(item);
                    if (v != null)
                        Assets.Add(v);
                }
                foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 4))
                {
                    var v = GetLiabilities(item);
                    if (v != null)
                        Liabilities.Add(v);
                }
                foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 5))
                {
                    var v = GetEquities(item);
                    if (v != null)
                        Equities.Add(v);
                }
                GetIncomeStatement(true);
                var netincome = GetNetIncome();
                if (netincome != 0)
                {
                    Equities.Add(new EquityVm { LedgerAccountName = "Net Income", Amount = netincome });
                }
                LiabilitiesAndEquity.Add(new Rms.Classes.LiabilitiesAndEquity { LedgerAccountName = "Total", Amount = TotalEquityBalance + GetNetIncome() + TotalLiabilitiesBalance });
                TotalAssetsRows = new ObservableCollection<AssetsVm>(new List<AssetsVm>
            { new AssetsVm { LedgerAccountName="Total Assets", Amount=TotalAssetsBalance } });
                TotalLiabilitiesRows = new ObservableCollection<LiabilitiesVm>(new List<LiabilitiesVm>
            { new LiabilitiesVm { LedgerAccountName="Total Liabilities", Amount=TotalLiabilitiesBalance } });
                TotalEquitiesRows = new ObservableCollection<EquityVm>(new List<EquityVm>
            { new EquityVm { LedgerAccountName="Total Equities", Amount=TotalEquityBalance +GetNetIncome()} });
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cannot Show BalanceSheet " + ex.Message);
            }
        }

        private EquityVm GetEquities(LedgerAccount LedgerAccount)
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
            EquityVm evm = new EquityVm();
            evm.LedgerAccountName = LedgerAccount.AccountName;
            balance = creditbalance - debitbalance;
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
            balance = creditbalance - debitbalance;
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
            balance = debitbalance - creditbalance;
            avm.Amount = balance;
            TotalAssetsBalance += balance;
            return avm;
        }
    }
}

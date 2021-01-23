using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Rms.Classes;
using Rms.Data;
using TescoWineShopSql;

namespace Rms.ViewModels.Accounts
{
    public partial class FinanceViewModel
    {
        #region GetIncomeStatementCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="GetIncomeStatementCommand" />
        /// </summary>
        private RelayCommand _GetIncomeStatementCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetIncomeStatementCommand
        {
            get
            {
                if (_GetIncomeStatementCommand == null)
                { _GetIncomeStatementCommand = new RelayCommand(GetIncomeStatementCommand_Execute, GetIncomeStatementCommand_CanExecute); }
                return _GetIncomeStatementCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="GetIncomeStatementCommand" />
        /// </summary>
        private void GetIncomeStatementCommand_Execute(object obj)
        {
            GetIncomeStatement(false);
        }
        /// <summary>
        /// Determines if <see cref="GetIncomeStatementCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetIncomeStatementCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion
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
        public void GetIncomeStatement(bool ForBalanceSheet)
        {
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
            foreach (var i in groups)
            {
                decimal revenuegroupbalance = 0;
                decimal expensegroupbalance = 0;
                foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 3 && a.parentLedgerAccount == i.Key))
                {
                    decimal debitbalance, creditbalance;
                    GetDebitCreditTotal(item, ForBalanceSheet, out debitbalance, out creditbalance);
                    var v = GetRevenues(item, debitbalance, creditbalance);
                    if (v != null)
                    {
                        revenuegroupbalance += v.Amount;
                    }
                   
                }
                foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 2 && a.parentLedgerAccount == i.Key))
                {
                    decimal debitbalance, creditbalance;
                    GetDebitCreditTotal(item, ForBalanceSheet, out debitbalance, out creditbalance);
                    var v = GetExpenses(item, debitbalance, creditbalance);
                    if (v != null)
                    {
                        expensegroupbalance += v.Amount;
                    }
                    
                }
                if (revenuegroupbalance > 0)
                {
                    Revenues.Add(new RevenueVm { LedgerAccountName = i.Key.AccountName, Amount = revenuegroupbalance });
                }
                if (expensegroupbalance > 0)
                {
                    Expenses.Add(new ExpenseVm { LedgerAccountName = i.Key.AccountName, Amount = expensegroupbalance });
                }
            }
            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 3))
            {
                decimal debitbalance, creditbalance;
                GetDebitCreditTotal(item, ForBalanceSheet, out debitbalance, out creditbalance);
                var v = GetRevenues(item, debitbalance, creditbalance);
                if (v != null)
                    Revenues.Add(v);
            }
            foreach (var item in accountlistwithoutparentaccount.Where(a => a.AccountClassId == 2))
            {
                decimal debitbalance, creditbalance;
                GetDebitCreditTotal(item, ForBalanceSheet, out debitbalance, out creditbalance);
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

        private void GetDebitCreditTotal(LedgerAccount LedgerAccount, bool ForBalanceSheet, out decimal debitbalance, out decimal creditbalance)
        {
            if (ForBalanceSheet)
            {
                debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                       .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                       .Sum(b => b.Debit);
                creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                    .Where(a => a.JournalEntryDate.Date <= BalanceSheetDate.Date)
                    .Sum(b => b.Credit);
            }
            else
            {
                debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                     .Where(a => a.JournalEntryDate.Date >= IncomeStatementDate.Date && a.JournalEntryDate.Date <= IncomeStatementDate1.Date)
                    .Sum(b => b.Debit);
                creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                    .Where(a => a.JournalEntryDate.Date >= IncomeStatementDate.Date&&a.JournalEntryDate.Date<=IncomeStatementDate1.Date)
                    .Sum(b => b.Credit);
            }
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
    }
}

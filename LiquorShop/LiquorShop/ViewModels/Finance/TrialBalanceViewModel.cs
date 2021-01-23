using Rms.Classes;
using Rms.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TescoWineShopSql;

namespace Rms.ViewModels.Accounts
{
    partial class FinanceViewModel
    {
        public void InitTrialBalance()
        {
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
        public void GetTrialBalance()
        {
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
                Tbms.Add(new TrialBalanceVm { LedgerAccountName = i.Key.AccountName });
                foreach (var item in LedgerAccounts.Where(a =>a.parentLedgerAccount == i.Key))
                {
                    var v = GetBalanceForTrialBalance(item);
                    if (v != null)
                        Tbms.Add(v);
                }
            }


            foreach (var item in accountlistwithoutparentaccount)
            {
                var v = GetBalanceForTrialBalance(item);
                if (v != null)
                    Tbms.Add(v);
            }
            TotalRows = new ObservableCollection<Rms.Classes.TrialBalanceVm>(
                new List<TrialBalanceVm> { new
                TrialBalanceVm { LedgerAccountName = "Total", DebitSide = TotalDebit, CreditSide = TotalCredit }});
        }
        public void GetTrialBalance(DateTime Date)
        {
            try
            {
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
                    bool moneyentered = false;
                    Tbms.Add(new TrialBalanceVm { LedgerAccountName = i.Key.AccountName, IsParentAccount = true });
                    foreach (var item in LedgerAccounts.Where(a => a.parentLedgerAccount == i.Key))
                    {
                        var v = GetBalanceForTrialBalance(item, Date);
                        if (v != null)
                        {
                            Tbms.Add(v);
                            moneyentered = true;
                        }
                    }

                    if(!moneyentered)
                    {
                        Tbms.Remove(Tbms.LastOrDefault());
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
            catch(Exception ex)
            {
                throw ex;
            }
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
                .Where(a => a.JournalEntryDate.Date <= ToTrialBalanceDate.Date&& a.JournalEntryDate.Date>=FromTrialBalanceDate)
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
                suffix = "dr";
            }
            else
            {
                balance = creditbalance - debitbalance;
                tbm.CreditSide = balance;
                TotalCredit += balance;
                suffix = "cr";
            }

            //  return balance.ToString() + " " + suffix;
            suffix = suffix + "q";
            return tbm;
        }
        private TrialBalanceVm GetBalanceForTrialBalance(LedgerAccount LedgerAccount)
        {

            var debitbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId).Sum(b => b.Debit);
            var creditbalance = Ledgergenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId).Sum(b => b.Credit);
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
                suffix = "dr";
            }
            else
            {
                balance = creditbalance - debitbalance;
                tbm.CreditSide = balance;
                TotalCredit += balance;
                suffix = "cr";
            }

            //  return balance.ToString() + " " + suffix;
            suffix = suffix + "q";
            return tbm;
        }

       
        #region GetTrialBalanceCommand Command
        /// <summary>
        /// Private member backing variable for <see cref="GetTrialBalanceCommand" />
        /// </summary>
        private RelayCommand _GetTrialBalanceCommand = null;
        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetTrialBalanceCommand
        {
            get
            {
                if (_GetTrialBalanceCommand == null)
                { _GetTrialBalanceCommand = new RelayCommand(GetTrialBalanceCommand_Execute, GetTrialBalanceCommand_CanExecute); }
                return _GetTrialBalanceCommand;
            }
        }
        /// <summary>
        /// Implements the execution of <see cref="GetTrialBalanceCommand" />
        /// </summary>
        private void GetTrialBalanceCommand_Execute(object obj)
        {
            try
            {
                GetTrialBalance(DateTime.Today);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Determines if <see cref="GetTrialBalanceCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetTrialBalanceCommand_CanExecute(object obj)
        {
            return true;
        }
        #endregion
    }
}

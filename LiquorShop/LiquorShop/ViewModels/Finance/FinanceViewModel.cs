using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using LiquorShopService.Services;
using System.Windows.Data;
using TescoWineShopSql;

namespace Rms.ViewModels.Accounts
{
   public partial class FinanceViewModel:ViewModelBase
    {
       public IAccountingService Service;
        internal User CreateFakeUser()
        {
            return Service.CreateFakeUserAsync().Result;
        }
        public FinanceViewModel(IAccountingService Service)
        {
            this.Service = Service;
          
           // InitJournalEntryListViewModel();
          
        }
        public User FakeUser { get; set; }
        public async void Init()
        {
            try
            {
                AccountClasses = new ObservableCollection<AccountClass>(await Service.GetAllAccountClassesAsync());
                LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsAsync());
                FakeUser = await Service.CreateFakeUserAsync();
                await Task.Run(() => Init2());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async void Init2()
        {
            try
            {
                LedgerTransactions = new ObservableCollection<LedgerTransaction>(await Service.GetAllLedgerTransactionsAsync());
                Ledgergenerals = new ObservableCollection<LedgerGeneral>(await Service.GetAllLedgerGeneralsAsync());
                LedgerTransactionDetails = new ObservableCollection<LedgerTransactionDetail>(Service.GetAllLedgerTransactionDetails());
                //LedgerAccounts =new ObservableCollection<LedgerAccount>(LedgerAccounts.OrderByDescending(a => a.LedgerAccountId));
                LedgerAccounts = new ObservableCollection<LedgerAccount>(LedgerAccounts.OrderBy(b => b.AccountName));
                getJournalsBydate();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void getJournalsBydate()
        {

            if (Ledgergenerals == null)
            {
                return;
            }
            var data = 
                    Ledgergenerals.OrderByDescending(a => a.JournalEntryDate).ThenBy(a => a.JournalEntryDate.TimeOfDay).Join(LedgerTransactions, b => b.LedgerTransactionId, a => a.LedgerTransactionId, (a, b) =>
                           new
                           {
                               LedgerTransactionId = a.LedgerTransactionId,
                               JournalEntryDate = a.JournalEntryDate,
                               AccountName = a.LedgerAccount.AccountName,
                               Particulars = a.Credit == 0 ? b.Description : "",
                               Debit = a.Debit,
                               Credit = a.Credit
                           }
                    ).Where(a => a.JournalEntryDate.Date == SelectedDate.Date)
                   .ToList();
            RView = new CollectionView(data);
        }

        DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return _SelectedDate;
            }
            set
            {
                if (_SelectedDate != value)
                {
                    _SelectedDate = value;
                    notify("SelectedDate");
                    if (Ledgergenerals != null)
                    {
                        if (Ledgergenerals.Count > 0)
                        {
                            getJournalsBydate();
                        }
                    }

                }
            }
        }
        CollectionView _RView;
        public CollectionView RView
        {
            get
            {
                return _RView;
            }
            set
            {
                if (_RView != value)
                {
                    _RView = value;
                    notify("RView");
                }
            }
        }

        #region props

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

        ObservableCollection<AccountClass> _AccountClasses;
        public ObservableCollection<AccountClass> AccountClasses
        {
            get
            {
                return _AccountClasses;
            }
            set
            {
                if (_AccountClasses != value)
                {
                    _AccountClasses = value;
                    notify("AccountClasses");
                }
            }
        }
        AccountClass _SelectedAccountClass;
        public AccountClass SelectedAccountClass
        {
            get
            {
                return _SelectedAccountClass;
            }
            set
            {
                if (_SelectedAccountClass != value)
                {
                    _SelectedAccountClass = value;
                    notify("SelectedAccountClass");
                }
            }
        }
        ObservableCollection<LedgerTransaction> _Ledgertransactions;
        public ObservableCollection<LedgerTransaction> LedgerTransactions
        {
            get
            {
                return _Ledgertransactions;
            }
            set
            {
                if (_Ledgertransactions != value)
                {
                    _Ledgertransactions = value;
                    notify("LedgerTransactions");
                }
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

        ObservableCollection<LedgerTransactionDetail> _LedgerTransactionDetails;
        public ObservableCollection<LedgerTransactionDetail> LedgerTransactionDetails
        {
            get
            {
                return _LedgerTransactionDetails;
            }
            set
            {
                if (_LedgerTransactionDetails != value)
                {
                    _LedgerTransactionDetails = value;
                    notify("LedgerTransactionDetails");
                }
            }
        }
        #endregion

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
            SelectedDate = DateTime.Today;
            FromTrialBalanceDate = DateTime.Today;
            IncomeStatementDate = DateTime.Today;
            IncomeStatementDate1 = DateTime.Today;
            BalanceSheetDate = DateTime.Today;
            FromTrialBalanceDate = DateTime.Today;
            ToTrialBalanceDate = DateTime.Today;
            InitJournalEntryVm();
            initcoavm();
            initledgervm();
            InitJournalEntryListViewModel();
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



    }
}

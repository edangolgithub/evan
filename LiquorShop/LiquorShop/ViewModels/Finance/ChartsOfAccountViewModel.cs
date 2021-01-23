using Rms.Data;
using LiquorShopService.Services;
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
  public partial  class FinanceViewModel
    {
        #region props
      
        Visibility _IsGridVisible;
        public Visibility IsGridVisible
        {
            get
            {
                return _IsGridVisible;
            }
            set
            {
                if (_IsGridVisible != value)
                {
                    _IsGridVisible = value;
                    notify("IsGridVisible");
                }
            }
        }
        Visibility _IsBusy;
        public Visibility IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    notify("IsBusy");
                }
            }
        }

      
        #endregion

        public void initcoavm()
        {

            
        
        }
        //private ObservableCollection<LedgerAccount> _PLedgerAccounts;

        //public ObservableCollection<LedgerAccount> PLedgerAccounts
        //{
        //    get { return _PLedgerAccounts; }
        //    set
        //    {
        //        _PLedgerAccounts = value;
        //        notify("PLedgerAccounts");
        //    }
        //}

        private void PostDataInit()
        {
            IsGridVisible = Visibility.Visible;
            IsBusy = Visibility.Hidden;
           // PLedgerAccounts = new ObservableCollection<LedgerAccount>(LedgerAccounts);
        }
       
        #region NewCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="NewCommand" />
        /// </summary>
        private RelayCommand _NewCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NewCommand
        {
            get
            {
                if (_NewCommand == null)
                { _NewCommand = new RelayCommand(NewCommand_Execute, NewCommand_CanExecute); }

                return _NewCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="NewCommand" />
        /// </summary>
        private void NewCommand_Execute(object obj)
        {
            SelectedLedgerAccount = new LedgerAccount();
        }

        /// <summary>
        /// Determines if <see cref="NewCommand" /> is allowed to execute
        /// </summary>
        private Boolean NewCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
        #region SaveCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SaveCommand" />
        /// </summary>
        private RelayCommand _SaveCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                { _SaveCommand = new RelayCommand(SaveCommand_Execute, SaveCommand_CanExecute); }

                return _SaveCommand;
            }
        }

        private bool SaveCommand_CanExecute(object obj)
        {
            return SelectedLedgerAccount != null;
        }

        private void SaveCommand_Execute(object obj)
        {
            SelectedLedgerAccount.CompanyId = 1;
            if(SelectedLedgerAccount.IsSystemLedger)
            {
                MessageBox.Show("Sorry Cannot Modify System Ledgers");
                return;
            }
            SelectedLedgerAccount.IsSystemLedger = false;
           if( Service.SaveLedgerAccount(SelectedLedgerAccount)>0)
            {
                var parentaccount = SelectedLedgerAccount.parentLedgerAccount;
                if (parentaccount != null)
                {
                    var lgs = Ledgergenerals.Where(a => a.LedgerAccountId == parentaccount.LedgerAccountId);
                    foreach (var item in lgs)
                    {
                        item.LedgerAccountId = SelectedLedgerAccount.LedgerAccountId;
                        Service.SaveLedgerGeneral(item);
                    }
                    var ltds = LedgerTransactionDetails.Where(a => a.LedgerAccountId == parentaccount.LedgerAccountId);
                    foreach (var item in ltds)
                    {
                        item.LedgerAccountId = SelectedLedgerAccount.LedgerAccountId;
                        Service.SaveLedgerTransactionDetail(item);
                    }
                }
            }
            Task.Run(() => Init());
            SelectedLedgerAccount = null;
        }




        #endregion
        #region DeleteCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="DeleteCommand" />
        /// </summary>
        private RelayCommand _DeleteCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                { _DeleteCommand = new RelayCommand(DeleteCommand_Execute, DeleteCommand_CanExecute); }

                return _DeleteCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="DeleteCommand" />
        /// </summary>
        private void DeleteCommand_Execute(object obj)
        {
            if (SelectedLedgerAccount.IsSystemLedger)
            {
                MessageBox.Show("Sorry Cannot Modify System Ledgers");
                return;
            }
            Service.DeleteLedgerAccount(SelectedLedgerAccount);
            Task.Run(() => Init());
            SelectedLedgerAccount = null;
        }

        /// <summary>
        /// Determines if <see cref="DeleteCommand" /> is allowed to execute
        /// </summary>
        private Boolean DeleteCommand_CanExecute(object obj)
        {
            if(SelectedLedgerAccount!=null)
            {
                if(SelectedLedgerAccount.LedgerAccountId>0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

       
        #region NewAccountClassCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="NewAccountClassCommand" />
        /// </summary>
        private RelayCommand _NewAccountClassCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NewAccountClassCommand
        {
            get
            {
                if (_NewAccountClassCommand == null)
                { _NewAccountClassCommand = new RelayCommand(NewAccountClassCommand_Execute, NewAccountClassCommand_CanExecute); }

                return _NewAccountClassCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="NewAccountClassCommand" />
        /// </summary>
        private void NewAccountClassCommand_Execute(object obj)
        {
            SelectedAccountClass = new AccountClass();
        }

        /// <summary>
        /// Determines if <see cref="NewAccountClassCommand" /> is allowed to execute
        /// </summary>
        private Boolean NewAccountClassCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
        #region SaveAccountClassCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SaveAccountClassCommand" />
        /// </summary>
        private RelayCommand _SaveAccountClassCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveAccountClassCommand
        {
            get
            {
                if (_SaveAccountClassCommand == null)
                { _SaveAccountClassCommand = new RelayCommand(SaveAccountClassCommand_Execute, SaveAccountClassCommand_CanExecute); }

                return _SaveAccountClassCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SaveAccountClassCommand" />
        /// </summary>
        private void SaveAccountClassCommand_Execute(object obj)
        {
            if (SelectedAccountClass.IsSystemAccount)
            {
                MessageBox.Show("Sorry Cannot Modify System Ledgers");
                return;
            }
            int[] ids = { 1, 2, 3, 4, 5 };
            if (!ids.Contains(SelectedAccountClass.AccountClassId))
            {
                Service.SaveAccountClass(SelectedAccountClass);
                Task.Run(() => Init());
                SelectedAccountClass = null;
            }
            else
            {
                MessageBox.Show("Default Account Types Cannot Be Modified");
            }
        }

        /// <summary>
        /// Determines if <see cref="SaveAccountClassCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveAccountClassCommand_CanExecute(object obj)
        {
            if (SelectedAccountClass != null)
            {
                
                    int[] ids = { 1, 2, 3, 4, 5 };
                    if (!ids.Contains(SelectedAccountClass.AccountClassId))
                        return true;
                
            }
            return false;
        }

        #endregion
        #region DeleteAccountClassCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="DeleteAccountClassCommand" />
        /// </summary>
        private RelayCommand _DeleteAccountClassCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DeleteAccountClassCommand
        {
            get
            {
                if (_DeleteAccountClassCommand == null)
                { _DeleteAccountClassCommand = new RelayCommand(DeleteAccountClassCommand_Execute, DeleteAccountClassCommand_CanExecute); }

                return _DeleteAccountClassCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="DeleteAccountClassCommand" />
        /// </summary>
        private void DeleteAccountClassCommand_Execute(object obj)
        {
            if (SelectedLedgerAccount.IsSystemLedger)
            {
                MessageBox.Show("Sorry Cannot Modify System Ledgers");
                return;
            }
            Service.DeleteAccountClass(SelectedAccountClass);
            Task.Run(() => Init());
        }

        /// <summary>
        /// Determines if <see cref="DeleteAccountClassCommand" /> is allowed to execute
        /// </summary>
        private Boolean DeleteAccountClassCommand_CanExecute(object obj)
        {
            if (SelectedAccountClass != null)
            {
                if (SelectedAccountClass.AccountClassId > 0)
                {
                    int[] ids = { 1, 2, 3, 4, 5 };
                    if(!ids.Contains(SelectedAccountClass.AccountClassId))
                    return true;
                }
            }
            return false;
        }

        #endregion




    }
}

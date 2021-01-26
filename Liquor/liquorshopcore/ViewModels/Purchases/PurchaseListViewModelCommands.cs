using System;
//using TescoWine.BusinessLayer;
//using wsql;
using TescoWineShopSql;
using Rms;


namespace LiquorShop.ViewModels.Purchases
{
    public partial class PurchaseListViewModel : ViewModelBase
    {

        #region GetStockCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetStockCommand" />
        /// </summary>
        private RelayCommand _GetStockCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetStockCommand
        {
            get
            {
                if (_GetStockCommand == null)
                { _GetStockCommand = new RelayCommand(GetStockCommand_ExecuteAsync, GetStockCommand_CanExecute); }

                return _GetStockCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetStockCommand" />
        /// </summary>
        private async void GetStockCommand_ExecuteAsync(object obj)
        {
            Beverage b = (Beverage)obj;
            await GetTotalStockAsync(b);
        }

        /// <summary>
        /// Determines if <see cref="GetStockCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetStockCommand_CanExecute(object obj)
        {
            return SelectedPurchase!=null;
        }

        #endregion




        #region GetPurchasesByDateCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetPurchasesByDateCommand" />
        /// </summary>
        private RelayCommand _GetPurchasesByDateCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetPurchasesByDateCommand
        {
            get
            {
                if (_GetPurchasesByDateCommand == null)
                { _GetPurchasesByDateCommand = new RelayCommand(GetPurchasesByDateCommand_Execute, GetPurchasesByDateCommand_CanExecute); }

                return _GetPurchasesByDateCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetPurchasesByDateCommand" />
        /// </summary>
        private void GetPurchasesByDateCommand_Execute(object obj)
        {
            GetInvoices(StartDatePurchase, EndDatePurchase);
            //Purchases = new ObservableCollection<Purchase>(await Service.FindPurchasesByDateAsync(StartDatePurchase,EndDatePurchase));
        }

        /// <summary>
        /// Determines if <see cref="GetPurchasesByDateCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetPurchasesByDateCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion



        #region GetInvoiceInfoCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetInvoiceInfoCommand" />
        /// </summary>
        private RelayCommand _GetInvoiceInfoCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetInvoiceInfoCommand
        {
            get
            {
                if (_GetInvoiceInfoCommand == null)
                { _GetInvoiceInfoCommand = new RelayCommand(GetInvoiceInfoCommand_Execute, GetInvoiceInfoCommand_CanExecute); }

                return _GetInvoiceInfoCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetInvoiceInfoCommand" />
        /// </summary>
        private void GetInvoiceInfoCommand_Execute(object obj)
        {
            if (int.TryParse(InvoiceNumber.ToString(), out int d))
            {
                GetInvoices(StartDatePurchase, EndDatePurchase, d);
            }
        }

        /// <summary>
        /// Determines if <see cref="GetInvoiceInfoCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetInvoiceInfoCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion



        #region PopupOpenCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="PopupOpenCommand" />
        /// </summary>
        private RelayCommand _PopupOpenCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand PopupOpenCommand
        {
            get
            {
                if (_PopupOpenCommand == null)
                { _PopupOpenCommand = new RelayCommand(PopupOpenCommand_Execute, PopupOpenCommand_CanExecute); }

                return _PopupOpenCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="PopupOpenCommand" />
        /// </summary>
        private void PopupOpenCommand_Execute(object obj)
        {
            if (PopUpOpen)
            {
                PopUpOpen = false;
            }
            else
            {
                PopUpOpen = true;

            }
            }

        /// <summary>
        /// Determines if <see cref="PopupOpenCommand" /> is allowed to execute
        /// </summary>
        private Boolean PopupOpenCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion







    }
}

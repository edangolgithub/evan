using LiquorShop.Classes;
using Rms;
using Rms.Views.Restaurant;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Sales
{
    public partial class SalesViewModel : ViewModelBase
    {

        #region ReCalculateCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="ReCalculateCommand" />
        /// </summary>
        private RelayCommand _ReCalculateCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand ReCalculateCommand
        {
            get
            {
                if (_ReCalculateCommand == null)
                { _ReCalculateCommand = new RelayCommand(ReCalculateCommand_Execute, ReCalculateCommand_CanExecute); }

                return _ReCalculateCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="ReCalculateCommand" />
        /// </summary>
        private void ReCalculateCommand_Execute(object obj)
        {
            IsDiscountApplied = false;
            IsDiscountNotApplied = true;
            SelectedSaleVm.Discount = 0;
            SelectedSaleVm.Due = 0;
            SelectedSaleVm.Change = 0;
            SelectedSaleVm.AmountPaid = SelectedSaleVm.TotalAmount;
            CalculateTotal();
        }

        /// <summary>
        /// Determines if <see cref="ReCalculateCommand" /> is allowed to execute
        /// </summary>
        private Boolean ReCalculateCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region DiscountCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="DiscountCommand" />
        /// </summary>
        private RelayCommand _DiscountCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DiscountCommand
        {
            get
            {
                if (_DiscountCommand == null)
                { _DiscountCommand = new RelayCommand(DiscountCommand_Execute, DiscountCommand_CanExecute); }

                return _DiscountCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="DiscountCommand" />
        /// </summary>
        private void DiscountCommand_Execute(object obj)
        {

            SelectedSaleVm.AmountAfterDiscount = SelectedSaleVm.TotalAmount - SelectedSaleVm.Discount;
            SelectedSaleVm.Due = SelectedSaleVm.AmountAfterDiscount - SelectedSaleVm.AmountPaid;
            if (SelectedSaleVm.Due <= 0)
            {
                SelectedSaleVm.Due = 0;
            }
            SelectedSaleVm.Change = SelectedSaleVm.AmountPaid - SelectedSaleVm.AmountAfterDiscount;
            if (SelectedSaleVm.Change <= 0)
            {
                SelectedSaleVm.Change = 0;
            }


        }

        /// <summary>
        /// Determines if <see cref="DiscountCommand" /> is allowed to execute
        /// </summary>
        private Boolean DiscountCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region SaveSalesCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SaveSalesCommand" />
        /// </summary>
        private RelayCommand _SaveSalesCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveSalesCommand
        {
            get
            {
                if (_SaveSalesCommand == null)
                { _SaveSalesCommand = new RelayCommand(SaveSalesCommand_Execute, SaveSalesCommand_CanExecute); }

                return _SaveSalesCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SaveSalesCommand" />
        /// </summary>
        private void SaveSalesCommand_Execute(object obj)
        {
            if (SelectedSaleVm.BevarageVms.Count > 0)
            {
                SaleOrder so = new SaleOrder();
                var TotalAmount = SelectedSaleVm.BevarageVms.Sum(a => a.Total);
                //TotalAmount = TotalAmount - Discount ?? 0;
                so.TotalAmount = SelectedSaleVm.TotalAmount; ;
                so.Discount = SelectedSaleVm.Discount;
                so.SaleDate = SelectedSaleVm.SaleDate;
                so.AmountPaid = SelectedSaleVm.AmountPaid;
                so.Due = SelectedSaleVm.Due;
                so.Customer = SelectedSaleVm.Customer;
                so.AmountAfterDiscount = SelectedSaleVm.AmountAfterDiscount;
                so.Sales = SelectedSaleVm.BevarageVms.Select(a => new Sale
                {
                    BeverageId = a.BeverageId,
                    Quantity = a.Quantity,
                    UnitPrice=a.Price??0,
                    SaleDate=SelectedSaleVm.SaleDate
                }).ToList();
                string mode;
                if(IsCash)
                {
                    mode = "IsCash";
                }
                else if(IsCredit)
                {
                    mode = "IsCredit";
                }
                else if(IsBank)
                {
                    mode = "IsBank";
                }
                else
                {
                    mode = "IsCash";
                }
                if (Service.SaveSaleOrder(so,mode,SelectedCustomer,SelectedBankAccount, ChequeNumber) > 0)
                {

                }
            }
            SelectedSaleVm = new SaleVm();
        }

        /// <summary>
        /// Determines if <see cref="SaveSalesCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveSalesCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region CreditCheckedCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="CreditCheckedCommand" />
        /// </summary>
        private RelayCommand _CreditCheckedCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand CreditCheckedCommand
        {
            get
            {
                if (_CreditCheckedCommand == null)
                { _CreditCheckedCommand = new RelayCommand(CreditCheckedCommand_Execute, CreditCheckedCommand_CanExecute); }

                return _CreditCheckedCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="CreditCheckedCommand" />
        /// </summary>
        private void CreditCheckedCommand_Execute(object obj)
        {
            if (SelectedCustomer == null)
            {
                MessageBox.Show("You cannot Select Credit Now, First Select Customer");
                IsCash = true;
                return;
            }
            SelectedSaleVm.AmountPaid = 0;
            SelectedSaleVm.Due = SelectedSaleVm.AmountAfterDiscount;
           
        }

        /// <summary>
        /// Determines if <see cref="CreditCheckedCommand" /> is allowed to execute
        /// </summary>
        private Boolean CreditCheckedCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region BankCheckedCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="BankCheckedCommand" />
        /// </summary>
        private RelayCommand _BankCheckedCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand BankCheckedCommand
        {
            get
            {
                if (_BankCheckedCommand == null)
                { _BankCheckedCommand = new RelayCommand(BankCheckedCommand_Execute, BankCheckedCommand_CanExecute); }

                return _BankCheckedCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="BankCheckedCommand" />
        /// </summary>
        private void BankCheckedCommand_Execute(object obj)
        {
            //if (SelectedCustomer == null)
            //{
            //    MessageBox.Show("You cannot Select Credit Now, First Select Customer");
            //    IsCash = true;
            //    return;
            //}
           
        }

        /// <summary>
        /// Determines if <see cref="BankCheckedCommand" /> is allowed to execute
        /// </summary>
        private Boolean BankCheckedCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region CancelSalesCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="CancelSalesCommand" />
        /// </summary>
        private RelayCommand _CancelSalesCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand CancelSalesCommand
        {
            get
            {
                if (_CancelSalesCommand == null)
                { _CancelSalesCommand = new RelayCommand(CancelSalesCommand_Execute, CancelSalesCommand_CanExecute); }

                return _CancelSalesCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="CancelSalesCommand" />
        /// </summary>
        private void CancelSalesCommand_Execute(object obj)
        {
            SelectedSaleVm = new SaleVm();
        }

        /// <summary>
        /// Determines if <see cref="CancelSalesCommand" /> is allowed to execute
        /// </summary>
        private Boolean CancelSalesCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region BillPrintCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="BillPrintCommand" />
        /// </summary>
        private RelayCommand _BillPrintCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand BillPrintCommand
        {
            get
            {
                if (_BillPrintCommand == null)
                { _BillPrintCommand = new RelayCommand(BillPrintCommand_Execute, BillPrintCommand_CanExecute); }

                return _BillPrintCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="BillPrintCommand" />
        /// </summary>
        private void BillPrintCommand_Execute(object obj)
        {
            if (SelectedSaleVm.BevarageVms.Count > 0)
            {
                InvoiceView view = new InvoiceView(SelectedSaleVm);
                view.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nothing to write in bill");
            }
        }

        /// <summary>
        /// Determines if <see cref="BillPrintCommand" /> is allowed to execute
        /// </summary>
        private Boolean BillPrintCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region PopularBeveragesCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="PopularBeveragesCommand" />
        /// </summary>
        private RelayCommand _PopularBeveragesCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand PopularBeveragesCommand
        {
            get
            {
                if (_PopularBeveragesCommand == null)
                { _PopularBeveragesCommand = new RelayCommand(PopularBeveragesCommand_ExecuteAsync, PopularBeveragesCommand_CanExecute); }

                return _PopularBeveragesCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="PopularBeveragesCommand" />
        /// </summary>
        private async void PopularBeveragesCommand_ExecuteAsync(object obj)
        {
            this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllPopularBeveragesAsync());
        }

        /// <summary>
        /// Determines if <see cref="PopularBeveragesCommand" /> is allowed to execute
        /// </summary>
        private Boolean PopularBeveragesCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion

    }
}

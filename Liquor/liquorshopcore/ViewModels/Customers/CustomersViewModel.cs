using LiquorShopService.Services;
using Rms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using TescoWineShopSql;
namespace LiquorShop.ViewModels.Customers
{
    public partial class CustomersViewModel : ViewModelBase
    {
        public ICustomerService Service;
        public CustomersViewModel(ICustomerService Service)
        {
            this.Service = Service;
        }
        private async Task InitAsync()
        {
            Customers = new ObservableCollection<Customer>(await Service.GetAllCustomersAsync());
        }
        #region props
        private ObservableCollection<Customer> _Customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _Customers; }
            set
            {
                _Customers = value;
                OnPropertyChanged("Customers");
            }
        }
      
        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set
            {
                _SelectedCustomer = value;
              
                GetSalesByCustomer();
                CalculateDueAmountByCustomer();
                OnPropertyChanged("SelectedCustomer");
            }
        }
        private string _CustomerDue;
        public string CustomerDue
        {
            get { return _CustomerDue; }
            set
            {
                _CustomerDue = value;
                OnPropertyChanged("CustomerDue");
            }
        }
        private ObservableCollection<Sale> _CustomerSales;
        public ObservableCollection<Sale> CustomerSales
        {
            get { return _CustomerSales; }
            set
            {
                _CustomerSales = value;
                OnPropertyChanged("CustomerSales");
            }
        }
        private Sale _SelectedCustomerSale;
        public Sale SelectedCustomerSale
        {
            get { return _SelectedCustomerSale; }
            set
            {
                _SelectedCustomerSale = value;
                
                OnPropertyChanged("SelectedCustomerSale");
            }
        }
        #endregion
        #region function
        private void CalculateDueAmountByCustomer()
        {
            //try
            //{
            //    Customer c = new Customer();
            //    c = SelectedCustomer;
            //    CustomerDue = "";
            //    CustomerDue = "Due : " + context.Sales.Where(a => a.CustomerId == c.CustomerId).Select(a => a.Due).DefaultIfEmpty().Sum().ToString();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
        private void GetSalesByCustomer()
        {
            //CustomerSales = new ObservableCollection<Sale>(context.Sales.Where(a => a.CustomerId == SelectedCustomer.CustomerId).ToList());
        }
        #endregion
        #region Commands
        private ICommand _NewCustomerCommand;
        public ICommand NewCustomerCommand
        {
            get
            {
                if (this._NewCustomerCommand == null)
                    this._NewCustomerCommand = (ICommand)new RelayCommand(new Action<object>(this.NewCustomerFunction), (Predicate<object>)(a => true));
                return this._NewCustomerCommand;
            }
        }
        private void NewCustomerFunction(object obj)
        {
            SelectedCustomer = new Customer();
        }
        private ICommand _SaveCustomerCommand { get; set; }
        public ICommand SaveCustomerCommand
        {
            get
            {
                if (this._SaveCustomerCommand == null)
                    this._SaveCustomerCommand = (ICommand)new RelayCommand(new Action<object>(this.SaveCustomerFunction), (Predicate<object>)(a => this.SelectedCustomer != null));
                return this._SaveCustomerCommand;
            }
        }
        private void SaveCustomerFunction(object obj)
        {
          var id=  Service.SaveCustomerAsync(SelectedCustomer);
          
        }
        private ICommand _DeleteCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand
        {
            get
            {
                if (this._DeleteCustomerCommand == null)
                    this._DeleteCustomerCommand = (ICommand)new RelayCommand(new Action<object>(this.DeleteCustomerFunction), (Predicate<object>)(a => true));
                return this._DeleteCustomerCommand;
            }
        }
        private void DeleteCustomerFunction(object obj)
        {
            try
            {
                Service.DeleteCustomerAsync(SelectedCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region common
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
        bool _IsDataLoaded;
        public bool IsDataLoaded
        {
            get
            {
                return _IsDataLoaded;
            }
            set
            {
                if (_IsDataLoaded != value)
                {
                    _IsDataLoaded = value;
                    notify("IsDataLoaded");
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
            await Task.Run(() => InitAsync()).ContinueWith(a => { IsDataNotLoaded = false; IsDataLoaded = true; });
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

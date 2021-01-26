using LiquorShopService.Services;
using Rms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Purchases
{
    class PurchaseDataWindowViewModel:ViewModelBase
    {
        private DateTime _StartDatePurchase;
        public DateTime StartDatePurchase
        {
            get { return _StartDatePurchase; }
            set
            {
                _StartDatePurchase = value;
                notify("StartDatePurchase");
                // Task.Run(() => GetPurchaseByDateRangeAsync()); thread unsafe error
            }
        }
        private DateTime _EndDatePurchase;
        public DateTime EndDatePurchase
        {
            get { return _EndDatePurchase; }
            set
            {
                _EndDatePurchase = value;
                notify("EndDatePurchase");
                //Task.Run(()=> GetPurchaseByDateRangeAsync());
            }
        }
        ObservableCollection<Beverage> _Beverages;      
        public ObservableCollection<Beverage>   Beverages
        {
            get
            {
                return _Beverages;
            }
            set
            {
                if (_Beverages != value)
                {
                    _Beverages = value;
                    notify("Beverages");
                }
            }
        }

        Beverage _SelectedBeverage;
        public Beverage SelectedBeverage
        {
            get
            {
                return _SelectedBeverage;
            }
            set
            {
                if (_SelectedBeverage != value)
                {
                    _SelectedBeverage = value;
                    notify("SelectedBeverage");
                }
            }
        }
        public object TotalIncome { get; set; }

        ObservableCollection<Purchase> _Purchases;
        public ObservableCollection<Purchase> Purchases
        {
            get
            {
                return _Purchases;
            }
            set
            {
                if (_Purchases != value)
                {
                    _Purchases = value;
                    notify("Purchases");
                }
            }
        }

        public IPurchaseService Service;
         public PurchaseDataWindowViewModel(IPurchaseService Service)
        {
            this.Service = Service;            
        }

        private async Task Init()
        {
            Purchases = new ObservableCollection<Purchase>();
            Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesAsync());
        }

        internal async Task GetAllPurchasesForBeverageAsync()
        {
            Purchases =new ObservableCollection<Purchase>(await Service.FindPurchasesByBeverageAsync(SelectedBeverage));
            GetTotalExpense(Purchases);
           
        }

        private void GetTotalExpense(ObservableCollection<Purchase> Purchases)
        {
            var tot = Purchases.Sum(a => ((a.Quantity*a.UnitPrice)-a.Discount)+((a.Quantity*a.UnitPrice)-a.Discount)*a.Vat/100);
            TotalIncome = tot;
        }

        internal async Task GetAllPurchasesForBeverageAsync(DateTime ds, DateTime de)
        {
            Purchases = new ObservableCollection<Purchase>(await Service.FindPurchasesByBeverageAndDateAsync(ds, de, SelectedBeverage));
            GetTotalExpense(Purchases);
          
        }

     
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
            StartDatePurchase = DateTime.Today;
            EndDatePurchase = DateTime.Today;
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

        #region Commands

        #region GetPurchasesForBeverageCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetPurchasesForBeverageCommand" />
        /// </summary>
        private RelayCommand _GetPurchasesForBeverageCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetPurchasesForBeverageCommand
        {
            get
            {
                if (_GetPurchasesForBeverageCommand == null)
                { _GetPurchasesForBeverageCommand = new RelayCommand(GetPurchasesForBeverageCommand_Execute, GetPurchasesForBeverageCommand_CanExecute); }

                return _GetPurchasesForBeverageCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetPurchasesForBeverageCommand" />
        /// </summary>
        private void GetPurchasesForBeverageCommand_Execute(object obj)
        {
           Task.Run(()=> GetAllPurchasesForBeverageAsync());
        }

        /// <summary>
        /// Determines if <see cref="GetPurchasesForBeverageCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetPurchasesForBeverageCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region PurchasesByDateCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="PurchasesByDateCommand" />
        /// </summary>
        private RelayCommand _PurchasesByDateCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand PurchasesByDateCommand
        {
            get
            {
                if (_PurchasesByDateCommand == null)
                { _PurchasesByDateCommand = new RelayCommand(PurchasesByDateCommand_ExecuteAsync, PurchasesByDateCommand_CanExecute); }

                return _PurchasesByDateCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="PurchasesByDateCommand" />
        /// </summary>
        private async void PurchasesByDateCommand_ExecuteAsync(object obj)
        {
          await  GetAllPurchasesForBeverageAsync(StartDatePurchase, EndDatePurchase);
        }

        /// <summary>
        /// Determines if <see cref="PurchasesByDateCommand" /> is allowed to execute
        /// </summary>
        private Boolean PurchasesByDateCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #endregion

    }
}

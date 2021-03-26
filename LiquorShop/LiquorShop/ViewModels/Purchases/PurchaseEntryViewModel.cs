using LiquorShopService.Services;
using Rms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TescoWineShopSql;
using WpfApp1;

namespace LiquorShop.ViewModels.Purchases
{
  public partial  class PurchaseEntryViewModel:ViewModelBase
    {
        private IPurchaseService Service;
        IAccountingService AccountService;
        public PurchaseEntryViewModel(IPurchaseService Service,IAccountingService AccountService)
        {
            this.Service = Service;
            this.AccountService = AccountService;
            IsDataNotLoaded = true;           
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
            SelectedPurchaseVm = new LiquorShop.Classes.PurchaseVm();
            NewPurchase = new Purchase();
            PostPurchaseToLedger = false;
            IsCash = true;
            IsCredit = false;
            IsBank = false;
            await Task.Run(() => InitAsync()).ContinueWith(a => { IsDataNotLoaded = false;IsDataLoaded = true; });
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

        public async Task InitAsync()
        {
            try
            {
                await Task.Run(() => { Metrics = Enum.GetNames(typeof(Metrics)).ToList(); });               
                this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesAsync());
                this.Suppliers = new ObservableCollection<Supplier>(await Service.GetAllSuppliersAsync());
                this.Banks = new ObservableCollection<LedgerAccount>(await Service.GetAllBanksAsync());
                await Task.Run(() =>
                {
                    PostPurchaseToLedger = ((App)Application.Current).AutomateLedgerPost;
                    if (!PostPurchaseToLedger)
                    {
                        PostLedgerCheckEnabled = true;
                    }
                    else
                    {
                        PostLedgerCheckEnabled = false;
                    }
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void CalculateLineAmount()
        {
            try
            {
                var beverage = NewPurchase.Beverage;
                decimal d;
                var rate = decimal.TryParse(RateString, out d);
                if (beverage == null)
                {
                    return;
                }
                NewPurchase.Rate = d;

                if (NewPurchase.Quantity == 0)
                {
                    NewPurchase.Quantity = NewPurchase.MetricQuantity;
                }
                if (NewPurchase.Metric == TescoWineShopSql.Metrics.Case)
                {
                    NewPurchase.LineTotalAmount = d * NewPurchase.MetricQuantity;
                }
                else
                {
                    NewPurchase.LineTotalAmount = d * NewPurchase.Quantity;
                }

                NewPurchase.UnitPrice = NewPurchase.LineTotalAmount / NewPurchase.Quantity;
            }
            catch(Exception ex)
            {
                //throw ex;
                throw ex;
            }
        }

        internal void CalculateActualUnitQuantity()
        {
            if (NewPurchase.Beverage == null)
            {
                return;
            }
            UnitQuantity = 0;
            if (NewPurchase != null)
            {
                var beverage = NewPurchase.Beverage;
                if (beverage == null)
                {
                    return;
                }
                NewPurchase.Metric = SelectedMetric;
                var volume = NewPurchase.Beverage.Volume;
                if (volume == null)
                {
                    throw new Exception("No Volume for this item");
                }
                var volumes = ((App)Application.Current).DrinkVolumes;

                ResolveActualQuantity(volume, volumes);

            }
        }

        private void ResolveActualQuantity(int? volume, Dictionary<string, string> volumes)
        {
            switch (NewPurchase.Metric)
            {
                case TescoWineShopSql.Metrics.Case:
                    foreach (var item in volumes)
                    {
                        if (item.Key.Contains(volume.ToString()))
                        {
                            var value = item.Value;
                            int uq;
                            if (!
                             int.TryParse(value, out uq))
                            {
                                throw new Exception("cannot resolve how many in Case");
                            }
                            UnitQuantity = uq * NewPurchase.MetricQuantity;
                            NewPurchase.Quantity = UnitQuantity;
                            break;
                        }
                    }
                    break;
                case TescoWineShopSql.Metrics.Bottle:
                    UnitQuantity = NewPurchase.MetricQuantity;
                    NewPurchase.Quantity = UnitQuantity;
                    break;
                case TescoWineShopSql.Metrics.Litre:

                    MessageBox.Show("I guess you want to select Bottle");
                    //NewPurchase.Quantity = UnitQuantity;
                    break;


            }
        }

        private void GetActualUnit()
        {
            CalculateActualUnitQuantity();
            if (NewPurchase != null)
            {
                if (NewPurchase.Rate > 0)
                {
                    CalculateLineAmount();
                }
            }
        }
      

    }
}

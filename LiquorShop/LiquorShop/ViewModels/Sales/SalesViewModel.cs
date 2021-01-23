
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;
using Rms;
using System.Windows;
using LiquorShop.Classes;
using System.ServiceModel;
using Rms.WebService;
using LiquorShopService.Services;
using WpfApp1;

namespace LiquorShop.ViewModels.Sales
{
   public partial class SalesViewModel : ViewModelBase
    {
        public ISalesService Service;
        public IAccountingService AccountService;
        public SalesViewModel(ISalesService Service,IAccountingService AccountService)
        {
            this.Service = Service;
            this.AccountService = AccountService;
        }
        public async Task Init()
        {
            BeverageCategories = new ObservableCollection<BeverageCategory>(await Service.GetAllBeverageCategoriesAsync());
            this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllPopularBeveragesAsync());
           // this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesByDrinkTypeAsync(SelectedDrinkType));
            Customers = new ObservableCollection<LedgerAccount>(await Service.GetAllCustomerAccountsAsync());           
            this.Banks = new ObservableCollection<LedgerAccount>(await Service.GetAllBanksAsync());
            SelectedBeverages = new ObservableCollection<BeverageVm>();
            DrinkTypes = Enum.GetNames(typeof(Drinktype)).ToList();            
            SaleOrder = new SaleOrder();
            PostPurchaseToLedger = ((App)Application.Current).AutomateLedgerPost;
           // BeverageCategories.Insert(0, new BeverageCategory { BeverageCategoryName = "Popular" });
            if (!PostPurchaseToLedger)
            {
                PostLedgerCheckEnabled = true;
            }
            else
            {
                PostLedgerCheckEnabled = false;
            }
            if (((App)Application.Current).UseBarCodeService)
            {
                StartWebService();
            }
            SelectedSaleVm = new SaleVm();
            IsDiscountApplied = false;
            IsDiscountNotApplied = true;
            IsCash = true;
        }
        BarCodeService rservice;
       public ServiceHost host;
        
        private void StartWebService()
        {
            try
            {
                //myServiceObject = ContainerHelper.Container.Resolve<BarCodeService>();
                //host = new ServiceHost(myServiceObject);
                //host.Open();

                rservice = new BarCodeService();
                host = new ServiceHost(rservice);
                if(host.State==CommunicationState.Opened)
                {
                    return;
                }
                host.Open();
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
            finally
            {
               
            }
        }

        public decimal SaleTotal { get; set; }
        public void UpdateSelectedBeverages(int beverageid)
        {

            BeverageVm svm = new BeverageVm();
            //Beverages =new ObservableCollection<Beverage>(context.Beverages.ToList());
            SelectedBeverage = Beverages.Where(a => a.BeverageId == beverageid).FirstOrDefault();
            if (SelectedBeverage != null)
            {
                svm.BeverageId = beverageid;
                svm.Name = SelectedBeverage.Name;
                svm.Price = SelectedBeverage.Price;
               // svm.Total = 0;
                svm.Volume = SelectedBeverage.Volume;
               // svm.DrinkType = SelectedBeverage.DrinkType;
                var yes = SelectedSaleVm.BevarageVms.Where(a => a.BeverageId == svm.BeverageId).FirstOrDefault();
                if (yes == null)
                {
                    svm.Quantity = 1;
                    svm.Total = svm.Quantity * svm.Price;
                    SelectedSaleVm.BevarageVms.Add(svm);
                    SaleTotal += svm.Total??0;
                    SelectedSaleVm.TotalAmount = SaleTotal;
                   
                }
                else
                {
                    yes.Quantity++;
                    yes.Total = yes.Quantity * yes.Price;
                    SaleTotal += yes.Total??0;
                    SelectedSaleVm.TotalAmount = SaleTotal;
                    SelectedSaleVm.AmountAfterDiscount = SelectedSaleVm.TotalAmount;
                }

            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            SelectedSaleVm.TotalAmount = SelectedSaleVm.BevarageVms.Sum(a => a.Total)??0;
            
            SelectedSaleVm.AmountAfterDiscount = SelectedSaleVm.TotalAmount;
            SelectedSaleVm.AmountPaid = SelectedSaleVm.AmountAfterDiscount;
        }

        private void CalculateDiscountInit()
        {
            if (SelectedBeverages.Count < 1)
            {
                return;
            }
            var nettotal = SelectedBeverages.Sum(a => a.Total);
            Discount =  nettotal-AmountPaid;
            Discount = Math.Abs(Discount??0);
        }
        private async Task GetBeveragesAsync()
        {
            //if (SelectedDrinkType == Drinktype.Popular)
            //{
            //    this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllPopularBeveragesAsync());
            //}
            //else
            //{
            //    this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesByDrinkTypeAsync(SelectedDrinkType));
            //}
            if (SelectedBeverageCategory.BeverageCategoryName == "Popular")
            {
                Beverages = new ObservableCollection<Beverage>(await Service.GetAllPopularBeveragesAsync());
            }
            else
            {
                this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesByCategoryAsync(SelectedBeverageCategory.BeverageCategoryId));
            }
        }
        private async Task SearchBeverageFunctionAsync()
        {
            if (SearchBeverage.Length > 0)
            {
                this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesByName(SearchBeverage));
            }
            else
            {
                this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllPopularBeveragesAsync());
            }
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
            await Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false;IsDataLoaded = true; });

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

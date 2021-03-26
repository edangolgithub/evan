using Rms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TescoWineShopSql;
using System.Windows.Input;
using LiquorShopService.Services;

namespace LiquorShop.ViewModels.Beverages
{
    public partial class BeverageViewModel:ViewModelBase
    {
        public IBeverageService Service;
        public BeverageViewModel(IBeverageService Service)
        {
            this.Service = Service;
        }
        BeverageCategory _SelectedBeverageCategory;
        public BeverageCategory SelectedBeverageCategory
        {
            get
            {
                return _SelectedBeverageCategory;
            }
            set
            {
                if (_SelectedBeverageCategory != value)
                {
                    _SelectedBeverageCategory = value;
                    notify("SelectedBeverageCategory");
                    Task.Run(() => GetBeveragesAsync());
                }
            }
        }
        ObservableCollection<BeverageCategory> _BeverageCategories;
        public ObservableCollection<BeverageCategory> BeverageCategories
        {
            get
            {
                return _BeverageCategories;
            }
            set
            {
                if (_BeverageCategories != value)
                {
                    _BeverageCategories = value;
                    notify("BeverageCategories");
                }
            }
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
        public async Task Init()
        {
            DrinkTypes = Enum.GetNames(typeof(Drinktype)).ToList(); 
            Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesAsync());
            BeverageCategories = new ObservableCollection<BeverageCategory>(await Service.GetAllBeverageCategoriesAsync());
        }
        List<string> _DrinkTypes;
        public List<string> DrinkTypes
        {
            get
            {
                return _DrinkTypes;
            }
            set
            {
                if (_DrinkTypes != value)
                {
                    _DrinkTypes = value;
                    notify("DrinkTypes");
                }
            }
        }
        ObservableCollection<Beverage> _Beverages;
        public ObservableCollection<Beverage> Beverages
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

        public void InitBeverageViewModel()
        {
            //try
            //{
            //    this.Beverages = new ObservableCollection<Beverage>(context.Beverages.Where(a => a.DrinkType ==TescoWineShopSql.Drinktype).ToList());
            //    ShowBtn = Visibility.Collapsed;
            //    sh = Visibility.Collapsed;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
              private List<Sale> _duecus;
        public List<Sale> duecus
        {
            get { return _duecus; }
            set
            {
                _duecus = value;
                notify("duecus");
            }
        }
        private string _TotalIncomeB;
        public string TotalIncomeB
        {
            get { return _TotalIncomeB; }
            set
            {
                _TotalIncomeB = value;
                notify("TotalIncomeB");
            }
        }
        private string _TotalExpenseB;
        public string TotalExpenseB
        {
            get { return _TotalExpenseB; }
            set
            {
                _TotalExpenseB = value;
                notify("TotalExpenseB");
            }
        }
        private string _TotalDueB;
        public string TotalDueB
        {
            get { return _TotalDueB; }
            set
            {
                _TotalDueB = value;
                notify("TotalDueB");
            }
        }
       
        private ObservableCollection<Sale> _SalesMoneyDue;
        public ObservableCollection<Sale> SalesMoneyDue
        {
            get { return _SalesMoneyDue; }
            set
            {
                _SalesMoneyDue = value;
                notify("SalesMoneyDue");
            }
        }
        


        #region command
        private ICommand _NewBeverageCommand;
        public ICommand NewBeverageCommand
        {
            get
            {
                if (this._NewBeverageCommand == null)
                    this._NewBeverageCommand = (ICommand)new RelayCommand(new Action<object>(this.NewBeverageFunction), (Predicate<object>)(a => true));
                return this._NewBeverageCommand;
            }
        }
        private void NewBeverageFunction(object obj)
        {
           // SelectedBeverage = new Beverage();
        }


        private ICommand _DeleteBeverageCommand { get; set; }
        public ICommand DeleteBeverageCommand
        {
            get
            {
                if (this._DeleteBeverageCommand == null)
                    this._DeleteBeverageCommand = (ICommand)new RelayCommand(new Action<object>(this.DeleteBeverageFunction), (Predicate<object>)(a => true));
                return this._DeleteBeverageCommand;
            }
        }
        private void DeleteBeverageFunction(object obj)
        {
            // try
            // {
            //     if (Service.DeleteBeverageAsync(SelectedBeverage).Result==1)
            //     {
            //         //MessageBox.Show("Deleted");
            //     }
                    
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }
        }
        private ICommand _ShowMoreBeverageInfoCmd { get; set; }
        public ICommand ShowMoreBeverageInfoCmd
        {
            get
            {
                if (this._ShowMoreBeverageInfoCmd == null)
                    this._ShowMoreBeverageInfoCmd = (ICommand)new RelayCommand(new Action<object>(this.ShowMoreBeverageInfoCmdFunction), (Predicate<object>)(a => true));
                return this._ShowMoreBeverageInfoCmd;
            }
        }
        private void ShowMoreBeverageInfoCmdFunction(object obj)
        {
            // if (SelectedBeverage != null)
            // {
            //     sh = Visibility.Visible;
               
            // }
        }
        #endregion

        #region function
        public void GetSalesByBeverage()
        {
            // if (SelectedBeverage != null)
            // {
            //     //this.Sales = new ObservableCollection<Sale>(context.Sales.Where(a => a.Beverage.BeverageId == SelectedBeverage.BeverageId).ToList());
            // }
        }
        internal void GetPurchasesByBeverage()
        {
            // if (SelectedBeverage != null)
            // {
            //     //this.Purchases = new ObservableCollection<Purchase>(context.Purchases.Where(a => a.Beverage.BeverageId == SelectedBeverage.BeverageId).ToList());
            // }
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

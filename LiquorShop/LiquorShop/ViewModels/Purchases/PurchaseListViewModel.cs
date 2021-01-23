//using EvanDangol.Wpf;
//using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
//using TescoWine.BusinessLayer;
//using wsql;
using TescoWineShopSql;
using Rms;
using System.Data.Entity.Validation;
using WpfApp1;
using System.Data.Entity;
using LiquorShopService.Services;
using System.Windows.Data;

namespace LiquorShop.ViewModels.Purchases
{
    public partial class PurchaseListViewModel : ViewModelBase
    {

        private void GetInvoices(DateTime StartDate,DateTime EndDate,int InvoiceNumber=0,Beverage bev=null)
        {
            if (InvoiceNumber < 1)
            {


                var data =
                    Purchases.Join(Invoices, b => b.InvoiceId, a => a.InvoiceId, (a, b) =>
                           new
                           {
                               BeverageName = a.Beverage.Name,
                               Quantity = a.Quantity,
                               Metric = a.Metric,
                               Rate = a.Rate,
                               Amount = a.LineTotalAmount,
                               InvoiceDate = b.Date,
                               InvoiceNumber = b.InvoiceNumber,
                               SupplierName = b.Supplier.SupplierName
                           }
                    ).Where(a => a.InvoiceDate.Date <= EndDate.Date && a.InvoiceDate.Date >= StartDate).ToList();
                App.Current.Dispatcher.Invoke(() => {
                    InvoiceRVIew = CollectionViewSource.GetDefaultView(data);
                    InvoiceRVIew.GroupDescriptions.Add(new PropertyGroupDescription("InvoiceNumber"));
                });
            }
            else
            {
                StartDatePurchase = DateTime.Today;
                EndDate = DateTime.Today;
                var data =
                   Purchases.Join(Invoices, b => b.InvoiceId, a => a.InvoiceId, (a, b) =>
                          new
                          {
                              BeverageName = a.Beverage.Name,
                              Quantity = a.Quantity,
                              Metric = a.Metric,
                              Rate = a.Rate,
                              Amount = a.LineTotalAmount,
                              InvoiceDate = b.Date,
                              InvoiceNumber = b.InvoiceNumber,
                              SupplierName = b.Supplier.SupplierName,
                              Invoice=b
                           
                          }
                   ).Where(a => a.InvoiceNumber==InvoiceNumber).ToList();
                App.Current.Dispatcher.Invoke(() => {
                    InvoiceRVIew = CollectionViewSource.GetDefaultView(data);
                    InvoiceRVIew.GroupDescriptions.Add(new PropertyGroupDescription("InvoiceNumber"));
                });

                
              
            }
         Task.Run(()=>   GetInfoPurchaseAsync(StartDate, EndDate, InvoiceNumber));

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
            StartDatePurchase = DateTime.Today;
            EndDatePurchase = DateTime.Today;
            BeforeBeveragesLoadFunction();
            await Task.Run(() => InitPurchasesAsync()).ContinueWith(a => { IsDataNotLoaded = false; IsDataLoaded = true; });
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
        private void BeforeBeveragesLoadFunction()
        {
            Beverage b = new Beverage { Name = "Loading ......." };
            Beverages = new ObservableCollection<Beverage>(new List<Beverage> { b});
        }

        public IPurchaseService Service;
        public PurchaseListViewModel(IPurchaseService Service)
        {
            this.Service = Service;
            IsDataLoaded = true; ;
            IsDataNotLoaded = false;
           
        }



        private async Task InitPurchasesAsync()
        {
            try
            {
                Metrics = Enum.GetNames(typeof(Metrics)).ToList();
                //await Task.Run(() => GetPurchaseByDateRangeAsync());
                this.Invoices = new ObservableCollection<Invoice>(await Service.GetAllInvoicesAsync());
                this.Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesAsync());
                this.Suppliers = new ObservableCollection<Supplier>(await Service.GetAllSuppliersAsync());
                this.Purchases = new ObservableCollection<Purchase>(await Service.GetAllPurchasesAsync());
                if (StartDatePurchase != null)
                {
                    await Task.Run(() => GetInvoices(StartDatePurchase, EndDatePurchase));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task GetPurchaseByDateRangeAsync()
        {

            this.Purchases = new ObservableCollection<Purchase>
                (await Service.FindPurchasesByDateAsync(StartDatePurchase, EndDatePurchase));
        }
     



        private decimal _VatPercent;

        public decimal VatPercent
        {
            get { return _VatPercent; }
            set
            {
                _VatPercent = value;
                notify("VatPercent");
            }
        }


        private async Task GetInfoPurchaseAsync(DateTime start,DateTime end,int invoicenumber=0)
        {
            decimal total = 0;
            if(invoicenumber<=0)
            {
                total =await Service.GetPurchaseTotalAmountAsync(start, end);
            }
            else
            {
              total=await  Service.GetInvoiceTotalAmountAsync(invoicenumber);
            }
            TotalInvoiceAmount = total;
           
        }



        internal async Task GetTotalStockAsync(Beverage b)
        {
            try
            {
                if (b == null)
                    return;
                if (b.BeverageId != 0)
                {
                    int total = await Service.GetBeverageStockAsync(b);
                    TotalStock = b.Name + " In Stock : " + (total).ToString();
                    scount = total;
                    GetStockColor(total);
                }
            }
            catch (Exception e)
            {
                throw e;
                //  MessageBox.Show(e.Message);
            }
        }






        private string _TotalExpenses;

        public string TotalExpenses
        {
            get { return _TotalExpenses; }
            set
            {
                _TotalExpenses = value;
                notify("TotalExpenses");
            }
        }

        private void GetTotalExpense(ObservableCollection<Purchase> Purchases)
        {
            decimal t = 0;
            decimal? discount = 0;
            decimal? tot = 0;
            decimal? vat = 0;
            decimal? Total = 0;
            foreach (var item in Purchases)
            {
                t = item.Quantity * item.UnitPrice;
                discount = item.Discount ?? 0;
                tot = t - discount;
                vat = item.VatAmount;
                Total += tot + vat;
            }
            var v = Purchases.Sum(b => ((b.Quantity * (b.UnitPrice)) - b.Discount ?? 0) + b.VatAmount);
            TotalExpenses = "Total Expenses Of Displayed Purchases : " + Total.ToString();
        }

        private void GetTotalVat(ObservableCollection<Purchase> Purchases)
        {
            try
            {
                TotalExpenseP = "Total Vat : " + Purchases
                    .Select(a => a.VatAmount)
                    .DefaultIfEmpty().Sum().ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task GetPurchaseBySearchBeverageAsync()
        {
            if (SearchPurchase != null)
            {
                this.Purchases = new ObservableCollection<Purchase>
                    (await Service.FindPurchasesByBeverageAsync(SearchPurchase));
            }
        }
        private async Task getTotalPurchasedAsync(Beverage SearchPurchase)
        {
            if (SearchPurchase != null)
            {
                try
                {
                    TotalPurchased = SearchPurchase.Name + " Total Purchased : " + await Service.GetTotalPurchaseAsync(SearchPurchase);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private string _TotalExpenseP;
        public string TotalExpenseP
        {
            get { return _TotalExpenseP; }
            set
            {
                _TotalExpenseP = value;
                notify("TotalExpenseP");
            }
        }
        private string _TotalExpenseBeverage;
        public string TotalExpenseBeverage
        {
            get { return _TotalExpenseBeverage; }
            set
            {
                _TotalExpenseBeverage = value;
                notify("TotalExpenseBeverage");
            }
        }
        private void GetTotalOnly(ObservableCollection<Purchase> Purch)
        {
            try
            {
                if (SearchPurchase != null)
                {
                    TotalExpenseBeverage = SearchPurchase.Name + " Total Expense : " + GetTotalabcd(Purch)[1];
                }
                else
                {

                    TotalExpenseBeverage = " Total Expense : " + GetTotalabcd(Purch)[1];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<decimal?> GetTotalabcd(ObservableCollection<Purchase> list)
        {
            try
            {
                decimal t = 0;
                decimal? discount = 0;
                decimal? tot = 0;
                decimal? vat = 0;
                decimal? Total = 0;
                decimal? totwovat = 0;
                foreach (var item in list.Cast<Purchase>())
                {
                    t = item.Quantity * item.UnitPrice;
                    discount = item.Discount ?? 0;
                    tot = t - discount;
                    totwovat += tot;
                    vat = item.VatAmount;
                    Total += tot + vat;
                }
                List<decimal?> dl = new List<decimal?>();
                dl.Add(Total);
                dl.Add(totwovat);
                return dl;
            }
            catch (Exception w)
            {
                throw w;
                //  MessageBox.Show(w.Message);
            }
            //  return null;
        }

       
    }
}

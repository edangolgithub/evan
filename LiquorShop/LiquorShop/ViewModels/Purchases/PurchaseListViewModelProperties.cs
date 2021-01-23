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
using LiquorShop.Classes;
using WpfApp1;
using System.ComponentModel;

namespace LiquorShop.ViewModels.Purchases
{
    public partial class PurchaseListViewModel : ViewModelBase
    {
        bool _PopUpOpen;
        public bool PopUpOpen
        {
            get
            {
                return _PopUpOpen;
            }
            set
            {
                if (_PopUpOpen != value)
                {
                    _PopUpOpen = value;
                    notify("PopUpOpen");
                }
            }
        }
        Invoice _SelectedInvoice;
        public Invoice SelectedInvoice
        {
            get
            {
                return _SelectedInvoice;
            }
            set
            {
                if (_SelectedInvoice != value)
                {
                    _SelectedInvoice = value;
                    notify("SelectedInvoice");
                }
            }
        }

        int _SelectedInvoiceNumber;
        public int SelectedInvoiceNumber
        {
            get
            {
                return _SelectedInvoiceNumber;
            }
            set
            {
                if (_SelectedInvoiceNumber != value)
                {
                    _SelectedInvoiceNumber = value;
                    
                    notify("SelectedInvoiceNumber");
                    if (_SelectedInvoiceNumber >= 0)
                    {
                      Task.Run(()=>  GetSelectedInvoiceAsync());
                    }
                }
            }
        }

        private async Task GetSelectedInvoiceAsync()
        {
            SelectedInvoice =await Service.FindInvoiceByInvoiceNumberAsync(SelectedInvoiceNumber);
        }

        ICollectionView _InvoiceRVIew;
        public ICollectionView InvoiceRVIew
        {
            get
            {
                return _InvoiceRVIew;
            }
            set
            {
                if (_InvoiceRVIew != value)
                {
                    _InvoiceRVIew = value;
                    notify("InvoiceRVIew");
                }
            }
        }

        ObservableCollection<Invoice> _Invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get
            {
                return _Invoices;
            }
            set
            {
                if (_Invoices != value)
                {
                    _Invoices = value;
                    notify("Invoices");
                }
            }
        }


        int _InvoiceNumber;
        public int InvoiceNumber
        {
            get
            {
                return _InvoiceNumber;
            }
            set
            {
                if (_InvoiceNumber != value)
                {
                    _InvoiceNumber = value;
                    notify("InvoiceNumber");
                }
            }
        }

        decimal _TotalInvoiceAmount;
        public decimal TotalInvoiceAmount
        {
            get
            {
                return _TotalInvoiceAmount;
            }
            set
            {
                if (_TotalInvoiceAmount != value)
                {
                    _TotalInvoiceAmount = value;
                    notify("TotalInvoiceAmount");
                }
            }
        }
        decimal? _TotalVatAmount;
        public decimal? TotalVatAmount
        {
            get
            {
                return _TotalVatAmount;
            }
            set
            {
                if (_TotalVatAmount != value)
                {
                    _TotalVatAmount = value;
                    notify("TotalVatAmount");
                }
            }
        }
        decimal? _TotalDiscountAmount;
        public decimal? TotalDiscountAmount
        {
            get
            {
                return _TotalDiscountAmount;
            }
            set
            {
                if (_TotalDiscountAmount != value)
                {
                    _TotalDiscountAmount = value;
                    notify("TotalDiscountAmount");
                }
            }
        }
        decimal? _TotalActualAmount;
        public decimal? TotalActualAmount
        {
            get
            {
                return _TotalActualAmount;
            }
            set
            {
                if (_TotalActualAmount != value)
                {
                    _TotalActualAmount = value;
                    notify("TotalActualAmount");
                }
            }
        }

        private Beverage _SearchPurchase;
        public Beverage SearchPurchase
        {
            get { return _SearchPurchase; }
            set
            {
                _SearchPurchase = value;

                notify("SearchPurchase");
                Task.Run(() => GetPurchaseBySearchBeverageAsync());
            }
        }
        List<string> _Metrics;
        public List<string> Metrics
        {
            get
            {
                return _Metrics;
            }
            set
            {
                if (_Metrics != value)
                {
                    _Metrics = value;
                    notify("Metrics");
                }
            }
        }
        private ObservableCollection<Supplier> _Suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get { return _Suppliers; }
            set
            {
                _Suppliers = value;
                notify("Suppliers");
            }
        }
        private string _TotalPurchased;
        public string TotalPurchased
        {
            get { return _TotalPurchased; }
            set
            {
                _TotalPurchased = value;
                notify("TotalPurchased");
            }
        }
        private ObservableCollection<Purchase> _Purchases;
        public ObservableCollection<Purchase> Purchases
        {
            get { return _Purchases; }
            set
            {
                _Purchases = value;
                notify("Purchases");
                
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


        private Purchase _SelectedPurchase;
        public Purchase SelectedPurchase
        {
            get { return _SelectedPurchase; }
            set
            {
                _SelectedPurchase = value;
                notify("SelectedPurchase");
            }
        }
       

        private double _Vat;

        public double Vat
        {
            get { return _Vat; }
            set
            {
                _Vat = value;
                notify("Vat");
            }
        }
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
        private string _TotalStock;
        public string TotalStock
        {
            get { return _TotalStock; }
            set
            {
                _TotalStock = value;
                notify("TotalStock");
            }
        }
        private Sale _SelectedSale;
        public Sale SelectedSale
        {
            get { return _SelectedSale; }
            set
            {
                _SelectedSale = value;
                // GetTotalAmount();
                notify("SelectedSale");
            }
        }
        private SolidColorBrush _stockcolor;
        public SolidColorBrush stockcolor
        {
            get { return _stockcolor; }
            set
            {
                _stockcolor = value;
                notify("stockcolor");
            }
        }
        private int _scount;
        public int scount
        {
            get { return _scount; }
            set
            {
                _scount = value;
                notify("scount");
            }
        }
        private void GetStockColor(int total)
        {
            if (total <= 0)
            {
                stockcolor = new SolidColorBrush(Colors.Red);
            }
            else if (total > 0 && total <= 10)
            {
                stockcolor = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                stockcolor = new SolidColorBrush(Colors.Green);
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

    }
}

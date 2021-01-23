using LiquorShop.Classes;
using Rms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Sales
{
    public partial class SalesViewModel : ViewModelBase
    {
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


        string _BankName;
        public string BankName
        {
            get
            {
                return _BankName;
            }
            set
            {
                if (_BankName != value)
                {
                    _BankName = value;
                    notify("BankName");
                }
            }
        }


        string _ChequeNumber;
        public string ChequeNumber
        {
            get
            {
                return _ChequeNumber;
            }
            set
            {
                if (_ChequeNumber != value)
                {
                    _ChequeNumber = value;
                    notify("ChequeNumber");
                }
            }
        }


        bool _IsCash;
        public bool IsCash
        {
            get
            {
                return _IsCash;
            }
            set
            {
                if (_IsCash != value)
                {
                    _IsCash = value;
                    notify("IsCash");
                }
            }
        }
        bool _IsCredit;
        public bool IsCredit
        {
            get
            {
                return _IsCredit;
            }
            set
            {
                if (_IsCredit != value)
                {
                    _IsCredit = value;
                    notify("IsCredit");
                }
            }
        }

        bool _IsBank;
        public bool IsBank
        {
            get
            {
                return _IsBank;
            }
            set
            {
                if (_IsBank != value)
                {
                    _IsBank = value;
                    notify("IsBank");
                }
            }
        }
        bool _IsDiscountNotApplied;
        public bool IsDiscountNotApplied
        {
            get
            {
                return _IsDiscountNotApplied;
            }
            set
            {
                if (_IsDiscountNotApplied != value)
                {
                    _IsDiscountNotApplied = value;
                    notify("IsDiscountNotApplied");
                }
            }
        }
        bool _IsDiscountApplied;
        public bool IsDiscountApplied
        {
            get
            {
                return _IsDiscountApplied;
            }
            set
            {
                if (_IsDiscountApplied != value)
                {
                    _IsDiscountApplied = value;
                    notify("IsDiscountApplied");
                }
            }
        }


        SaleVm _SelectedSaleVm;
        public SaleVm SelectedSaleVm
        {
            get
            {
                return _SelectedSaleVm;
            }
            set
            {
                if (_SelectedSaleVm != value)
                {
                    _SelectedSaleVm = value;
                    notify("SelectedSaleVm");
                }
            }
        }

        LedgerAccount _SelectedCustomer;
        public LedgerAccount SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }
            set
            {
                if (_SelectedCustomer != value)
                {
                    _SelectedCustomer = value;
                    notify("SelectedCustomer");
                }
            }
        }
        LedgerAccount _SelectedBankAccount;
        public LedgerAccount SelectedBankAccount
        {
            get
            {
                return _SelectedBankAccount;
            }
            set
            {
                if (_SelectedBankAccount != value)
                {
                    _SelectedBankAccount = value;
                    notify("SelectedBankAccount");
                }
            }
        }

        ObservableCollection<LedgerAccount> _Banks;
        public ObservableCollection<LedgerAccount> Banks
        {
            get
            {
                return _Banks;
            }
            set
            {
                if (_Banks != value)
                {
                    _Banks = value;
                    notify("Banks");
                }
            }
        }

        ObservableCollection<LedgerAccount> _Customers;
        public ObservableCollection<LedgerAccount> Customers
        {
            get
            {
                return _Customers;
            }
            set
            {
                if (_Customers != value)
                {
                    _Customers = value;
                    notify("Customers");
                }
            }
        }


        bool _PostPurchaseToLedger;
        public bool PostPurchaseToLedger
        {
            get
            {
                return _PostPurchaseToLedger;
            }
            set
            {
                if (_PostPurchaseToLedger != value)
                {
                    _PostPurchaseToLedger = value;
                    notify("PostPurchaseToLedger");
                }
            }
        }
        bool _PostLedgerCheckEnabled;
        public bool PostLedgerCheckEnabled
        {
            get
            {
                return _PostLedgerCheckEnabled;
            }
            set
            {
                if (_PostLedgerCheckEnabled != value)
                {
                    _PostLedgerCheckEnabled = value;
                    notify("PostLedgerCheckEnabled");
                }
            }
        }

        SaleOrder _SaleOrder;
        public SaleOrder SaleOrder
        {
            get
            {
                return _SaleOrder;
            }
            set
            {
                if (_SaleOrder != value)
                {
                    _SaleOrder = value;
                    notify("SaleOrder");
                }
            }
        }

        string _SearchBeverage;
        public string SearchBeverage
        {
            get
            {
                return _SearchBeverage;
            }
            set
            {
                if (_SearchBeverage != value)
                {
                    _SearchBeverage = value;
                    notify("SearchBeverage");
                   Task.Run(()=> SearchBeverageFunctionAsync());
                }
            }
        }

        decimal _AmountPaid;
        public decimal AmountPaid
        {
            get
            {
                return _AmountPaid;
            }
            set
            {
                if (_AmountPaid != value)
                {
                    _AmountPaid = value;
                    notify("AmountPaid");
                    CalculateDiscountInit();
                }
            }
        }


        decimal? _NetTotal;
        public decimal? NetTotal
        {
            get
            {
                return _NetTotal;
            }
            set
            {
                if (_NetTotal != value)
                {
                    _NetTotal = value;
                    notify("NetTotal");
                    
                }
            }
        }
        decimal? _Discount;
        public decimal? Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                if (_Discount != value)
                {
                    _Discount = value;
                    notify("Discount");
                }
            }
        }


        private ObservableCollection<Beverage> _Beverages;
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Beverages; }
            set
            {
                _Beverages = value;
                notify("Beverages");
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

                _SelectedBeverage = value;
                notify("SelectedBeverage");
                // UpdateSelectedBeverages();

            }
        }
        ObservableCollection<BeverageVm> _SelectedBeverages;
        public ObservableCollection<BeverageVm> SelectedBeverages
        {
            get
            {
                return _SelectedBeverages;
            }
            set
            {
                if (_SelectedBeverages != value)
                {
                    _SelectedBeverages = value;
                    notify("SelectedBeverages");
                }
            }
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


        Drinktype _SelectedDrinkType ;
        public Drinktype SelectedDrinkType
        {
            get
            {
                return _SelectedDrinkType;
            }
            set
            {
                if (_SelectedDrinkType  != value)
                {
                    _SelectedDrinkType  = value;
                    notify("SelectedDrinkType");
                  
                }
            }
        }

       
    }
}

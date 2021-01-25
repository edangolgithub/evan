using LiquorShop.Classes;
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
    public partial class PurchaseEntryViewModel : ViewModelBase
    {
      

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
                }
            }
        }
        decimal _Due;
        public decimal Due
        {
            get
            {
                return _Due;
            }
            set
            {
                if (_Due != value)
                {
                    _Due = value;
                    notify("Due");
                }
            }
        }


        Purchase _SelectedPurchaseEntry;
        public Purchase SelectedPurchaseEntry
        {
            get
            {
                return _SelectedPurchaseEntry;
            }
            set
            {
                if (_SelectedPurchaseEntry != value)
                {
                    _SelectedPurchaseEntry = value;
                    notify("SelectedPurchaseEntry");
                }
            }
        }

        Invoice _Invoice;
        public Invoice voice
        {
            get
            {
                return _Invoice;
            }
            set
            {
                if (_Invoice != value)
                {
                    _Invoice = value;
                    notify("voice");
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

        string _PurchaseNarration;
        public string PurchaseNarration
        {
            get
            {
                return _PurchaseNarration;
            }
            set
            {
                if (_PurchaseNarration != value)
                {
                    _PurchaseNarration = value;
                    notify("PurchaseNarration");
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
        decimal _PurchaseSubTotal;
        public decimal PurchaseSubTotal
        {
            get
            {
                return _PurchaseSubTotal;
            }
            set
            {
                if (_PurchaseSubTotal != value)
                {
                    _PurchaseSubTotal = value;
                    notify("PurchaseSubTotal");
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


        string _RateString;
        public string RateString
        {
            get
            {
                return _RateString;
            }
            set
            {
                if (_RateString != value)
                {
                    _RateString = value;
                    notify("RateString");
                    CalculateLineAmount();
                }
            }
        }
        PurchaseVm _SelectedPurchaseVm;
        public PurchaseVm SelectedPurchaseVm
        {
            get
            {
                return _SelectedPurchaseVm;
            }
            set
            {
                if (_SelectedPurchaseVm != value)
                {
                    _SelectedPurchaseVm = value;
                    notify("SelectedPurchaseVm");
                }
            }
        }

        bool _IsDirty;
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                if (_IsDirty != value)
                {
                    _IsDirty = value;
                    notify("IsDirty");
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
        private Purchase _SelectedEntryPurchase;
        public Purchase SelectedEntryPurchase
        {
            get { return _SelectedEntryPurchase; }
            set
            {
                _SelectedEntryPurchase = value;
                notify("SelectedEntryPurchase");
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
        private Supplier _SelectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return _SelectedSupplier; }
            set
            {
                _SelectedSupplier = value;
                notify("SelectedSupplier");
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
        Purchase _NewPurchase;
        public Purchase NewPurchase
        {
            get
            {
                return _NewPurchase;
            }
            set
            {

                _NewPurchase = value;
                notify("NewPurchase");

            }
        }
        Metrics? _SelectedMetric;
        public Metrics? SelectedMetric
        {
            get
            {
                return _SelectedMetric;
            }
            set
            {
                if (_SelectedMetric != value)
                {
                    _SelectedMetric = value;
                    notify("SelectedMetric");
                    GetActualUnit();
                }
            }
        }


        int _UnitQuantity;
        public int UnitQuantity
        {
            get
            {
                return _UnitQuantity;
            }
            set
            {
                if (_UnitQuantity != value)
                {
                    _UnitQuantity = value;
                    notify("UnitQuantity");
                }
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
    }
}

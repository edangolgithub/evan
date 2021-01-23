using Rms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;
using System.Collections.ObjectModel;

namespace LiquorShop.Classes
{
   public class PurchaseVm:ViewModelBase
    {
        public int PurchaseVmId { get; set; }
        public PurchaseVm()
        {
            Purchases = new ObservableCollection<Purchase>();
            Date = DateTime.Now;
        }

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


        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        decimal _Discount;
        public decimal Discount
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

        public decimal AmountPaid { get; set; }
        public decimal Due { get; set; }
        private void CalculateInVoiceTotal()
        {
            
        }

        public Supplier Supplier { get; set; }
        public decimal TaxableAmount { get; set; }
        decimal _VatAmount;
        public decimal VatAmount
        {
            get
            {
                return _VatAmount;
            }
            set
            {
                if (_VatAmount != value)
                {
                    _VatAmount = value;
                    notify("VatAmount");
                  
                }
            }
        }


       

        public int? InvoiceNumber { get; set; }
    }
}

using Rms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShop.Classes
{
   public class BeverageVm: ViewModelBase
    {
        public int BeverageId { get; set; }
        public int SaleOrderId { get; set; }
        public string Name { get; set; }
        public int BeverageCategoryId { get; set; }
        public BeverageCategory BeverageCategory { get; set; }
        public int? Volume { get; set; }
        public string Country { get; set; }
        decimal? _Price;
        public decimal? Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    notify("Price");
                    CalculateTotal();
                }
            }
        }

        public string Image { get; set; }
        decimal? _Total;
        public decimal? Total
        {
            get
            {
               
                return _Total;
            }
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                    notify("Total");
                }
            }
        }

        int _Quantity;
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    notify("Quantity");
                    CalculateTotal();
                }
            }
        }
        decimal _AmountPaid;
        public decimal AmoutPaid
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
                    notify("AmoutPaid");
                }
            }
        }

        private void CalculateTotal()
        {
            Total = Quantity * Price ?? 0;
        }
    }
}

using Rms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;

namespace LiquorShop.Classes
{
   public class SaleVm:ViewModelBase
    {
        public SaleVm()
        {
            SaleDate = DateTime.Now;
            BevarageVms = new ObservableCollection<BeverageVm>();
        }
        public int SaleOrderId { get; set; }
        ObservableCollection<BeverageVm> _BevarageVms;
        public ObservableCollection<BeverageVm> BevarageVms
        {
            get
            {
                return _BevarageVms;
            }
            set
            {
                if (_BevarageVms != value)
                {
                    _BevarageVms = value;
                    notify("BevarageVms");
                }
            }
        }



        private decimal _AmountAfterDiscount;
        public decimal AmountAfterDiscount
        {
            get { return _AmountAfterDiscount; }
            set
            {
                SetProperty<decimal>(ref _AmountAfterDiscount, value);
            }
        }

        private decimal _TotalAmount;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set
            {
                SetProperty<decimal>(ref _TotalAmount, value);
            }
        }



        private decimal _AmountPaid;
        public decimal AmountPaid
        {
            get { return _AmountPaid; }
            set
            {
                SetProperty<decimal>(ref _AmountPaid, value);
                CalculateDueAndChange();
            }
        }

        private void CalculateDueAndChange()
        {
            if(Discount>0)
            {
                Due =  AmountAfterDiscount-AmountPaid;
                if (Due <= 0)
                {
                    Due = 0;
                }
                Change = AmountPaid - AmountAfterDiscount;
                if (Change <= 0)
                {
                    Change = 0;
                }
            }
            else
            {
                Due = TotalAmount - AmountPaid;
                if (Due <= 0)
                {
                    Due = 0;
                }
                Change = AmountPaid - TotalAmount;
                if (Change <= 0)
                {
                    Change = 0;
                }
            }
           
        }

        private decimal _Discount;
        public decimal Discount
        {
            get { return _Discount; }
            set
            {
                SetProperty<decimal>(ref _Discount, value);
            }
        }
        public DateTime SaleDate { get; set; }


        private decimal _Due;
        public decimal Due
        {
            get { return _Due; }
            set
            {
                SetProperty<decimal>(ref _Due, value);
            }
        }


        private decimal _Change;
        public decimal Change
        {
            get { return _Change; }
            set
            {
                SetProperty<decimal>(ref _Change, value);
            }
        }
        public  Customer Customer { get; set; }
        public int? CustomerId { get; set; }
    }
}

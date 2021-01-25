using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquorShop.Classes
{
    public class PurchaseReportVm
    {
        public int PurchaseReportVmId { get; set; }
        DateTime _PurchaseDate;
        public DateTime PurchaseDate
        {
            get
            {
                return _PurchaseDate;
            }
            set
            {
                if (_PurchaseDate != value)
                {
                    _PurchaseDate = value;

                }
            }
        }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Metric { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        private decimal _Total;

        public decimal Total
        {
            get { return _Total; }
            set { _Total = Quantity * UnitPrice; }
        }
        public int PurchaseId { get; set; }
        public string Beverage { get; set; }
        public decimal Rate { get; set; }
        public decimal LineTotalAmout { get; set; }
        public int MetricQuantity { get; set; }
        private decimal _Discount;

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        private decimal _Vat;

        public decimal Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }


        private decimal _VatAmount;

        public decimal VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }

        public int SupplierId { get; set; }
        private int _InvoiceNumber;

        public int InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }

        public int InvoiceId { get; set; }
        public string Supplier { get; set; }
    }
}

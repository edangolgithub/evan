using Rms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
    public enum Metrics { Bottle,Case, Litre }


    public partial class Purchase : ModelBase
    {
        public int PurchaseId { get; set; }
        public int BeverageId { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        decimal _UnitPrice;
        public decimal UnitPrice
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                if (_UnitPrice != value)
                {
                    _UnitPrice = value;
                    notify("UnitPrice");
                }
            }
        }


        public decimal Rate { get; set; }
        decimal _LineTotalAmout;
        public decimal LineTotalAmount
        {
            get
            {
                return _LineTotalAmout;
            }
            set
            {
                if (_LineTotalAmout != value)
                {
                    _LineTotalAmout = value;
                    notify("LineTotalAmount");
                }
            }
        } 
        public int Quantity { get; set; }
        public int MetricQuantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? VatAmount { get; set; }       
       // public int SupplierId { get; set; }
       // public int? InvoiceNumber { get; set; }      
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public Metrics? Metric { get; set; }
        public virtual Beverage Beverage { get; set; }
      //  public virtual Supplier Supplier { get; set; }
    }
}
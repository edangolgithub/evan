using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
  public class Sale
    {
        public int SaleId { get; set; }
        public int? SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public int BeverageId { get; set; }
        public Beverage Beverage { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Due { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Profit { get; set; }
        public virtual Customer Customer { get; set;}
        public int? CustomerId { get; set; }      
        public bool DuePaying { get; set; }
        public virtual ICollection<DuePay> DuePays { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquorShop.Classes
{
   public class SalesReportVm
    {
        public int SalesReportVmId { get; set; }
        public int SaleId { get; set; }
        public int BeverageId { get; set; }
        public string Beverage { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Due { get; set; }
        public decimal Paid { get; set; }
        public decimal Profit { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public bool DuePaying { get; set; }
        public decimal TotalAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
                                           //  public virtual ICollection<DuePay> DuePays { get; set; }


    }
   
}

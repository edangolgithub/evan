using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
   public class SaleOrder
    {
        public int SaleOrderId { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }       
        public decimal Discount { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Due { get; set; }
        public virtual Customer Customer { get; set; }
        public int? CustomerId { get; set; }
      
        public decimal AmountAfterDiscount { get; set; }
    }
}

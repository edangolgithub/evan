using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
    public class DuePay
    {
        public int DuePayId { get; set; }
        public int SaleOrderId { get; set; }
        public Nullable<DateTime> Date { get; set; }  
        public decimal Amount { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
    }
}

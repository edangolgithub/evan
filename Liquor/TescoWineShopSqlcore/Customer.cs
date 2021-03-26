using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
   public class Customer
    {
        public int CustomerId { get; set; }
        public String CustomerName { get; set; }
        public String  CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<DuePay> Dues { get; set; }

    }
}

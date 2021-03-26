using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
  public  class SalesReturn
    {
        public int SalesReturnId { get; set; }
        public int SaleId { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Quantity { get; set; }
        public decimal ReturnAmount { get; set; }
        public int? BeverageId { get; set; }
        public virtual Beverage Beverage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierCity { get; set; }
        public string PanNumber { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}

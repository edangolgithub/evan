using Rms.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoWineShopSql
{
   public class Invoice : ModelBase
    {
        public int InvoiceId { get; set; }
        public Invoice()
        {
            Purchases = new ObservableCollection<Purchase>();
        }

       
        public ICollection<Purchase> Purchases
        {
            get;

            set;
           
        }


        public decimal Total { get; set; }

        public decimal TaxableAmount { get; set; }

        public decimal AmountPaid { get; set; }
        public decimal Due { get; set; }
        public DateTime Date { get; set; }

        public decimal Discount { get; set; }

        public Supplier Supplier { get; set; }
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public int SupplierId { get; set; }
        public decimal Vat { get; set; }

        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public int? InvoiceNumber { get; set; }
    }
}

using Rms.ViewModels;
using Rms.ViewModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Classes
{
   public class InvoiceVm
    {
        public int InvoiceId { get; set; }
        public List<OrderViewModel>  ItemsOrdered { get; set; }
        public decimal  TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
        public int TableNumber { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxableAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string User { get; set; }
    }
}

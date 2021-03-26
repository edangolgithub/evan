using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Classes
{
   public class TrialBalanceVm
    {
        public int TrialBalanceVmId { get; set; }
        public string LedgerAccountName { get; set; }
        public decimal CreditSide { get; set; }
        public decimal DebitSide { get; set; }
        public DateTime Date { get; set; }
        public bool IsParentAccount { get; set; }
    }
}

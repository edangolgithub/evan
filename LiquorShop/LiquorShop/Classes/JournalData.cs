using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Classes
{
    public class JournalData
    {
        public int coaid { get; set; }
        public string coa { get; set; }
        public decimal amount { get; set; }
        public int EntryId { get; set; }
        public int LedgerAccountId { get; set; }
    }
}

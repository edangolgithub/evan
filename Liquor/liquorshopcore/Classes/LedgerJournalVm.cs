using System;

namespace LiquorShop.Classes
{
    public class LedgerJournalVm
    {
        public int LedgergeneralId { get; set; }
        public string LedgerAccount { get; set; }
        public DateTime Date { get; set; }
        public int TransactionId { get; set; }
        public string OtherLedgerAccount { get; set; }
        public int PeriodYear { get; set; }
        public int Period { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string DebitString { get; set; }
        public string CreditString { get; set; }
        public decimal BalanceBroughtDown { get; set; }
        public string BalanceCarriedDown { get; set; }
        public string Particulars { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Balance { get; set; }
    }


    
   //public class LedgerReport
   // {
   //     Rms.Data.RmsContext context = new Data.RmsContext();
   //     MainViewModel vm = new MainViewModel();
   //     public void GetLedger()
   //     {
      

   //     }
    //}
}

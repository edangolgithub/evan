using System;
using System.Linq;

namespace Rms.ViewModels.Accounts
{
    public partial class FinanceViewModel
    {
      
        public void  InitJournalEntryListViewModel()
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
        }

     
        #region SearchJournalsCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SearchJournalsCommand" />
        /// </summary>
        private RelayCommand _SearchJournalsCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SearchJournalsCommand
        {
            get
            {
                if (_SearchJournalsCommand == null)
                { _SearchJournalsCommand = new RelayCommand(SearchJournalsCommand_Execute, SearchJournalsCommand_CanExecute); }

                return _SearchJournalsCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SearchJournalsCommand" />
        /// </summary>
        private void SearchJournalsCommand_Execute(object obj)
        {
            GetJournalEntries(FromDate, ToDate);
        }

        /// <summary>
        /// Determines if <see cref="SearchJournalsCommand" /> is allowed to execute
        /// </summary>
        private Boolean SearchJournalsCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion

        DateTime _FromDate;
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    notify("FromDate");
                }
            }
        }
        DateTime _ToDate;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    notify("ToDate");
                }
            }
        }
        private void GetJournalEntries(DateTime fromDate, DateTime toDate)
        {
            // var data = Ledgergenerals.GroupBy(a => a.LedgerTransactionId);
            // var data = Ledgergenerals.Where(a => a.JournalEntryDate.Date == FromDate.Date).ToList();
            var data =
                Ledgergenerals.Join(LedgerTransactions, b => b.LedgerTransactionId, a => a.LedgerTransactionId, (a, b) =>
                       new
                       {
                           LedgerTransactionId=a.LedgerTransactionId,
                           JournalEntryDate = a.JournalEntryDate,
                           AccountName=a.LedgerAccount.AccountName,
                           Particulars =a.Credit==0? b.Description:"",
                           Debit = a.Debit,
                           Credit = a.Credit
                       }
                ).OrderBy(a => a.JournalEntryDate).Where(a => a.JournalEntryDate.Date >= FromDate).OrderBy(a=>a.Debit)
                .Where(a => a.JournalEntryDate.Date <= ToDate).ToList();
             }
       
       
    }
}

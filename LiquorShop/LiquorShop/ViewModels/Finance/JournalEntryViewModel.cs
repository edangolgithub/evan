using Rms.Data;
using LiquorShopService.Services;
//using Rms.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Rms.ViewModels.Accounts
{
    public partial class FinanceViewModel :ViewModelBase
    {
        public void InitJournalEntryVm()
        {
            JournalDate = DateTime.Now; 
        }

        #region props

      
        IEnumerable<string> _JournalEntries;
        public IEnumerable<string> JournalEntries
        {
            get
            {
                return _JournalEntries;
            }
            set
            {
                if (_JournalEntries != value)
                {
                    _JournalEntries = value;
                    notify("JournalEntries");
                }
            }
        }

        private DateTime _JournalDate;

        public DateTime JournalDate
        {
           
            get { return _JournalDate; }
            set
            { 
                _JournalDate = value;
                NotifyPropertyChanged("JournalDate");
                SelectedDate = _JournalDate;
            }
        }




        #endregion

    
    }
}

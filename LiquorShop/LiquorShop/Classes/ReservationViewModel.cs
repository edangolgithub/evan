using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.ViewModels.Classes
{
    public class ReservationViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void notify(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        int _RId;
        public int RId
        {
            get
            {
                return _RId;
            }
            set
            {
                if (_RId != value)
                {
                    _RId = value;
                    notify("RId");
                }
            }
        }

        string _RName;
        public string RName
        {
            get
            {
                return _RName;
            }
            set
            {
                if (_RName != value)
                {
                    _RName = value;
                    notify("RName");
                }
            }
        }

        string _RAddress;
        public string RAddress
        {
            get
            {
                return _RAddress;
            }
            set
            {
                if (_RAddress != value)
                {
                    _RAddress = value;
                    notify("RAddress");
                }
            }
        }

        string _RPhone;
        public string RPhone
        {
            get
            {
                return _RPhone;
            }
            set
            {
                if (_RPhone != value)
                {
                    _RPhone = value;
                    notify("RPhone");
                }
            }
        }

        string _RTable;
        public string RTable
        {
            get
            {
                return _RTable;
            }
            set
            {
                if (_RTable != value)
                {
                    _RTable = value;
                    notify("Rtable");
                }
            }
        }

        DateTime? _RTime;
        public DateTime? RTime
        {
            get
            {
                return _RTime;
            }
            set
            {
                if (_RTime != value)
                {
                    _RTime = value;
                    notify("RTime");
                }
            }
        }
        DateTime? _RDate;
        public DateTime? RDate
        {
            get
            {
                return _RDate;
            }
            set
            {
                if (_RDate != value)
                {
                    _RDate = value;
                    notify("RDate");
                }
            }
        }


    }
}

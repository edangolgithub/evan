using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Classes
{
  public  class CustomerTableModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        #endregion
        public int CustomerTableId { get; set; }
        public int CustomerTableNumber { get; set; }
        public bool TableOccupied { get; set; }
        public DateTime TimeSeated { get; set; }
        public DateTime TimeCheckedOut { get; set; }
    }
}

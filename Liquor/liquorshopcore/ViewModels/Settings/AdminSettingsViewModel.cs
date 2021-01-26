using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Rms;

namespace LiquorShop.ViewModels.Settings
{
   public class AdminSettingsViewModel:ViewModelBase
    {

        string _TaxValue;
        public string TaxValue
        {
            get
            {
                return _TaxValue;
            }
            set
            {
                if (_TaxValue != value)
                {
                    _TaxValue = value;
                    notify("TaxValue");
                }
            }
        }

        string _ServiceCharge;
        public string ServiceCharge
        {
            get
            {
                return _ServiceCharge;
            }
            set
            {
                if (_ServiceCharge != value)
                {
                    _ServiceCharge = value;
                    notify("ServiceCharge");
                }
            }
        }
        string _ImageFolder;
        public string ImageFolder
        {
            get
            {
                return _ImageFolder;
            }
            set
            {
                if (_ImageFolder != value)
                {
                    _ImageFolder = value;
                    notify("ImageFolder");
                }
            }
        }

        string _DocumentFolder;
        public string DocumentFolder
        {
            get
            {
                return _DocumentFolder;
            }
            set
            {
                if (_DocumentFolder != value)
                {
                    _DocumentFolder = value;
                    notify("DocumentFolder");
                }
            }
        }
    }
}

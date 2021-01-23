using LiquorShop.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TescoWineShopSql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {

        Company _SelectedCompany;
        public Company SelectedCompany
        {
            get
            {
                return _SelectedCompany;
            }
            set
            {
                if (_SelectedCompany != value)
                {
                    _SelectedCompany = value;
                    notify("SelectedCompany");
                }
            }
        }

        public Dictionary<string, string> DrinkVolumes { get; set; }
        public static bool IsLicenseValid { get; set; }
        public SalesViewModel SalesViewModel { get; set; }
        User _CurrentUser;
        public User CurrentUser
        {
            get
            {
                return _CurrentUser;
            }
            set
            {
                if (_CurrentUser != value)
                {
                    _CurrentUser = value;
                    notify("CurrentUser");
                }
            }
        }
        String _BarCodeFolder;
        public String BarCodeFolder
        {
            get
            {
                return _BarCodeFolder;
            }
            set
            {
                if (_BarCodeFolder != value)
                {
                    _BarCodeFolder = value;
                    notify("BarCodeFolder");
                }
            }
        }
        bool _AutomateLedgerPost;
        public bool AutomateLedgerPost
        {
            get
            {
                return _AutomateLedgerPost;
            }
            set
            {
                if (_AutomateLedgerPost != value)
                {
                    _AutomateLedgerPost = value;
                    notify("AutomateLedgerPost");
                }
            }
        }
        bool _UseBarCodeService;
        public bool UseBarCodeService
        {
            get
            {
                return _UseBarCodeService;
            }
            set
            {
                if (_UseBarCodeService != value)
                {
                    _UseBarCodeService = value;
                    notify("UseBarCodeService");
                }
            }
        }

        private void ResolveWindowFontAndStyles()
        {
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
        }

        private static void ResolveGlobalization()
        {
            CultureInfo info = new CultureInfo("en-US");
            info.NumberFormat.CurrencySymbol = "Rs";
            info.DateTimeFormat = new DateTimeFormatInfo();
            info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            info.DateTimeFormat.AMDesignator = "AM";
            info.DateTimeFormat.PMDesignator = "PM";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;
        }

        private void ResolveDrinkTypes()
        {
            var v = LiquorShop.Resources.drinks.volume;
            var row = v.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            DrinkVolumes = new Dictionary<string, string>();
            foreach (var item in row)
            {
                var columns = item.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                DrinkVolumes.Add(columns[0].Trim(), columns[1].Trim());

            }
        }

      
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
    }
}

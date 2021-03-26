using System.ComponentModel;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {


        //private static Mutex mutex = null;




        protected override void OnStartup(StartupEventArgs e)
        {


            base.OnStartup(e);

            IsLicenseValid = true;
            Initialize();
        }
        private void Initialize()
        {
            CultureInfo info = new CultureInfo("ne-NP");
            info.NumberFormat.CurrencySymbol = "Rs";
            info.DateTimeFormat = new DateTimeFormatInfo();
            info.DateTimeFormat.Calendar = new GregorianCalendar(GregorianCalendarTypes.Localized);
            info.DateTimeFormat.AMDesignator = "AM";
            info.DateTimeFormat.PMDesignator = "PM";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;

            //d.Activate();
        }

        private void exception(object sender, FirstChanceExceptionEventArgs e)
        {
            var v = e.Exception;
        }
    }
}

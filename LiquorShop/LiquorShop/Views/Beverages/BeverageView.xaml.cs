using LiquorShop.ViewModels.Beverages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using TescoWine.Reports;
//using TescoWine.ViewModels;
using TescoWineShopSql;
//using TescoWineShopSql;
using Rms.InfraStructures;
using Unity;
namespace LiquorShop.Views.Beverages
{
    /// <summary>
    /// Interaction logic for BeverageView.xaml
    /// </summary>
    public partial class BeverageView : UserControl
    {
        BeverageViewModel vm;
        public BeverageView()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<BeverageViewModel>();
            DataContext = vm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //var dts = Enum.GetValues(typeof(Drinktype));
            //dcombo.ItemsSource = dts;
            //dcombo1.ItemsSource = dts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var v = dgrid.ItemsSource;
            //BeverageReport report = new BeverageReport(v,"b");
            //report.Show();
        }

        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (dgrid.SelectedItem != null)
            //{
            //    Beverage b = dgrid.SelectedItem as Beverage;
            //    ((BeverageViewModel)DataContext).GetTotalStock(b);
            //    ((BeverageViewModel)DataContext).GetTotalSaleBeverage(b);
            //    ((BeverageViewModel)DataContext).GetSalesByBeverage();
            //    ((BeverageViewModel)DataContext).GetPurchasesByBeverage();
            //}
        }

        private void newbtn_Click(object sender, RoutedEventArgs e)
        {
            bevtxt.Focus();
        }
    }
}

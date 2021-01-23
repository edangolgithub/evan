using LiquorShop.ViewModels.Purchases;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Rms.InfraStructures;
using Unity;
using TescoWineShopSql;

namespace LiquorShop.Views.Purchases
{ 
    /// <summary>
    /// Interaction logic for PurchaseDataWindow.xaml
    /// </summary>
public partial class PurchaseDataWindow : UserControl
    {
         PurchaseDataWindowViewModel vm;
        public PurchaseDataWindow()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<PurchaseDataWindowViewModel>();
            DataContext = vm;
        }

        private void Allbtn_Click(object sender, RoutedEventArgs e)
        {
           // Purchasesgrid.ItemsSource = bl.context.Purchases.ToList();
        }

        private void Purchasesbtn_Click(object sender, RoutedEventArgs e)
        {
            //PreQuery();
            //if(b==null)
            //{
            //    MessageBox.Show("Select Beverage");
            //    return;
            //}
            //Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(b);
        }

        public void PreQuery()
        {
            ds = StartDate.SelectedDate ?? DateTime.Today;
            de = EndDate.SelectedDate ?? DateTime.Today;
            if (bevcombo.SelectedItem != null)
            {
                b = new Beverage();
                b = bevcombo.SelectedItem as Beverage;
            }
           
        }

        public DateTime ds { get; set; }
        public DateTime de { get; set; }
        public Beverage b { get; set; }

        private void Purchasesbydatebtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();            
          //  Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(ds, de, b);
           // totalincomelabel.Content = bl.TotalIncome;
        }

        private void Purchasestodaybtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();
          //  Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(ds, de, b);
          //  totalincomelabel.Content = bl.TotalIncome;
        }

       

        private void Purchasesyesteraybtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();
           // Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(ds.AddDays(-1), de.AddDays(-1), b);
           // totalincomelabel.Content = bl.TotalIncome;
        }

        private void Purchasesthismonthbtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();
            DateTime dtoday = DateTime.Today;
            var date = dtoday.Date.Day;
           // Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(ds.AddDays(-(date)), de, b);
           // totalincomelabel.Content = bl.TotalIncome;
        }

        private void Purchaseslastmonthbtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();
           // var today = DateTime.Today;
           // var month = new DateTime(today.Year, today.Month, 1);
           // var first = month.AddMonths(-1);
           // var last = month.AddDays(-1);
           // Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(first, last, b);
            //totalincomelabel.Content = bl.TotalIncome;
        }

        private void Purchasesthisyearbtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();
            //int year = DateTime.Now.Year;
            //DateTime first = new DateTime(year, 1, 1);
            //DateTime last = new DateTime(year, 12, 31);
            //Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(first, last, b);
            //totalincomelabel.Content = bl.TotalIncome;
            //totalincomelabel.Content = bl.TotalIncome;
        }

        private void Purchaseslastyearbtn_Click(object sender, RoutedEventArgs e)
        {
            PreQuery();
            //int year = DateTime.Now.Year;
            //year = year - 1;
            //DateTime first = new DateTime(year, 1, 1);
            //DateTime last = new DateTime(year, 12, 31);
            //Purchasesgrid.ItemsSource = bl.GetAllPurchasesForBeverage(first, last, b);
            //totalincomelabel.Content = bl.TotalIncome;
        }

        private void resetbtn_Click(object sender, RoutedEventArgs e)
        {
            //bevcombo.SelectedItem = null;
            //b = null;
            //Purchasesgrid.ItemsSource = null;
            //totalincomelabel.Content = bl.TotalIncome;
        }
        
    }
}

using LiquorShop.ViewModels.Beverages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using MahApps.Metro.Controls;
using Rms.InfraStructures;
using Unity;
using LiquorShop.ViewModels.Purchases;
using WpfApp1;

namespace LiquorShop.Views.Purchases
{
    /// <summary>
    /// Interaction logic for PurchaseView.xaml
    /// </summary>
    public partial class PurchaseListView : UserControl
    {
       
        private PurchaseListViewModel vm;

        public PurchaseListView()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<PurchaseListViewModel>();
            DataContext = vm;
           
        }

        public PurchaseListView(PurchaseListViewModel purchaseViewModel)
        {
            InitializeComponent();
            this.vm = purchaseViewModel;
            DataContext = vm;
           
        }

       

      
        private async void Invoicefindbtn_ClickAsync(object sender, RoutedEventArgs e)
        {
           
            if(Invoicetxt.Text.Length<1)
            {
                return;
            }
            try
            {
                int b = int.Parse(Invoicetxt.Text);
                
                ((PurchaseListViewModel)DataContext).Purchases = new System.Collections.ObjectModel.ObservableCollection<Purchase>
                    (await 
                    Task.Run(() => ((PurchaseListViewModel)DataContext).Service.FindPurchasesByInvoiceAsync(b))
                    ); 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

      
        private void changevatbtn_Click(object sender, RoutedEventArgs e)
        {
            //AdminWindow aw = new AdminWindow((BeverageViewModel)DataContext);
            //aw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            p.IsOpen = false;
        }

        private void JournalListGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            p.IsOpen = false;
        }



        //private void vattxt_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    if (quantitytxt.Text.Length < 1 || unitpricetxt.Text.Length < 1)
        //    {
        //        return;
        //    }
        //    try
        //    {
        //        var v = decimal.Parse(quantitytxt.Text);
        //        var x = decimal.Parse(unitpricetxt.Text);
        //        decimal y = 0;
        //        if (discounttxt.Text.Length > 1)
        //        {
        //            y = decimal.Parse(discounttxt.Text);
        //        }
        //        var totalvat = ((v * x) - y) * (((PurchaseListViewModel)DataContext).VatPercent/100);
        //        vattxt.ToolTip ="vat for this purchase : "+ totalvat;
        //        var z = vattxt.ToolTip;
        //        codePopup = new Popup();
        //        StackPanel sp = new StackPanel();
        //        Border b = new Border();
        //        b.BorderBrush = Brushes.Fuchsia;
        //        b.BorderThickness = new Thickness(3);
        //        b.CornerRadius = new CornerRadius(2);

        //        TextBlock popupText = new TextBlock();
        //        popupText.FontSize = 15;
        //        popupText.Text = z.ToString();
        //        sp.Background = Brushes.FloralWhite;
        //        sp.Height = 100;
        //        sp.Width = 100;
        //        popupText.Foreground = Brushes.DarkKhaki;
        //        codePopup.Child = popupText;
        //        codePopup.PlacementTarget = vattxt;
        //        codePopup.Placement=PlacementMode.Right;
        //        codePopup.IsOpen = true;

        //        sp.Children.Add(codePopup);
        //        b.Child = sp;

        //    }
        //    catch (Exception)
        //    {
        //        vattxt.ToolTip = "Cannot Calculate Vat";
        //    }
        //}





    }
    public class PurchaseReportVm
    {
        public DateTime? sdate { get; set; }
        public DateTime? edate { get; set; }
        public IEnumerable<Purchase> v { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? Total { get; set; }
        public Beverage SBeversge { get; set; }
        public bool BeverageNull { get; set; }
    }
}

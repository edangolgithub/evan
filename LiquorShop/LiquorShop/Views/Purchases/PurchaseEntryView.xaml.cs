using LiquorShop.ViewModels.Purchases;
using Rms.InfraStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Unity;

namespace LiquorShop.Views.Purchases
{
    /// <summary>
    /// Interaction logic for PurchaseEntry.xaml
    /// </summary>
    public partial class PurchaseEntryView : UserControl
    {
        PurchaseEntryViewModel vm; 
        public PurchaseEntryView()
        {
            
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<PurchaseEntryViewModel>();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PurchaseView view = new PurchaseView(this.DataContext as PurchaseViewModel);
            //view.Show();
            //this.Close();
        }

        private void quantitytxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((PurchaseEntryViewModel)DataContext).CalculateActualUnitQuantity();
        }

        private void ratetextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (ratetextbox.Text.Length < 1)
            //{
            //    ratetextbox.Text = "0";

            //}
           // ((PurchaseViewModel)DataContext).CalculateLineAmount();
        }

        private void SelectTextBoxText(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void DecimalTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }
        private void AllowDecimal(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9) &&
                e.Key != System.Windows.Input.Key.Back && e.Key != System.Windows.Input.Key.Decimal
                && e.Key != System.Windows.Input.Key.OemPeriod
                ))
            {
                e.Handled = true;
                return;
            }
            // checks to make sure only 1 decimal is allowed
            if (e.Key == System.Windows.Input.Key.Decimal || e.Key == System.Windows.Input.Key.OemPeriod)
            {
                if ((sender as TextBox).Text.Contains("."))
                    e.Handled = true;
            }
        }

       // public decimal PurchaseSubTotalreal { get; set; }

        private void calculateinvoicetotaldiscount(object sender, TextChangedEventArgs e)
        {
        
            //var amt = (sender as TextBox).Text;
            //if(amt.Length<1)
            //{
            //    return;
            //}
            //decimal amnt;
            //((PurchaseViewModel)DataContext).PurchaseSubTotal = ((PurchaseViewModel)DataContext).SelectedPurchaseVm.Purchases.Sum(a => a.LineTotalAmount);
            //if (decimal.TryParse(amt, out amnt))
            //{
               
            //    ((PurchaseViewModel)DataContext).PurchaseSubTotal -= amnt;
            //}
        }
        private void calculateinvoicetotalvat(object sender, TextChangedEventArgs e)
        {
            //var amt = (sender as TextBox).Text;
            //if (amt.Length < 1)
            //{
            //    return;
            //}
            //decimal amnt;
            //((PurchaseViewModel)DataContext).PurchaseSubTotal = ((PurchaseViewModel)DataContext).SelectedPurchaseVm.Purchases.Sum(a => a.LineTotalAmount);

            //if (decimal.TryParse(amt, out amnt))
            //{
            //    ((PurchaseViewModel)DataContext).PurchaseSubTotal += amnt;
            //}
        }

        private void scombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((PurchaseEntryViewModel)DataContext).IsDirty = true;
        }
    }
}

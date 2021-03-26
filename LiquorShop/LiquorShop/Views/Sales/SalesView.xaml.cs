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
using LiquorShop.ViewModels.Sales;
using MahApps.Metro.Controls;
using WpfApp1;
using Rms.InfraStructures;
using Unity;
using System.Globalization;

namespace LiquorShop.Views.Sales
{
    /// <summary>
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        SalesViewModel vm;
        public SalesView()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<SalesViewModel>();
            ((App)Application.Current).SalesViewModel=vm;
            DataContext = ((App)Application.Current).SalesViewModel;
            InitPayment();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
               
                StackPanel s = sender as StackPanel;
                if (s == null)
                {
                    return;
                }
                int itemid = 0;
                foreach (var i in s.Children)
                {
                    if (i is Label)
                    {
                        Label l = (Label)i;
                        if (!
                        int.TryParse(l.Content.ToString(), out itemid))
                            throw new Exception("Item Id is not valid");
                        break;
                    }
                }
           ((SalesViewModel)DataContext).UpdateSelectedBeverages(itemid);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void textboxfocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            ((SalesViewModel)DataContext).host.Close();
        }

        public bool flag { get; set; }
        public bool decimalflag { get; set; }
        private void InitButtonValue(Button b)
        {

            //Binding bt = new Binding { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Path = new PropertyPath("AmountPaid"), };
            //tb1.SetBinding(TextBox.TextProperty, bt);
            if (b.Content.ToString() == "C")
            {
                tb1.Clear();
                return;
            }
            decimal orig;
            if (decimal.TryParse(b.Content.ToString(), out orig) || b.Content.ToString() == ".")
            {
                if (b.Content.ToString() == "50")
                {
                    var oldmoney = decimal.Parse(tb1.Text, NumberStyles.Currency);
                    orig = oldmoney + 50;
                    tb1.Text = orig.ToString();
                    flag = true;
                    return;
                }
                if (b.Content.ToString() == "100")
                {
                    var oldmoney = decimal.Parse(tb1.Text, NumberStyles.Currency);
                    orig = oldmoney + 100;
                    tb1.Text = orig.ToString();
                    flag = true;
                    return;
                }
                if (b.Content.ToString() == "500")
                {
                    var oldmoney = decimal.Parse(tb1.Text, NumberStyles.Currency);
                    orig = oldmoney + 500;
                    tb1.Text = orig.ToString();
                    flag = true;
                    return;
                }
                if (b.Content.ToString() == "1000")
                {
                    var oldmoney = decimal.Parse(tb1.Text, NumberStyles.Currency);
                    orig = oldmoney + 1000;
                    tb1.Text = orig.ToString();
                    flag = true;
                    return;
                }
                if (b.Tag.ToString() != "sp" || b.Content.ToString() == ".")
                {
                    string totalmoneystring = "";
                    //string prev = tb1.Text;
                    if (flag == true)
                    {
                        tb1.Clear();
                        flag = false;
                    }

                    decimal oldmoney = 0;
                    decimal.TryParse(tb1.Text, NumberStyles.Currency, System.Threading.Thread.CurrentThread.CurrentCulture, out oldmoney);
                    if (oldmoney != 0)
                    {
                        if (b.Content.ToString() == ".")
                        {
                            //if (decimalflag)
                            //{
                            //    return;
                            //}

                            BindingOperations.ClearBinding(tb1, TextBox.TextProperty);
                            tb1.Text = string.Concat(oldmoney.ToString(), ".");
                            decimalflag = true;
                            //Binding bt1 = new Binding { UpdateSourceTrigger = UpdateSourceTrigger.LostFocus, Path = new PropertyPath("AmountPaid"), };
                            //tb1.SetBinding(TextBox.TextProperty, bt1);
                            decimalstring = tb1.Text;
                            return;
                        }
                        else if (b.Content.ToString() == "0")
                        {
                            if (decimalflag)
                            {
                                tb1.Text = decimalstring + "0";
                            }
                            else
                            {
                                tb1.Text = oldmoney.ToString() + "0";
                            }
                            decimalflag = false;
                            return;
                        }
                        else
                        {
                            if (decimalflag)
                            {
                                tb1.Text = decimalstring + b.Content.ToString();
                                decimalflag = false;
                                return;
                            }
                            else
                            {
                                var newmoney = decimal.Parse(b.Content.ToString(), NumberStyles.Currency, NumberFormatInfo.CurrentInfo);
                                totalmoneystring = oldmoney.ToString() + newmoney.ToString();
                            }

                        }
                        tb1.Text = totalmoneystring;
                        decimalflag = false;
                        flag = false;
                        return;
                    }
                    else
                    {
                        tb1.Text = b.Content.ToString();
                    }

                }
            }
            if (b.Content.ToString() == "All")
            {
                return;
            }
            if (b.Content.ToString() == "PayBill")
            {
                return;
            }
            if (b.Content.ToString() == "Free")
            {
                return;
            }
        }
        public string decimalstring { get; set; }
        private void B_Click(object sender, RoutedEventArgs e)
        {
            Binding bt = new Binding { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Path = new PropertyPath("AmountPaid"), };
            tb1.SetBinding(TextBox.TextProperty, bt);
            InitButtonValue((Button)sender);
            //Binding bt = new Binding { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            //    Converter=new Converters.DecimalConverter(),
            //    StringFormat = "C",ConverterCulture= CultureInfo.CurrentCulture, Path = new PropertyPath("AmountPaid"), };

        }
        private void tb1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (tb1.Text.Length < 1)
            {
                tb1.Text = "0";
            }
        }

        private void InitPayment()
        {
            foreach (var item in buttonwrap.Children)
            {
                if (item is Button)
                {
                    Button b = (Button)item;
                    b.Click += B_Click;
                }
            }
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.Foreground = new SolidColorBrush(Colors.Wheat);
        }
    }
}

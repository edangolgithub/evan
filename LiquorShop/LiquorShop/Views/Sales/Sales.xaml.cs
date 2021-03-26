using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using MahApps.Metro.Controls;
using System.Windows.Media;
using Rms.ViewModels.Restaurant;
using Rms.InfraStructures;
using Unity;
using System.Threading.Tasks;
using System.Windows.Data;
using Rms.Converters;
using EvanDangol.IO;
using System.IO;
using System.Collections.Generic;

namespace Rms.Views.Restaurant
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : MetroWindow, INotifyPropertyChanged
    {
        int _TableNumber;
        public int TableNumber
        {
            get
            {
                return _TableNumber;
            }
            set
            {
                if (_TableNumber != value)
                {
                    _TableNumber = value;
                    notify("TableNumber");
                }
            }
        }
        public Sales()
        {
           
            InitializeComponent();
            Task.Run(()=> Init());
            DataContext = vm;
            IsDirty = false;
        }

       private void Init()
        {
            vm = ContainerHelper.Container.Resolve<SalesViewModel>();
            vm.GetSalesObject(SelectedRestaurantTable);
        }

        SalesViewModel vm;
        public Sales(int tablenumber) : this()
        {
            this.TableNumber = tablenumber;
        }
        RestaurantTable _SelectedRestaurantTable;
        public RestaurantTable SelectedRestaurantTable
        {
            get
            {
                return _SelectedRestaurantTable;
            }
            set
            {
                if (_SelectedRestaurantTable != value)
                {
                    _SelectedRestaurantTable = value;
                    notify("SelectedRestaurantTable");
                }
            }
        }

        public Sales(RestaurantTable selectedRestaurantTable) : this()
        {
           // FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;           
            InitializeComponent();
            this.SelectedRestaurantTable = selectedRestaurantTable;          
            Task.Run(() => Init()).Wait();
            DataContext = vm;
            IsDirty = false;
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
        private void B_Click(object sender, RoutedEventArgs e)
        {
            Binding bt = new Binding { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Path = new PropertyPath("AmountPaid"), };
            tb1.SetBinding(TextBox.TextProperty, bt);
            InitButtonValue((Button)sender);
            //Binding bt = new Binding { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            //    Converter=new Converters.DecimalConverter(),
            //    StringFormat = "C",ConverterCulture= CultureInfo.CurrentCulture, Path = new PropertyPath("AmountPaid"), };
           
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
                    string totalmoneystring="";
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
                            if(decimalflag)
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
                            if(decimalflag)
                            {
                                tb1.Text =decimalstring + b.Content.ToString();
                                decimalflag = false;
                                return;
                            }
                            else
                            {
                                var newmoney = decimal.Parse(b.Content.ToString(), NumberStyles.Currency,NumberFormatInfo.CurrentInfo);
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void lstitems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b.CommandParameter == null)
                return;
            int itemid = (int)b.CommandParameter;
            //string tag = b.Tag.ToString();
            ((SalesViewModel)DataContext).SalesAdd(itemid);
            IsDirty = true;
        }
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b.CommandParameter == null)
                return;
            int itemid = (int)b.CommandParameter;
            //string tag = b.Tag.ToString();
            ((SalesViewModel)DataContext).SalesRemove(itemid);
            IsDirty = true;
        }
        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            ((SalesViewModel)DataContext).SaveOrder();
            IsDirty = false;
        }
        private ObservableCollection<string> _Tables;
        public ObservableCollection<string> Tables
        {
            get { return _Tables; }
            set
            {
                _Tables = value;
                notify("Tables");
            }
        }

        private void DiscountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }
        private void PayBillBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsDirty)
            {
                salestab.Focus();
            }
            else
            {
            }
            return;
        }
        private void paymenttab_Loaded(object sender, RoutedEventArgs e)
        {
            InitPayment();
            //  ((SalesViewModel)DataContext).CalculateBilli();
            // tb1.Text = ((RestaurantViewModel)DataContext).TotalAmount.ToString("C");
            tb1.Dispatcher.BeginInvoke(
                   new Action(delegate
                   {
                       tb1.Focus();
                   }), System.Windows.Threading.DispatcherPriority.Input);
        }
        private void tb1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
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
        private void tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // CalculateChange();
        }

        bool _IsDirty;
        public bool IsDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                if (_IsDirty != value)
                {
                    _IsDirty = value;
                    ((SalesViewModel)DataContext).IsDirty = _IsDirty;
                    notify("IsDirty");
                }
            }
        }
        public void ResolveAuthority()
        {
            if (((App)Application.Current).CurrentUser != null)
            {



                if (((App)Application.Current).CurrentUser.UserRole == null)
                {
                    throw new Exception("Log in error");
                }
                var role = ((App)Application.Current).CurrentUser.UserRole.UserInRole;
                switch (role)
                {
                    case Data.UserRoles.KitchenSupport:
                        throw new Exception("you have no authority");
                    case Data.UserRoles.Guest:
                        throw new Exception("you have no authority");

                    default:
                        throw new Exception("you have no authority");

                    case Data.UserRoles.Accountant:

                        throw new Exception("you have no authority");

                    case Data.UserRoles.Staff:
                        break;
                    case Data.UserRoles.Administrator:
                        break;
                    case Data.UserRoles.Manager:
                        break;
                    case Data.UserRoles.HeadChef:
                        break;
                    case Data.UserRoles.SousChef:
                        break;
                    case Data.UserRoles.Cashier:
                        break;
                    case Data.UserRoles.Waiter:
                        break;


                }
            }
        }
        private void lstitems_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ResolveAuthority();
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
            ((SalesViewModel)DataContext).PlaceOrderItem(itemid);
                IsDirty = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void paymentbutton_Click(object sender, RoutedEventArgs e)
        {
            if (IsDirty)
            {
                salestab.Focus();
                MessageBox.Show("Please Save order ");
            }
            else
            {
                paymenttab.Focus();
            }
        }

        private void DiscountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DiscountTextBox.Text.Length < 1)
            {
                DiscountTextBox.Text = "0";
            }
        }

        private void tb1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (tb1.Text.Length < 1)
            {
                tb1.Text = "0";
            }
        }

        private void tb1_GotFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)e.OriginalSource;
            tb.Dispatcher.BeginInvoke(
                new Action(delegate
                {
                    tb.SelectAll();
                }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void lstitems_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void hitrybtn_MouseEnter(object sender, MouseEventArgs e)
        {
            
          
        }

        private void hitrybtn_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var m = EvanDirectory.MakeDirectoryInSpecialPlaceWithPermission("Rms", Environment.SpecialFolder.LocalApplicationData);
            var old = "T" + ((SalesViewModel)DataContext).SelectedRestaurantTable.TableNumber + ".json";
            string oldfile = Path.Combine(m, old);

            string readend = "";
            if (File.Exists(oldfile))
            {
                readend = File.ReadAllText(oldfile);
                List<string> olddata = new List<string>();
                List<string> olddata1 = new List<string>();
                if (readend.Length > 0)
                {
                    olddata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(readend);

                }
                olddata.RemoveAt(0);
                foreach (var item in olddata)
                {
                    olddata1.Add("Table " + item);
                }
                 ((SalesViewModel)DataContext).TableHistory1 = olddata1;
            }
        }
    }
}

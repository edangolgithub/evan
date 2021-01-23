using Rms.Classes;
using Rms.Data;
using Rms.InfraStructures;
using Rms.ViewModels.Accounts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Unity;
using System.Threading.Tasks;
//using Rms.Utility;
using System;
using MahApps.Metro.Controls;
using TescoWineShopSql;
using WpfApp1;
using MahApps.Metro.IconPacks;
using System.Windows.Media;

namespace Rms.UserControls.Accounts
{
    /// <summary>
    /// Interaction logic for JournalEntryControl.xaml
    /// </summary>
    public partial class JournalEntryControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public JournalEntryControl()
        {
            InitializeComponent();
            d = new ObservableCollection<JournalData>();
            c = new ObservableCollection<JournalData>();

           
        }
        public ObservableCollection<JournalData> d { get; set; }
        public ObservableCollection<JournalData> c { get; set; }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddDebitPanel();
        }
        public StackPanel s1;
        public StackPanel s2;
        private void AddDebitPanel()
        {
            s1 = new StackPanel();
            s1.Orientation = Orientation.Horizontal;
            //s.Margin = new Thickness(5, 0, 0, 0);
            TextBox t = new TextBox();
            ComboBox c = new ComboBox();
            Button b1 = new Button();
            b1.Style = (Style)Application.Current.FindResource("PlusButtom");            
            var v= new PackIconModern() { Kind = PackIconModernKind.Minus, Height = 15, Width = 15, FontWeight = FontWeights.ExtraBold, Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#AEE71B"))};
            b1.Content = v;
            b1.Click += B1_Click;           
            c.ItemsSource = ((FinanceViewModel)DataContext).LedgerAccounts;
            c.DisplayMemberPath = "AccountName";
            c.SelectedValuePath = "LedgerAccountId";
            c.IsEditable = true;
            c.IsTextSearchEnabled = true;
            c.Text = "Select Account";
            
            t.Width = 200;
            c.Width = 300;
            TextBoxHelper.SetWatermark(t, "Please Enter Debit Amount");
            t.MinHeight = 35;
            c.Height = 35;
            t.KeyDown += textBox21_KeyDown;
            t.Margin = new Thickness(5);
            c.Margin = new Thickness(5);
            b1.Margin = new Thickness(5);
            s1.Children.Add(c);
            s1.Children.Add(t);
            s1.Children.Add(b1);
            xt1.Children.Add(s1);
        }
        private void B1_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parent = ((Button)sender as Button).Parent as StackPanel;
            xt1.Children.Remove(parent);
        }
        private void addcrdit_Click(object sender, RoutedEventArgs e)
        {
            AddCreditPanel();
        }
        private void AddCreditPanel()
        {
            StackPanel s2 = new StackPanel();
            s2.Orientation = Orientation.Horizontal;
            //s.Margin = new Thickness(5, 0, 0, 0);
            TextBox t = new TextBox();
            ComboBox c = new ComboBox();
            Button b2 = new Button();
            var v = new PackIconModern() { Kind = PackIconModernKind.Minus, Height = 15, Width = 15, FontWeight = FontWeights.ExtraBold, Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#AEE71B")) };
            b2.Content = v;
            b2.Style = (Style)Application.Current.FindResource("PlusButtom");
            b2.Margin = new Thickness(5);
            b2.Click += B2_Click;
            c.ItemsSource = ((FinanceViewModel)DataContext).LedgerAccounts;
            c.DisplayMemberPath = "AccountName";
            c.SelectedValuePath = "LedgerAccountId";
            c.Text = "Select Account";
            c.IsEditable = true;
            c.IsTextSearchEnabled = true;
            c.Height = 35;
            t.MinHeight = 35;
            TextBoxHelper.SetWatermark(t, "Please Enter Credit Amount");           
            
            t.Width = 200;
            c.Width = 300;
            t.KeyDown += textBox21_KeyDown;
            t.Margin = new Thickness(5);
            c.Margin = new Thickness(5);
            s2.Children.Add(c);
            s2.Children.Add(t);
            s2.Children.Add(b2);
            cdt1.Children.Add(s2);
        }
        private void B2_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parent = ((Button)sender as Button).Parent as StackPanel;
            cdt1.Children.Remove(parent);
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            if(dpicker.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Not valid Date");
                return;
            }
            SaveJournalEntry();
            //  ResetControls();
        }
        private void ResetControls()
        {
            xt1.Children.Clear();
            cdt1.Children.Clear();
            AddStackPanels();
            //  ((FinanceViewModel)DataContext).JournalDate = DateTime.Now;
            textBox1.Clear();
            jdsd = null;
            jdsc = null;
        }
        private void AddStackPanels()
        {
            s1 = new StackPanel();
            s1.Orientation = Orientation.Horizontal;
            //s.Margin = new Thickness(5, 0, 0, 0);
            TextBox t = new TextBox();
            ComboBox c = new ComboBox();
            Button b1 = new Button();
            b1.Click += add_Click;
            b1.Style = (Style)Application.Current.FindResource("PlusButtom");
            var v = new PackIconModern() { Kind = PackIconModernKind.Add, Height = 15, Width = 15, FontWeight = FontWeights.ExtraBold, Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#AEE71B")) };
            b1.Content = v;

            c.ItemsSource = ((FinanceViewModel)DataContext).LedgerAccounts;
            c.DisplayMemberPath = "AccountName";
            c.SelectedValuePath = "LedgerAccountId";
            c.Text = "Select Account";
            c.IsEditable = true;
            c.IsTextSearchEnabled = true;
          
            t.Width = 200;
            c.Width = 300;
            c.Height = 35;
            t.MinHeight = 35;
            t.KeyDown += textBox21_KeyDown;
            TextBoxHelper.SetWatermark(t, "Please Enter Debit Amount");
            t.Margin = new Thickness(5);
            c.Margin = new Thickness(5);
            b1.Margin = new Thickness(5);
            s1.Children.Add(c);
            s1.Children.Add(t);
            s1.Children.Add(b1);
            xt1.Children.Add(s1);
            StackPanel s2 = new StackPanel();
            s2.Orientation = Orientation.Horizontal;
            //s.Margin = new Thickness(5, 0, 0, 0);
            TextBox t1 = new TextBox();
            ComboBox c1 = new ComboBox();
            Button b2 = new Button();
            b1.Margin = new Thickness(5);
            b2.Click += addcrdit_Click;
            c1.ItemsSource = ((FinanceViewModel)DataContext).LedgerAccounts;
            c1.DisplayMemberPath = "AccountName";
            c1.SelectedValuePath = "LedgerAccountId";
            c1.Text = "Select Account";
            c1.IsEditable = true;
            c1.IsTextSearchEnabled = true;
            c1.Height = 35;
            var v1 = new PackIconModern() { Kind = PackIconModernKind.Add, Height = 15, Width = 15, FontWeight = FontWeights.ExtraBold, Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#AEE71B")) };
            b2.Content = v1;
            b2.Style = (Style)Application.Current.FindResource("PlusButtom");
            t1.Width = 200;
            c1.Width = 300;
            t1.MinHeight = 35;
            t1.KeyDown += textBox21_KeyDown;
            TextBoxHelper.SetWatermark(t1, "Please Enter Credit Amount");
            t1.Margin = new Thickness(5);
            c1.Margin = new Thickness(5);
            s2.Children.Add(c1);
            s2.Children.Add(t1);
            s2.Children.Add(b2);
            cdt1.Children.Add(s2);
        }
       
        private void SaveJournalEntry()
        {
            try
            {
                if(App.IsLicenseValid==false)
                {
                    MessageBox.Show("Your License Has Expired");
                    return;
                }
              

               
                if (((FinanceViewModel)DataContext).FakeUser ==null)
                {
                    throw new Exception("Please Login");
                }
                 ((App)Application.Current).CurrentUser = ((FinanceViewModel)DataContext).FakeUser;
                //if(FakeUser.UserRole.UserInRole!=UserRoles.Accountant && FakeUser.UserRole.UserInRole != UserRoles.Administrator)
                //{
                //    throw new Exception("You have no authority to enter journals");
                //}
                int TransactionId = 0;
                try
                {
                    var v = ((FinanceViewModel)DataContext).LedgerTransactions.DefaultIfEmpty().Max(a => a.LedgerTransactionId);
                    TransactionId = v + 1;
                }
                catch
                {
                    TransactionId = 1;
                }
                jdsd = new List<JournalData>();
                jdsc = new List<JournalData>();
                foreach (var item in xt1.Children)
                {
                    JournalData jd = new JournalData();
                    if (item is StackPanel)
                    {
                        StackPanel s = item as StackPanel;
                        foreach (var cnt in s.Children)
                        {
                            if (cnt is TextBox)
                            {
                                TextBox t1 = cnt as TextBox;
                                if (!string.IsNullOrEmpty(t1.Text))
                                {
                                    decimal d = 0;
                                    if (decimal.TryParse(t1.Text, out d))
                                    {
                                        jd.amount = d;
                                        jd.EntryId = TransactionId;
                                    }
                                }
                            }
                            else if (cnt is ComboBox)
                            {
                                ComboBox c1 = cnt as ComboBox;
                                if (c1.SelectedIndex > -1)
                                {
                                    var la = c1.SelectedItem as LedgerAccount;
                                   // var logic =.Where(a => a.parentLedgerAccount.LedgerAccountId == la.LedgerAccountId).FirstOrDefault();
                                    foreach (var account in ((FinanceViewModel)DataContext).LedgerAccounts)
                                    {
                                        if (account.parentLedgerAccount != null)
                                        {
                                            if (la.LedgerAccountId == account.parentLedgerAccount.LedgerAccountId)
                                            {
                                                throw new Exception(la.AccountName + " Cannot be Selected");
                                            }
                                        }
                                    }
                                    jd.LedgerAccountId = la.LedgerAccountId;
                                }
                            }
                        }
                        if (jd.LedgerAccountId > 0 && jd.amount > 0)
                        {
                            jdsd.Add(jd);
                        }
                    }
                }
                foreach (var item in cdt1.Children)
                {
                    JournalData jd = new JournalData();
                    if (item is StackPanel)
                    {
                        StackPanel s = item as StackPanel;
                        foreach (var cnt in s.Children)
                        {
                            if (cnt is TextBox)
                            {
                                TextBox t1 = cnt as TextBox;
                                if (!string.IsNullOrEmpty(t1.Text))
                                {
                                    decimal d = 0;
                                    if (decimal.TryParse(t1.Text, out d))
                                    {
                                        jd.amount = d;
                                        jd.EntryId = TransactionId;
                                    }
                                }
                            }
                            else if (cnt is ComboBox)
                            {
                                ComboBox c1 = cnt as ComboBox;
                                if (c1.SelectedIndex > -1)
                                {
                                    var la = c1.SelectedItem as LedgerAccount;
                                    // var logic =.Where(a => a.parentLedgerAccount.LedgerAccountId == la.LedgerAccountId).FirstOrDefault();
                                    foreach (var account in ((FinanceViewModel)DataContext).LedgerAccounts)
                                    {
                                        if (account.parentLedgerAccount != null)
                                        {
                                            if (la.LedgerAccountId == account.parentLedgerAccount.LedgerAccountId)
                                            {
                                                throw new Exception(la.AccountName + " Cannot be Selected");
                                            }
                                        }
                                    }
                                    jd.LedgerAccountId = (c1.SelectedItem as LedgerAccount).LedgerAccountId;
                                }
                            }
                        }
                        if (jd.LedgerAccountId > 0 && jd.amount > 0)
                        {
                            jdsc.Add(jd);
                        }
                    }
                }
                if (!IsJournalEntryValid())
                {
                    return;
                }
                if (jdsd.Count < 1 || jdsc.Count < 1)
                {
                    return;
                }
                else
                {
                    var totaldebits = jdsd.Sum(a => a.amount);
                    var totalcredits = jdsc.Sum(a => a.amount);
                    if (totaldebits != totalcredits)
                    {
                        throw new Exception("Debit and Credit Do Not Balance \nOr You May have Some Error in Assigning Accounts");
                    }
                }
                //if (((App)Application.Current).V)
                //{
                //    MessageBox.Show("trial version");
                //    return;
                //}
                List<string> debitaccounts = new List<string>();
                List<string> creditamounts = new List<string>();
                LedgerTransaction lt = new LedgerTransaction();
                //lt.LedgerTransactionId = TransactionId;
                lt.Date = ((FinanceViewModel)DataContext).JournalDate;
                lt.Description = textBox1.Text;
                
                lt.UserId =((FinanceViewModel)DataContext).FakeUser.UserId;
                ((FinanceViewModel)DataContext).Service.InsertLedgerTransaction(lt);
                foreach (var item in jdsd)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = item.amount;
                    ltd.LedgerAccountId = item.LedgerAccountId;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ((FinanceViewModel)DataContext).Service.InsertLedgerTransactionDetail(ltd);
                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = item.LedgerAccountId;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Debit = item.amount;
                    lg.JournalEntryDate = lt.Date;
                    ((FinanceViewModel)DataContext).Service.InsertLedgerGeneral(lg);
                    debitaccounts.Add(lg.LedgerAccount.AccountName);
                }
                foreach (var item in jdsc)
                {
                    LedgerTransactionDetail ltd = new LedgerTransactionDetail();
                    ltd.Amount = item.amount;
                    ltd.LedgerAccountId = item.LedgerAccountId;
                    ltd.LedgerTransactionId = lt.LedgerTransactionId;
                    ((FinanceViewModel)DataContext).Service.InsertLedgerTransactionDetail(ltd);
                    LedgerGeneral lg = new LedgerGeneral();
                    lg.LedgerAccountId = item.LedgerAccountId;
                    lg.LedgerTransactionId = lt.LedgerTransactionId;
                    lg.Credit = item.amount;
                    lg.JournalEntryDate = lt.Date;
                    ((FinanceViewModel)DataContext).Service.InsertLedgerGeneral(lg);
                    creditamounts.Add(lg.LedgerAccount.AccountName);
                }
                if (lt.Description.Length < 1)
                {
                    StringBuilder sb = new StringBuilder();
                    // sb.Append("Debit Account(s)");
                    foreach (var item in debitaccounts)
                    {
                        sb.AppendLine(item);
                    }
                    // sb.Append("Credit Account(s)");
                    foreach (var item in creditamounts)
                    {
                        sb.AppendLine(item);
                    }
                    lt.Description = sb.ToString();
                }
             
              
                ((FinanceViewModel)DataContext).Service.SaveAll();
              //  Messenger.Default.Send<UpdateView>(new Utility.UpdateView());
                ((FinanceViewModel)DataContext).Init2();
               // ((FinanceViewModel)DataContext).getJournalsBydate();
                ResetControls();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Debit and Credit Do Not Balance")
                {
                    throw ex;
                }
                else
                {
                    ResetControls();
                    throw ex;
                   
                }
            }
        }
        private bool IsJournalEntryValid()
        {
            try
            {
                Dictionary<int, int> ledgerids = new Dictionary<int, int>();
                foreach (var item in jdsd)
                {
                    ledgerids.Add(item.LedgerAccountId, item.LedgerAccountId);
                }
                foreach (var item in jdsc)
                {
                    ledgerids.Add(item.LedgerAccountId, item.LedgerAccountId);
                }
                return true;
            }
            catch
            {
                // throw new Exception("Journal Entry Contains Possible Wrong Account(s)");
                MessageBox.Show("Journal Entry Contains Possible Wrong Account(s)");
                return false;
            }
        }
        public List<JournalData> jdsd { get; set; }
        public List<JournalData> jdsc { get; set; }
        private void creditcomboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void textBox21_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
            if (e.Key == System.Windows.Input.Key.Decimal|| e.Key == System.Windows.Input.Key.OemPeriod)
            {
                if ((sender as TextBox).Text.Contains("."))
                    e.Handled = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        //private void save1_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = dpicker.SelectedDate <= DateTime.Now.Date;
        //}
        //private void save1_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        //{
        //    SaveJournalEntry();
        //}
    }
}

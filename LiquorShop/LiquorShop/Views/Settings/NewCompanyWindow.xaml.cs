using LiquorShop.ViewModels.Settings;
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

namespace LiquorShop.Views.Settings
{
    /// <summary>
    /// Interaction logic for NewCompanyWindow.xaml
    /// </summary>
    public partial class NewCompanyWindow : Window
    {
        SettingsViewModel vm;
        public NewCompanyWindow(SettingsViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext =this.vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.SelectedCompany.IsActive = true;
        }

        private void coadeletebtn_Click(object sender, RoutedEventArgs e)
        {
            vm.SelectedCompany = vm.SelectedCompanyBackUp;
            MessageBox.Show("No new company created");
            this.Close();
        }
    }
}

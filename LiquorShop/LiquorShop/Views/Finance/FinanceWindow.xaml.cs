using MahApps.Metro.Controls;
using Rms.InfraStructures;
using Rms.ViewModels.Accounts;
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
using Unity;
namespace LiquorShop.Views.Finance
{
    /// <summary>
    /// Interaction logic for FinanceView.xaml
    /// </summary>
    public partial class FinanceWindow : MetroWindow
    {
        FinanceViewModel vm;
        public FinanceWindow()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<FinanceViewModel>();
            DataContext = vm;
        }
        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            //ReportsView view = new Views.ReportsView();
            //MessageBoxResult result = MessageBox.Show("Do you want to Close This Window?\n If No Both Window Will Be Opened! ", "Close Window", MessageBoxButton.YesNo);

            //if (result == MessageBoxResult.Yes)
            //{
            //    this.Close();
            //}
            //view.ShowDialog();

        }

        private void ChartsButton_Click(object sender, RoutedEventArgs e)
        {
            //ChartsView view = new Views.ChartsView();
            //MessageBoxResult result = MessageBox.Show("Do you want to Close This Window?\n If No Both Window Will Be Opened! ", "Close Window", MessageBoxButton.YesNo);
            //if (result == MessageBoxResult.Yes)
            //{
            //    this.Close();
            //}
            //view.ShowDialog();
        }
    }
}


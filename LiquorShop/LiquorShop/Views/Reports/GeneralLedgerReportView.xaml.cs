using LiquorShop.ViewModels.Reports;
using SAPBusinessObjects.WPF.Viewer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using Rms.InfraStructures;
namespace LiquorShop.Views.Reports
{
    /// <summary>
    /// Interaction logic for GeneralLedgerControl.xaml
    /// </summary>
    public partial class GeneralLedgerReportView : UserControl
    {
        GeneralLedgerViewModel vm;
        public GeneralLedgerReportView()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<GeneralLedgerViewModel>();
        }

        private void accountcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(fromdatepicker.SelectedDate>todatepicker.SelectedDate)
            {
                MessageBox.Show("Start Date Must Be Equal Or Greater Than End Date");
                return;
            }
           //((GeneralLedgerViewModel)DataContext).GetLedgerReport(this.ReportViewr as CrystalReportsViewer);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         ((GeneralLedgerViewModel)DataContext).GetLedgerReport(this.ReportViewr as CrystalReportsViewer);
        }
    }
}

using Rms.ViewModels.Charts;
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
using OxyPlot.Wpf;
using OxyPlot;
using System.Collections.ObjectModel;
using Rms.Data;

namespace Rms.UserControls.Charts
{
    /// <summary>
    /// Interaction logic for IncomeChartControl.xaml
    /// </summary>
    public partial class IncomeChartControl : UserControl
    {
        IncomeChartViewModel vm;
        public IncomeChartControl()
        {
            vm = ContainerHelper.Container.Resolve<IncomeChartViewModel>();
            DataContext = vm;
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //((IncomeChartViewModel)DataContext).LedgerAccounts = new ObservableCollection<LedgerAccount>(vm.Service.GetAllLedgerAccounts());
            //((IncomeChartViewModel)DataContext).LedgerGenerals = new ObservableCollection<Ledgergeneral>(vm.Service.GetAllLedgerGenerals());
            //((IncomeChartViewModel)DataContext).PlotModel = new PlotModel();
            //((IncomeChartViewModel)DataContext).SetUpModelNew();
            //((IncomeChartViewModel)DataContext).GetIncomeChart();
            ////PlotView view = obj as PlotView;
            ////view.Model = PlotModel;
            //oxy.Model = ((IncomeChartViewModel)DataContext).PlotModel;
            //oxy.InvalidatePlot();
           
        }
    }
}

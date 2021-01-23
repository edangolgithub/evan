using Rms.Data;
using Rms.InfraStructures;
using Rms.ViewModels.Charts;
using System.Windows.Controls;
using Unity;
namespace Rms.UserControls.Charts
{
    /// <summary>
    /// Interaction logic for AccountChartControl.xaml
    /// </summary>
    public partial class AccountChartControl : UserControl
    {
        AccountChartViewModel vm;
        public AccountChartControl()
        {
            vm = ContainerHelper.Container.Resolve<AccountChartViewModel>();
            DataContext = vm;
            InitializeComponent();
        }

        private void accountcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             ((AccountChartViewModel)DataContext).LoadChartForSingleAccount();
            oxy.Model = ((AccountChartViewModel)DataContext).PlotModel;
            oxy.InvalidatePlot(true);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ((AccountChartViewModel)DataContext).InitLoaded(oxy);
        }
    }
}

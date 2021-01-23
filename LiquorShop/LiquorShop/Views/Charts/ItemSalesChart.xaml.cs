using Rms.InfraStructures;
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
namespace Rms.UserControls.Charts
{
    /// <summary>
    /// Interaction logic for ItemSalesChart.xaml
    /// </summary>
    public partial class ItemSalesChart : UserControl
    {
        ItemSalesChartViewModel ItemSalesChartViewModel;
        public ItemSalesChart()
        {
            ItemSalesChartViewModel = ContainerHelper.Container.Resolve<ItemSalesChartViewModel>();
            DataContext = ItemSalesChartViewModel;
            InitializeComponent();


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              ((ItemSalesChartViewModel)DataContext).GetItemSalesChartForSingleItem();

            oxy.Model = ((ItemSalesChartViewModel)DataContext).PlotModel;
            oxy.InvalidatePlot(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((ItemSalesChartViewModel)DataContext).InitLoaded(oxy);

        }
    }
}

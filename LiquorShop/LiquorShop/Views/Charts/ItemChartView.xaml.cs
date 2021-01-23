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
using Rms.InfraStructures;
using Unity;
namespace Rms.UserControls.Charts
{
    /// <summary>
    /// Interaction logic for ItemChartView.xaml
    /// </summary>
    public partial class ItemChartView : UserControl
    {
        ItemChartViewModel vm;
        public ItemChartView()
        {
            vm = ContainerHelper.Container.Resolve<ItemChartViewModel>();
            DataContext = vm;
            InitializeComponent();
          // Task.Run(()=> ((ItemChartViewModel)DataContext).);
        }
    }
}

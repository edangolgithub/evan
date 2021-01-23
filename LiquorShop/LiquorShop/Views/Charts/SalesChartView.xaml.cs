using Rms.Data;
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
    /// Interaction logic for SalesChartViewModel.xaml
    /// </summary>
    public partial class SalesChartView : UserControl
    {
        SalesChartViewModel SalesChartViewModel;
        public SalesChartView()
        {
            SalesChartViewModel = ContainerHelper.Container.Resolve<SalesChartViewModel>();
            DataContext = SalesChartViewModel;
            InitializeComponent();
            
        }

      
    }
}

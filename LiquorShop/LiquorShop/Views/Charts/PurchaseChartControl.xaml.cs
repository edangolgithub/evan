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
using Rms.ViewModels.Charts;
using Unity;
using Rms.InfraStructures;
namespace Rms.UserControls.Charts
{
    /// <summary>
    /// Interaction logic for PurchaseChartControl.xaml
    /// </summary>
    public partial class PurchaseChartControl : UserControl
    {
        PurchaseChartViewModel vm;
        public PurchaseChartControl()
        {
            vm = ContainerHelper.Container.Resolve<PurchaseChartViewModel>();
            DataContext = vm;
            InitializeComponent();
           
        }
    }
}

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
using Rms.ViewModels.Reports;
using Unity;
using Rms.InfraStructures;
using LiquorShop.ViewModels.Reports;

namespace LiquorShop.Views.Reports
{
    /// <summary>
    /// Interaction logic for PurchaseReportControl.xaml
    /// </summary>
    public partial class PurchaseReportControl : UserControl
    {
        PurchaseReportViewModel vm;
        public PurchaseReportControl()
        {
            vm = ContainerHelper.Container.Resolve<PurchaseReportViewModel>();
            DataContext = vm;
            InitializeComponent();
        }
    }
}

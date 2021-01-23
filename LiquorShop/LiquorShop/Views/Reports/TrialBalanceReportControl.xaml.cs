using Rms.ViewModels.Reports;
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
using LiquorShop.ViewModels.Reports;

namespace LiquorShop.Views.Reports
{
    /// <summary>
    /// Interaction logic for TrialBalanceReportControl.xaml
    /// </summary>
    public partial class TrialBalanceReportControl : UserControl
    {
        TrialBalanceReportViewModel vm;
        public TrialBalanceReportControl()
        {
            vm = ContainerHelper.Container.Resolve<TrialBalanceReportViewModel>();
            DataContext = vm;
            InitializeComponent();
        }
    }
}

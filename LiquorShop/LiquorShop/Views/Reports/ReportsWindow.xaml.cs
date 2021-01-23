using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using Rms.InfraStructures;
using Unity;

namespace LiquorShop.Views.Reports
{
    /// <summary>
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : MetroWindow
    {
        ReportsMainViewModel vm;
        public ReportsWindow()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<ReportsMainViewModel>();
            DataContext = vm;
        }
    }
}

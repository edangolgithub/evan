using LiquorShop.ViewModels.Sales;
using MahApps.Metro.Controls;
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

namespace LiquorShop.Views.Sales
{
    /// <summary>
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : MetroWindow
    {
        SalesMainViewModel vm;
        public SalesWindow()
        {
            InitializeComponent();
           vm = ContainerHelper.Container.Resolve<SalesMainViewModel>();
          DataContext = vm;
        }
    }
}

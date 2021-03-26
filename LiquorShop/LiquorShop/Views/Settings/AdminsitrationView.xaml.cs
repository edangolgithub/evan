using Rms.ViewModels;
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
using MahApps.Metro.Controls;

namespace Rms.Views
{
    /// <summary>
    /// Interaction logic for AdminsitrationView.xaml
    /// </summary>
    public partial class AdministrationView : MetroWindow
    {
        AdministrationViewModel vm;
        public AdministrationView()
        {
            vm = ContainerHelper.Container.Resolve<AdministrationViewModel>();
            DataContext = vm;
            InitializeComponent();
          
        }
    }
}

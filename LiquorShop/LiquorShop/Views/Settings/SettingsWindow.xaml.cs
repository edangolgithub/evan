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
using LiquorShop.ViewModels.Settings;
using MahApps.Metro.Controls;

namespace LiquorShop.Views.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        SettingsViewModel vm;
        public SettingsWindow()
        {

            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<SettingsViewModel>();
            DataContext = vm;
        }
    }
}

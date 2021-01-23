using LiquorShop.ViewModels.Beverages;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Rms;
using Rms.InfraStructures;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;


namespace LiquorShop.Views.Beverages
{
    /// <summary>
    /// Interaction logic for BeveragesWindow.xaml
    /// </summary>
    public partial class BeveragesWindow : MetroWindow
    {
        BeverageMainViewModel vm;
        public BeveragesWindow()
        {
            InitializeComponent();
            vm = ContainerHelper.Container.Resolve<BeverageMainViewModel>();
            DataContext = vm;
        }
    }
}

using MahApps.Metro.Controls;
using Rms.Data;
using Rms.InfraStructures;
using Rms.ViewModels.Settings;
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
using Unity;
namespace Rms.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : MetroWindow
    {
        SettingsViewModel vm;
        public SettingsView()
        {
            vm = ContainerHelper.Container.Resolve<SettingsViewModel>();
            DataContext = vm;
            InitializeComponent();
          
            
        }

        //private void CheckListBox_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        //{
        //    var item = e.Item as Item;
        //    //if (item.ShowInChart == true)
        //    //{
        //    //    item.ShowInChart = false;
        //    //}
        //    //else
        //    //{
        //    //    item.ShowInChart = true; ;
        //    //}
        //     ((SettingsViewModel)DataContext).ChangedItems.Add(item);
        //}

        //private void inventorylistbox_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        //{
        //    var item = e.Item as InventoryItem;


        ////    if (item.ShowInChart == true)
        ////    {
        ////        item.ShowInChart = false;
        ////    }
        ////    else
        ////    {
        ////        item.ShowInChart = true; ;
        ////    }
        //     ((SettingsViewModel)DataContext).ChangedInventoryItems.Add(item);
        //}
    }
}

using Rms.InfraStructures;
using Rms.ViewModels.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
namespace Rms.ViewModels
{
    class ChartsMainViewModel:ViewModelBase
    {
        ViewModelBase _CurrentViewModel;
        ItemChartViewModel ItemChartViewModel;
        SalesChartViewModel SalesChartViewModel;
        AccountChartViewModel AccountChartViewModel;
        PurchaseChartViewModel PurchaseChartViewModel;
        ItemSalesChartViewModel ItemSalesChartViewModel;
        //InventoryPurchaseChartViewModel InventoryPurchaseChartViewModel;
        IncomeChartViewModel IncomeChartViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _CurrentViewModel;
            }
            set
            {
                if (_CurrentViewModel != value)
                {
                    _CurrentViewModel = value;
                    notify("CurrentViewModel");
                }
            }
        }

        public ChartsMainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            
           
           
           
        }

        private void OnNav(string Destination)
        {
            switch (Destination)
            {
                case "itemschartparam":
                    ItemChartViewModel = ContainerHelper.Container.Resolve<ItemChartViewModel>();
                    CurrentViewModel = ItemChartViewModel;
                    break;
                case "PurchaseChartParam":                    
                    PurchaseChartViewModel = ContainerHelper.Container.Resolve<PurchaseChartViewModel>();
                    CurrentViewModel = PurchaseChartViewModel;
                    break;
                case "saleschartparam":
                    SalesChartViewModel = ContainerHelper.Container.Resolve<SalesChartViewModel>();
                    CurrentViewModel = SalesChartViewModel;
                    break;
                case "accountchartparam":
                    AccountChartViewModel = ContainerHelper.Container.Resolve<AccountChartViewModel>();
                    CurrentViewModel = AccountChartViewModel;
                    break;
                case "ItemSalesChartParam":
                    ItemSalesChartViewModel = ContainerHelper.Container.Resolve<ItemSalesChartViewModel>();
                    CurrentViewModel = ItemSalesChartViewModel;
                    break;
                case "InventoryPurchaseChartParam":
                    //InventoryPurchaseChartViewModel = ContainerHelper.Container.Resolve<InventoryPurchaseChartViewModel>();
                    //CurrentViewModel = InventoryPurchaseChartViewModel;
                    break;
                case "IncomeChartParam":
                    IncomeChartViewModel = ContainerHelper.Container.Resolve<IncomeChartViewModel>();
                    CurrentViewModel = IncomeChartViewModel;
                    break;


            }
        }


        #region NavCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="NavCommand" />
        /// </summary>
        private RelayCommand _NavCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NavCommand
        {
            get
            {
                if (_NavCommand == null)
                { _NavCommand = new RelayCommand(NavCommand_Execute, NavCommand_CanExecute); }

                return _NavCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="NavCommand" />
        /// </summary>
        private void NavCommand_Execute(object obj)
        {
            OnNav(obj.ToString());
        }

        /// <summary>
        /// Determines if <see cref="NavCommand" /> is allowed to execute
        /// </summary>
        private Boolean NavCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
    }
}

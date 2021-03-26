using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Rms;
using Rms.InfraStructures;
using Unity;
namespace LiquorShop.ViewModels.Suppliers
{
   public class SuppliersMainViewModel:ViewModelBase
    {
        ViewModelBase _CurrentViewModel;
        SuppliersViewModel SupplierListViewModel; 
        
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
        public SuppliersMainViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            //Task.Run(() => Init());
        }

        private void Init()
        {
            SupplierListViewModel = ContainerHelper.Container.Resolve<SuppliersViewModel>();
         
            CurrentViewModel = SupplierListViewModel;
        }

        private void OnNav(string Destination)
        {
            switch(Destination)
            {
                case "SuppliersParam":
                    CurrentViewModel = SupplierListViewModel;
                    break;
                //case "SupplierDetailsParam":
                //    CurrentViewModel = SupplierDetailViewModel;
                //    break;
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

        #region common
        bool _IsDataNotLoaded;
        public bool IsDataNotLoaded
        {
            get
            {
                return _IsDataNotLoaded;
            }
            set
            {
                if (_IsDataNotLoaded != value)
                {
                    _IsDataNotLoaded = value;
                    notify("IsDataNotLoaded");
                }
            }
        }

        #region LoadedCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="LoadedCommand" />
        /// </summary>
        private RelayCommand _LoadedCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                if (_LoadedCommand == null)
                { _LoadedCommand = new RelayCommand(LoadedCommand_Execute, LoadedCommand_CanExecute); }

                return _LoadedCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="LoadedCommand" />
        /// </summary>
        private async void LoadedCommand_Execute(object obj)
        {
            IsDataNotLoaded = true;
            await Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false; });

        }

        /// <summary>
        /// Determines if <see cref="LoadedCommand" /> is allowed to execute
        /// </summary>
        private Boolean LoadedCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
        #endregion

    }
}

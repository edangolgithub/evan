using System;
using System.Threading.Tasks;
using Rms;
namespace LiquorShop.ViewModels.Customers
{
    public class CustomersMainViewModel:ViewModelBase
    {
        ViewModelBase _CurrentViewModel;
        CustomersViewModel CustomersViewModel; 
        
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
        public CustomersMainViewModel()
        {
           
        }

        private void Init()
        {
            //CustomersViewModel = ContainerHelper.Container.Resolve<CustomersViewModel>();         
            CurrentViewModel = CustomersViewModel;
        }

        private void OnNav(string Destination)
        {
            switch(Destination)
            {
                case "CustomersParam":
                    CurrentViewModel = CustomersViewModel;
                    break;
                //case "CustomerDetailsParam":
                //    CurrentViewModel = CustomerDetailViewModel;
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

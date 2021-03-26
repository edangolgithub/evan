using Rms;
using System;
using System.Threading.Tasks;
namespace LiquorShop.ViewModels.Users
{
    public class UserMainViewModel:ViewModelBase
    {

        
        UsersViewModel UsersViewModel;



        private ViewModelBase _CurrentViewModel;


        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _CurrentViewModel;
            }
            set
            {
                SetProperty<ViewModelBase>(ref _CurrentViewModel, value);
            }
        }
        public UserMainViewModel()
        {
            IsDataNotLoaded = true;
            
        }

        private void Init()
        {
           // UsersViewModel = ContainerHelper.Container.Resolve<UsersViewModel>();
           
            CurrentViewModel = UsersViewModel;
        }

        private void OnNav(string Destination)
        {
            switch (Destination)
            {
                case "userparam":
                    CurrentViewModel = UsersViewModel;
                    break;
                //case "inventorycategoriesparam":
                //    CurrentViewModel = InventoryCategoriesViewModel;
                //    break;
                //case "inventorystocksParam":
                //    CurrentViewModel = StocksViewModel;
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
        #region ExitWindowCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="ExitWindowCommand" />
        /// </summary>
        private RelayCommand _ExitWindowCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand ExitWindowCommand
        {
            get
            {
                if (_ExitWindowCommand == null)
                { _ExitWindowCommand = new RelayCommand(ExitWindowCommand_Execute, ExitWindowCommand_CanExecute); }

                return _ExitWindowCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="ExitWindowCommand" />
        /// </summary>
        private void ExitWindowCommand_Execute(object obj)
        {
           
        }

        /// <summary>
        /// Determines if <see cref="ExitWindowCommand" /> is allowed to execute
        /// </summary>
        private Boolean ExitWindowCommand_CanExecute(object obj)
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

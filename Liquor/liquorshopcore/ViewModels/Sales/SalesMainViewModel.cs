using Rms;
using System.Threading.Tasks;

namespace LiquorShop.ViewModels.Sales
{
    public class SalesMainViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;
        private SalesViewModel SalesViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                if (currentViewModel != value)
                {
                    currentViewModel = value;
                    notify("CurrentViewModel");
                }
            }
        }
        public SalesMainViewModel()
        {
        }

        private void Init()
        {
         
        }

        private void OnNav(string Destination)
        {
            switch (Destination)
            {
                case "SalesEntry":
                    CurrentViewModel = SalesViewModel;
                    break;
                    //case "PurchaseList":
                    //    PurchaseListViewModel = ContainerHelper.Container.Resolve<PurchaseListViewModel>();
                    //    CurrentViewModel = PurchaseListViewModel;
                    //    break;
                    //case "PurchaseData":
                    //    PurchaseDataWindowViewModel = ContainerHelper.Container.Resolve<PurchaseDataWindowViewModel>();
                    //    CurrentViewModel = PurchaseDataWindowViewModel;
                    //    break;
            }
        }

        #region NavCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="NavCommand" />
        /// </summary>
        private RelayCommand navCommand;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NavCommand
        {
            get
            {
                if (navCommand == null)
                { navCommand = new RelayCommand(NavCommand_Execute, NavCommand_CanExecute); }

                return navCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="NavCommand" />
        /// </summary>
        private void NavCommand_Execute(object obj) => OnNav(obj.ToString());

        /// <summary>
        /// Determines if <see cref="NavCommand" /> is allowed to execute
        /// </summary>
        private bool NavCommand_CanExecute(object obj) => true;

        #endregion
        #region ExitWindowCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="ExitWindowCommand" />
        /// </summary>
        private RelayCommand exitWindowCommand;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand ExitWindowCommand
        {
            get
            {
                if (exitWindowCommand == null)
                { exitWindowCommand = new RelayCommand(ExitWindowCommand_Execute, ExitWindowCommand_CanExecute); }

                return exitWindowCommand;
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
        private bool ExitWindowCommand_CanExecute(object obj) => true;

        #endregion

        #region common
        private bool isDataNotLoaded;
        public bool IsDataNotLoaded
        {
            get
            {
                return isDataNotLoaded;
            }
            set
            {
                if (isDataNotLoaded != value)
                {
                    isDataNotLoaded = value;
                    notify("IsDataNotLoaded");
                }
            }
        }

        #region LoadedCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="LoadedCommand" />
        /// </summary>
        private RelayCommand loadedCommand;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                if (loadedCommand == null)
                { loadedCommand = new RelayCommand(LoadedCommand_Execute, LoadedCommand_CanExecute); }

                return loadedCommand;
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
        private bool LoadedCommand_CanExecute(object obj) => true;

        #endregion
        #endregion
    }
}
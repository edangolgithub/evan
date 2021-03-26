using Rms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LiquorShopService.Services;
using System.Windows;
using Rms;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Suppliers
{
    class SuppliersViewModel:ViewModelBase
    {
        ISupplierService Service;
        public SuppliersViewModel(ISupplierService Service)
        {
            this.Service = Service;
            IsDataNotLoaded = true;
            IsDataLoaded = false;
            Task.Run(() => Init());
        }


        private bool _IsDataNotLoaded;


        public bool IsDataNotLoaded
        {
            get
            {
                return _IsDataNotLoaded;
            }
            set
            {
                SetProperty<bool>(ref _IsDataNotLoaded, value);
            }
        }
        private bool _IsDataLoaded;


        public bool IsDataLoaded
        {
            get
            {
                return _IsDataLoaded;
            }
            set
            {
                SetProperty<bool>(ref _IsDataLoaded, value);
            }
        }
        private async void Init()
        {
            Suppliers = new ObservableCollection<Supplier>(await Service.GetAllSuppliersAsync());
            IsDataNotLoaded = false;
            IsDataLoaded = true;
        }

        ObservableCollection<Supplier> _Suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get
            {
                return _Suppliers;
            }
            set
            {
                if (_Suppliers != value)
                {
                    _Suppliers = value;
                    notify("Suppliers");
                }
            }
        }
        Supplier _SelectedSupplier;
        public Supplier SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            }
            set
            {
                if (_SelectedSupplier != value)
                {
                    _SelectedSupplier = value;
                    notify("SelectedSupplier");
                }
            }
        }

        #region NewSupplierCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="NewSupplierCommand" />
        /// </summary>
        private RelayCommand _NewSupplierCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand NewSupplierCommand
        {
            get
            {
                if (_NewSupplierCommand == null)
                { _NewSupplierCommand = new RelayCommand(NewSupplierCommand_Execute, NewSupplierCommand_CanExecute); }

                return _NewSupplierCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="NewSupplierCommand" />
        /// </summary>
        private void NewSupplierCommand_Execute(object obj)
        {
            SelectedSupplier = new Supplier();
        }

        /// <summary>
        /// Determines if <see cref="NewSupplierCommand" /> is allowed to execute
        /// </summary>
        private Boolean NewSupplierCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion


        #region SaveSupplierCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="SaveSupplierCommand" />
        /// </summary>
        private RelayCommand _SaveSupplierCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand SaveSupplierCommand
        {
            get
            {
                if (_SaveSupplierCommand == null)
                { _SaveSupplierCommand = new RelayCommand(SaveSupplierCommand_Execute, SaveSupplierCommand_CanExecute); }

                return _SaveSupplierCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="SaveSupplierCommand" />
        /// </summary>
        private async void SaveSupplierCommand_Execute(object obj)
        {
            var v = await Service.SaveSupplierAsync(SelectedSupplier);
            if (v > 0)
            {
                Init();
                SelectedSupplier = null;
                SelectedSupplier = new Supplier();
            }
        }

        /// <summary>
        /// Determines if <see cref="SaveSupplierCommand" /> is allowed to execute
        /// </summary>
        private Boolean SaveSupplierCommand_CanExecute(object obj)
        {
            if(SelectedSupplier!=null)
            {
                if (!string.IsNullOrEmpty(SelectedSupplier.SupplierName))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region DeleteSupplierCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="DeleteSupplierCommand" />
        /// </summary>
        private RelayCommand _DeleteSupplierCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand DeleteSupplierCommand
        {
            get
            {
                if (_DeleteSupplierCommand == null)
                { _DeleteSupplierCommand = new RelayCommand(DeleteSupplierCommand_Execute, DeleteSupplierCommand_CanExecute); }

                return _DeleteSupplierCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="DeleteSupplierCommand" />
        /// </summary>
        private void DeleteSupplierCommand_Execute(object obj)
        {
        }

        /// <summary>
        /// Determines if <see cref="DeleteSupplierCommand" /> is allowed to execute
        /// </summary>
        private Boolean DeleteSupplierCommand_CanExecute(object obj)
        {
            if (SelectedSupplier != null)
            {
                if (SelectedSupplier.SupplierId > 0)
                {
                    return true;
                }
              
            }
            return false;
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
            Window w = (Window)obj;
            w.Close();
        }

        /// <summary>
        /// Determines if <see cref="ExitWindowCommand" /> is allowed to execute
        /// </summary>
        private Boolean ExitWindowCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
    }
}

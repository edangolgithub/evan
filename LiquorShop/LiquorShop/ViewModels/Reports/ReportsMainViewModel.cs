using Rms.InfraStructures;
using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using LiquorShop.ViewModels.Reports;

namespace Rms.ViewModels.Reports
{
    class ReportsMainViewModel:ViewModelBase
    {
        ViewModelBase _CurrentViewModel;
        GeneralLedgerViewModel GeneralLedgerViewModel;
        PurchaseReportViewModel PurchaseReportViewModel;
        SalesReportViewModel SalesReportViewModel;
        IncomeStatementReportViewModel IncomeStatementReportViewModel;
        TrialBalanceReportViewModel TrialBalanceReportViewModel;
        BalanceSheetReportViewModel BalanceSheetReportViewModel;
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
        bool _IsReportMayLoaded;
        public bool IsReportMayLoaded
        {
            get
            {
                return _IsReportMayLoaded;
            }
            set
            {
                if (_IsReportMayLoaded != value)
                {
                    _IsReportMayLoaded = value;
                    notify("IsReportMayLoaded");
                }
            }
        }

        public ReportsMainViewModel()
        {
           
        }
        //public bool Isvirgin { get; set; }

        private void Init()
        {
            //IsReportMayLoaded = true;
            //Isvirgin = true;
            IsDataNotLoaded = true;
           
           
            //
        }
        private void OnNav(string Destination)
        {

            switch (Destination)
            {

                case "generalledgerParam":
                    GeneralLedgerViewModel = ContainerHelper.Container.Resolve<GeneralLedgerViewModel>();
                    CurrentViewModel = GeneralLedgerViewModel;
                    break;
                    case "PurchaseReportParam":
                    PurchaseReportViewModel = ContainerHelper.Container.Resolve<PurchaseReportViewModel>();
                    CurrentViewModel = PurchaseReportViewModel;
                   break;
                case "SalesReportParam":
                    SalesReportViewModel = ContainerHelper.Container.Resolve<SalesReportViewModel>();
                    CurrentViewModel = SalesReportViewModel;
                    break;
                case "IncomeReportParam":
                    IncomeStatementReportViewModel = ContainerHelper.Container.Resolve<IncomeStatementReportViewModel>();
                    CurrentViewModel = IncomeStatementReportViewModel;
                    break;

                case "TrialBalanceReportParam":
                    TrialBalanceReportViewModel = ContainerHelper.Container.Resolve<TrialBalanceReportViewModel>();
                    CurrentViewModel = TrialBalanceReportViewModel;
                    break;
                case "BalanceSheetReportParam":
                    BalanceSheetReportViewModel = ContainerHelper.Container.Resolve<BalanceSheetReportViewModel>();
                    CurrentViewModel = BalanceSheetReportViewModel;
                    break;
            }

            //if (Isvirgin)
            //{
            //    IsReportMayLoaded = false;
            //    await Task.Delay(5000).ContinueWith(a => IsReportMayLoaded = true);
            //}
            //Isvirgin = false;
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

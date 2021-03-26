using Rms.Data;
using Rms.Reports;
using LiquorShopService.Services;
using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Rms;
using LiquorShop.Reports;
using LiquorShop.Classes;
using TescoWineShopSql;

namespace LiquorShop.ViewModels.Reports
{
    public class PurchaseReportViewModel : ViewModelBase
    {

        private bool _IsReportNotLoaded;
        public bool IsReportNotLoaded
        {
            get { return _IsReportNotLoaded; }
            set
            {
                SetProperty<bool>(ref _IsReportNotLoaded, value);
            }
        }

        private bool _IsReportLoaded;
        public bool IsReportLoaded
        {
            get { return _IsReportLoaded; }
            set
            {
                SetProperty<bool>(ref _IsReportLoaded, value);
            }
        }
        PurchaseReport report; 
        private void Init()
        {
            IsReportLoaded = true;
            report = new PurchaseReport();
        }

        IPurchaseService Service;
        public PurchaseReportViewModel(IPurchaseService Service)
        {
            this.Service = Service;
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
            IsDataLoaded = true;
        }

        DateTime _FromDate;
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    notify("FromDate");
                }
            }
        }
        DateTime _ToDate;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                if (_ToDate != value)
                {
                    _ToDate = value;
                    notify("ToDate");
                }
            }
        }
        List<PurchaseReportVm> PurchaseReportVms;
        public async Task<IEnumerable<Purchase>> GetPurchaseReport()
        {
            return await Service.FindPurchasesByDateAsync(FromDate, ToDate);
        }


        #region GetPurchaseReportCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetPurchaseReportCommand" />
        /// </summary>
        private RelayCommand _GetPurchaseReportCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetPurchaseReportCommand
        {
            get
            {
                if (_GetPurchaseReportCommand == null)
                { _GetPurchaseReportCommand = new RelayCommand(GetPurchaseReportCommand_Execute, GetPurchaseReportCommand_CanExecute); }

                return _GetPurchaseReportCommand;
            }
        }

        /// <summary>
        /// Implements the execution of <see cref="GetPurchaseReportCommand" />
        /// </summary>
        private async void GetPurchaseReportCommand_Execute(object obj)
        {
            try
            {
                IsReportLoaded = false;
                IsReportNotLoaded = true;
                PurchaseReportVms = new List<PurchaseReportVm>();
                CrystalReportsViewer ReportViewer = (CrystalReportsViewer)obj;

                data = await Task.Run(async () => await GetPurchaseReport());
                await Task.FromResult(0).ContinueWith(b =>
                 {

                     PurchaseReportVms = data.Select(

                         a => new PurchaseReportVm
                         {
                             Beverage = a.Beverage.Name,
                             PurchaseDate = a.PurchaseDate,
                             Metric = a.Metric.ToString(),
                             Quantity = a.Quantity,
                             UnitPrice = a.UnitPrice,
                             FromDate = FromDate.ToLongDateString(),
                             ToDate = ToDate.ToLongDateString(),
                             Rate=a.Rate,
                             MetricQuantity=a.MetricQuantity,
                             LineTotalAmout = a.Quantity * a.UnitPrice,
                             //Supplier=a.Supplier.SupplierName
                         }).ToList();
                 });
                await System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                    (ThreadStart)delegate

                    {
                        IsReportLoaded = true;
                        IsReportNotLoaded = false;
                        if (PurchaseReportVms.Count<1)
                        {
                            MessageBox.Show("No Report");
                            return;
                        }
                        report.SetDataSource(PurchaseReportVms);
                        ReportViewer.ViewerCore.ReportSource = report;
                        
                    });
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        IEnumerable<Purchase> data = new List<Purchase>();
        /// <summary>
        /// Determines if <see cref="GetPurchaseReportCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetPurchaseReportCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion

        #region common



        private bool _IsDataLoaded;
        public bool IsDataLoaded
        {
            get { return _IsDataLoaded; }
            set
            {
                SetProperty<bool>(ref _IsDataLoaded, value);
            }
        }
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
            IsDataLoaded = false;
            IsDataNotLoaded = true;
            await Task.Run(() => Init()).ContinueWith(a => { IsDataNotLoaded = false; IsDataLoaded = true; });



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

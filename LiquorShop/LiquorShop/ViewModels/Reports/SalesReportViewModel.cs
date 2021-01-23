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
using LiquorShop.Classes;
using LiquorShop.Reports;
using Rms;

namespace LiquorShop.ViewModels.Reports
{
    public class SalesReportViewModel : ViewModelBase
    {
        public List<SalesReportVm> SalesReportVms { get; private set; }

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
      

        private async void GetSalesReport(CrystalReportsViewer ReportViewer)
        {
            IsReportLoaded = false;
            IsReportNotLoaded = true;
            SalesReportVms = new List<SalesReportVm>();


            var data = await Task.Run(async () => await Service.FindSalesByDateAsync(FromDate, ToDate));
            // await Task.CompletedTask.ContinueWith(b => this is fucking for fucking dot net 4.6.2 only
            await Task.FromResult(0).ContinueWith(b =>
            {

                SalesReportVms = data.Select(

                       a => new SalesReportVm
                       {
                           Beverage = a.Beverage.Name,
                           Quantity = a.Quantity,
                           Paid = (a.Paid ?? 0)*a.Quantity,
                           Discount = a.Discount ?? 0,
                           SaleDate = a.SaleDate,
                           UnitPrice = a.UnitPrice,
                           SalesReportVmId = a.SaleId,
                           Customer =a.Customer==null?"": a.Customer.CustomerName,
                           ToDate = ToDate.ToLongDateString(),
                           
                           FromDate = FromDate.ToLongDateString()
                           //SalesDetails = new List<SalesDetail>(GetSalesDetails(a.OrderId))
                           
                       }).ToList();

            });


            await System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                    (ThreadStart)delegate

                    {
                        IsReportLoaded = true;
                        IsReportNotLoaded = false;
                        if (SalesReportVms.Count < 1)
                        {
                            MessageBox.Show("No Report");
                            return;

                        }

                        report.SetDataSource(SalesReportVms);

                        ReportViewer.ViewerCore.ReportSource = report;

                    });
        }

        ISalesService Service;
        public SalesReportViewModel(ISalesService Service)
        {
            this.Service = Service;

        }
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
        SalesReport report;
        private void Init()
        {
            IsReportLoaded = true;
            report = new SalesReport();

        }
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
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
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

        #region Commands

        #region GetSalesReportCommand Command

        /// <summary>
        /// Private member backing variable for <see cref="GetSalesReportCommand" />
        /// </summary>
        private RelayCommand _GetSalesReportCommand = null;

        /// <summary>
        /// Gets the command which The command's value
        /// </summary>
        public RelayCommand GetSalesReportCommand
        {
            get
            {
                if (_GetSalesReportCommand == null)
                { _GetSalesReportCommand = new RelayCommand(GetSalesReportCommand_Execute, GetSalesReportCommand_CanExecute); }

                return _GetSalesReportCommand;
            }
        }




        /// <summary>
        /// Implements the execution of <see cref="GetSalesReportCommand" />
        /// </summary>
        private async void GetSalesReportCommand_Execute(object obj)
        {
            await Task.Run(() => GetSalesReport(obj as CrystalReportsViewer));
        }



        /// <summary>
        /// Determines if <see cref="GetSalesReportCommand" /> is allowed to execute
        /// </summary>
        private Boolean GetSalesReportCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion

        #endregion

    }
}

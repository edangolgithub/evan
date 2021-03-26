using OxyPlot;
using OxyPlot.Wpf;
using Rms.Data;
using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TescoWineShopSql;
using WpfApp1;

namespace Rms.ViewModels.Charts
{
    class AccountChartViewModel : ViewModelBase
    {
        IAccountingService Service;
        public AccountChartViewModel(IAccountingService Service)
        {
            this.Service = Service;
        }
        LedgerAccount _SelectedLedgerAccount;
        public LedgerAccount SelectedLedgerAccount
        {
            get
            {
                return _SelectedLedgerAccount;
            }
            set
            {
                if (_SelectedLedgerAccount != value)
                {
                    _SelectedLedgerAccount = value;
                    notify("SelectedLedgerAccount");
                }
            }
        }
        ObservableCollection<LedgerTransaction> _Ledgertransactions;
        public ObservableCollection<LedgerTransaction> LedgerTransactions
        {
            get
            {
                return _Ledgertransactions;
            }
            set
            {
                if (_Ledgertransactions != value)
                {
                    _Ledgertransactions = value;
                    notify("LedgerTransactions");
                }
            }
        }

        ObservableCollection<LedgerTransactionDetail> _LedgerTransactionDetails;
        public ObservableCollection<LedgerTransactionDetail> LedgerTransactionDetails
        {
            get
            {
                return _LedgerTransactionDetails;
            }
            set
            {
                if (_LedgerTransactionDetails != value)
                {
                    _LedgerTransactionDetails = value;
                    notify("LedgerTransactionDetails");
                }
            }
        }
        private ObservableCollection<LedgerAccount> _LedgerAccounts;
        public ObservableCollection<LedgerAccount> LedgerAccounts
        {
            get { return _LedgerAccounts; }
            set
            {
                _LedgerAccounts = value;
                notify("LedgerAccounts");
            }
        }
        IEnumerable<KeyValuePair<string, decimal>> _ChartData;
        public IEnumerable<KeyValuePair<string, decimal>> ChartData
        {
            get
            {
                return _ChartData;
            }
            set
            {
                if (_ChartData != value)
                {
                    _ChartData = value;
                    notify("ChartData");
                }
            }
        }
        //internal void LoadChart(LedgerAccount ledgerAccount, Chart chart)
        //{
        //    throw new NotImplementedException();
        //}
        internal void LoadChartForSingleAccount()
        {
            //    if (SelectedLedgerAccount != null)
            //    {
            //        var measurements = LedgerTransactionDetails
            //            .Join(LedgerTransactions,
            //            a => a.LedgerTransactionId,
            //            b => b.LedgerTransactionId,
            //            (a, b) =>
            //            new
            //            {
            //                Date = b.Date,
            //                Amount = a.Amount,
            //                Account = a.LedgerAccount.AccountName
            //            }).ToList();
            //        var k = measurements.Where(a => a.Account == SelectedLedgerAccount.AccountName)
            //       .GroupBy(s => s.Date)
            //       .Select(s => new { name = s.Key, sum = s.Sum(a => a.Amount) })
            //       .OrderBy(s => s.sum).AsEnumerable()
            //       .Select(a => new KeyValuePair<string, decimal>(a.name.Date.ToShortDateString(), (decimal)a.sum)).ToList();
            //        ChartData = k;
            //}

            try
            {
                PlotModel = new PlotModel();
                SetUpModelNew();
                var dataPerDetector = LedgerTransactionDetails.Where(a => a.LedgerAccountId == SelectedLedgerAccount.LedgerAccountId)
                    .Take(10).OrderBy(a => a.LedgerTransaction.Date.Date)
                    .GroupBy(m => m.LedgerTransaction.Date.Date)
                    .Select(g => new
                    {
                        Total = g.Sum(p => p.Amount),
                        date = g.Key,
                        Account = g.Select(a => a.LedgerAccount.AccountName).FirstOrDefault()
                    })
                        .ToList();

                //string bevname = GetBeverageName(data.Key);
                var lineSerie = new OxyPlot.Series.LineSeries
                {
                    ToolTip = string.Format(" {0} Sales", dataPerDetector.Select(a => a.Account).FirstOrDefault()),
                    // LabelFormatString = string.Format("{0} Sales", dataPerDetector.Select(a => a.Item).FirstOrDefault()),
                    StrokeThickness = 1,
                    MarkerSize = 3,
                    MarkerStroke = OxyColor.FromUInt32((uint)SelectedLedgerAccount.LedgerAccountId),

                    MarkerType = MarkerType.Diamond,// markerTypes[data.Key],
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("{0} Sales", dataPerDetector.Select(a => a.Account).FirstOrDefault())
                };
                dataPerDetector.ForEach(d => lineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.date), double.Parse(d.Total.ToString()))));
                PlotModel.Series.Add(lineSerie);


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        public class ChartHelper
        {
            public int Key { get; set; }
            public decimal Count { get; set; }
            public string Account { get; set; }
            public DateTime Date { get; set; }
        }
        internal void LoadChart()
        {
            try
            {
                foreach (var item in LedgerAccounts)
                {
                    var dataPerDetector = LedgerTransactionDetails.Where(a => a.LedgerAccountId == item.LedgerAccountId).OrderByDescending(m => m.LedgerTransaction.Date.Date).GroupBy(m => m.LedgerTransaction.Date.Date).
               Select(g => new { Total = g.Sum(p => p.Amount), date = g.Key }).Take(100).ToList();
                    var lineSerie = new OxyPlot.Series.LineSeries
                    {
                       // LabelFormatString = item.AccountName,
                        StrokeThickness = 1,
                        MarkerSize = 3,
                        MarkerStroke = OxyColor.FromUInt32((UInt32)item.LedgerAccountId),
                        MarkerType = MarkerType.Diamond,
                        CanTrackerInterpolatePoints = false,
                        Title = item.AccountName,
                        ToolTip = item.AccountName
                    };
                    foreach (var item2 in dataPerDetector)
                    {
                        lineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(item2.date), (double)item2.Total));
                    }
                    PlotModel.Series.Add(lineSerie);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //if (SelectedLedgerAccount != null)
            //{
            //    var measurements = LedgerTransactionDetails
            //        .Join(LedgerTransactions,
            //        a => a.LedgerTransactionId,
            //        b => b.LedgerTransactionId,
            //        (a, b) =>
            //        new
            //        {
            //            Date = b.Date,
            //            Amount = a.Amount,
            //            Account = a.LedgerAccount.AccountName
            //        }).ToList();
            //    var k = measurements.Where(a => a.Account == SelectedLedgerAccount.AccountName)
            //   .GroupBy(s => s.Date)
            //   .Select(s => new { name = s.Key, sum = s.Sum(a => a.Amount) })
            //   .OrderBy(s => s.sum).AsEnumerable()
            //   .Select(a => new KeyValuePair<string, decimal>(a.name.Date.ToShortDateString(), (decimal)a.sum)).ToList();
            //    ChartData = k;
            //}
        }
        public static List<T> MakeList<T>(T itemOftype)
        {
            List<T> newList = new List<T>();
            return newList;
        }
        private void SetUpModelNew()
        {

            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            // ; "Date", )
            var dateAxis = new OxyPlot.Axes.DateTimeAxis()
            { StringFormat = "dd/MM/yy", Title = "Transaction Date", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new OxyPlot.Axes.LinearAxis() { AxisDistance = 0, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Money In Rupee" };
            PlotModel.Axes.Add(valueAxis);
        }
        internal void InitLoaded(object obj)
        {
            IsDataLoaded = false;
            IsDataNotLoaded = true;
            Task.Run(async () =>
            {
                LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsForChartAsync());
                LedgerTransactionDetails = new ObservableCollection<LedgerTransactionDetail>(await Service.GetAllLedgerTransactionDetailsAsync());
                LedgerTransactions = new ObservableCollection<LedgerTransaction>(await Service.GetAllLedgerTransactionsAsync());
                PlotModel = new PlotModel();
                SetUpModelNew();
                LoadChart();
            }).ContinueWith(a =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    PlotView view = obj as PlotView;
                    view.Model = PlotModel;
                    view.InvalidatePlot();
                    IsDataLoaded = true;
                    IsDataNotLoaded = false;
                });
            });
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
        private void LoadedCommand_Execute(object obj)
        {
            InitLoaded(obj);
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
        private PlotModel _PlotModel;
        public PlotModel PlotModel
        {
            get { return _PlotModel; }
            set
            {
                _PlotModel = value;
                notify("PlotModel");
            }
        }
        private String _ChartHeader;
        public String ChartHeader
        {
            get { return _ChartHeader; }
            set
            {
                _ChartHeader = value;
                notify("ChartHeader");
            }
        }
    }

}

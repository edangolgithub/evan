using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using Rms.Data;
using LiquorShopService.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TescoWineShopSql;
using WpfApp1;

namespace Rms.ViewModels.Charts
{
    public class PurchaseChartViewModel : ViewModelBase
    {
        IPurchaseService Service;
        public PurchaseChartViewModel(IPurchaseService Service)
        {
            this.Service = Service;

        }
      
       
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
            { StringFormat= "dd/MM/yy", Title="Purchase Date", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new OxyPlot.Axes.LinearAxis() { AxisDistance = 0, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Money In Rupee" };
            PlotModel.Axes.Add(valueAxis);
        }
      
        private  void GetTotalPurchaseAmountChart()
        {

            try
            {
                     
                ChartHeader = "Purchase Total";
                PlotModel.Title = ChartHeader;
                var dataPerDetector = StockIns.OrderBy(m => m.PurchaseDate).GroupBy(m => m.PurchaseDate).
                Select(g => new { Total = g.Sum(p => p.UnitPrice*p.Quantity), date = g.Key }).ToList();
                
                var lineSerie = new OxyPlot.Series.LineSeries
                {
                    // LabelFormatString = dataPerDetector.Select(a=>a.Total).FirstOrDefault().ToString(),
                    StrokeThickness = 1,
                    MarkerSize = 3,
                   // MarkerStroke = OxyColors.Red,
                    MarkerType = MarkerType.Diamond,
                    CanTrackerInterpolatePoints = false,
                    Title = "Total Purchase",                     
                    Smooth = false,
                    
                };
                foreach (var item in dataPerDetector)
                {
                  
                    lineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(item.date), (double)item.Total));
                   
                }
                
               
                PlotModel.Series.Add(lineSerie);
               
            }
            catch(Exception ex)
            {
                throw ex;
            }

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

        public void InitLoaded(object obj)
        {
            IsDataLoaded = false;
            IsDataNotLoaded = true;


            Task.Run(async () =>
            {
                StockIns = new ObservableCollection<Purchase>(await Service.GetAllPurchasesAsync());
               
               
            }).ContinueWith(a =>
            {
                PlotModel = new PlotModel();
                SetUpModelNew();
                GetTotalPurchaseAmountChart();
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


        /// <summary>
        /// Determines if <see cref="LoadedCommand" /> is allowed to execute
        /// </summary>
        private Boolean LoadedCommand_CanExecute(object obj)
        {
            return true;
        }

        #endregion
        #endregion



        private ObservableCollection<Purchase> _StockIns;
        public ObservableCollection<Purchase> StockIns
        {
            get { return _StockIns; }
            set
            {
                SetProperty<ObservableCollection<Purchase>>(ref _StockIns, value);
            }
        }
    }
}
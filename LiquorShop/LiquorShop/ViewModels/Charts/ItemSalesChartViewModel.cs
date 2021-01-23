using OxyPlot;
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
    public class ItemSalesChartViewModel : ViewModelBase
    {
        ISalesService Service;
        public ItemSalesChartViewModel(ISalesService Service)
        {
            this.Service = Service;
        }
        public void GetItemSalesChart()
        {

            try
            {
                foreach (var item in Beverages)
                {
                    var dataPerDetector = OrderDetails.Where(a => a.BeverageId == item.BeverageId).OrderByDescending(m => m.SaleDate.Date).GroupBy(m => m.SaleDate.Date).
               Select(g => new { Total = g.Sum(p => p.Quantity), date = g.Key }).Take(100).ToList();
                    var lineSerie = new OxyPlot.Series.LineSeries
                    {
                       // LabelFormatString = item.ItemName,
                        StrokeThickness = 1,
                        MarkerSize = 3,
                        MarkerStroke = OxyColor.FromUInt32((UInt32)item.BeverageId),
                        MarkerType = MarkerType.Diamond,
                        CanTrackerInterpolatePoints = false,
                        Title = item.Name,
                        ToolTip = item.Name
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
        }

        internal void InitLoaded(object obj)
        {

            IsDataLoaded = false;
            IsDataNotLoaded = true;



            Task.Run(async () =>
            {
                Beverages = new ObservableCollection<Beverage>(await Service.GetAllBeveragesForChartAsync());
                  OrderDetails = new ObservableCollection<Sale>(await Service.GetAllSalesAsync());
                PlotModel = new PlotModel();
                SetUpModelNew();
                GetItemSalesChart();
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

        public void GetItemSalesChartForSingleItem()
        {

            try
            {
                PlotModel = new PlotModel();
                SetUpModelNew();
                var dataPerDetector = OrderDetails.Where(a => a.BeverageId == SelectedBeverage.BeverageId).Take(10).OrderBy(a => a.SaleDate.Date)
                    .GroupBy(m => m.SaleDate.Date).Select(g => new { Total = g.Sum(p => p.Quantity), date = g.Key, Item = g.Select(a => a.Beverage.Name).FirstOrDefault() }).ToList();

                //string bevname = GetBeverageName(data.Key);
                var lineSerie = new OxyPlot.Series.LineSeries
                {
                    ToolTip = string.Format(" {0} Sales", dataPerDetector.Select(a => a.Item).FirstOrDefault()),
                    // LabelFormatString = string.Format("{0} Sales", dataPerDetector.Select(a => a.Item).FirstOrDefault()),
                    StrokeThickness = 1,
                    MarkerSize = 3,
                    //MarkerStroke = OxyColor.FromUInt32((uint)data.Key),

                    MarkerType = MarkerType.Diamond,// markerTypes[data.Key],
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("{0} Sales", dataPerDetector.Select(a => a.Item).FirstOrDefault())
                };
                dataPerDetector.ForEach(d => lineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.date), d.Total)));
                PlotModel.Series.Add(lineSerie);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Beverage _SelectedBeverage;
        public Beverage SelectedBeverage
        {
            get { return _SelectedBeverage; }
            set
            {
                SetProperty<Beverage>(ref _SelectedBeverage, value);
                //GetItemSalesChartForSingleItem();
            }
        }


        private ObservableCollection<Beverage> _Items;
        public ObservableCollection<Beverage> Beverages
        {
            get { return _Items; }
            set
            {
                SetProperty<ObservableCollection<Beverage>>(ref _Items, value);
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

        ObservableCollection<Sale> _OrderDetails;
        public ObservableCollection<Sale> OrderDetails
        {
            get
            {
                return _OrderDetails;
            }
            set
            {
                if (_OrderDetails != value)
                {
                    _OrderDetails = value;
                    notify("OrderDetails");
                }
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
            { StringFormat = "dd/MM/yy", Title = "Sales Date", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new OxyPlot.Axes.LinearAxis()
            {
                AxisDistance = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Unit Sold"
            };
            PlotModel.Axes.Add(valueAxis);
        }
    }
}

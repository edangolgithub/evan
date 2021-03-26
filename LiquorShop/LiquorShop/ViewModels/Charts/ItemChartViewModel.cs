using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rms.Data;
using System.Collections.ObjectModel;
using LiquorShopService.Services;
using System.Windows.Controls.DataVisualization.Charting;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using System.Windows.Controls;
using TescoWineShopSql;
using WpfApp1;

namespace Rms.ViewModels.Charts
{
    class ItemChartViewModel : ViewModelBase
    {


        private ObservableCollection<Beverage> _Items;
        public ObservableCollection<Beverage> Items
        {
            get { return _Items; }
            set
            {
                SetProperty<ObservableCollection<Beverage>>(ref _Items, value);
            }
        }

        ObservableCollection<KeyValuePair<string, int>> _ItemsChart;
        public ObservableCollection<KeyValuePair<string, int>> ItemsChart
        {
            get
            {
                return _ItemsChart;
            }
            set
            {
                if (_ItemsChart != value)
                {
                    _ItemsChart = value;
                    notify("ItemsChart");
                }
            }
        }
        IBeverageService Service;
        public ItemChartViewModel(IBeverageService Service)
        {
            //PlotModel = new PlotModel { Title = "hello" };
            //this.PlotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            this.Service = Service;


        }

        private void GetItemAndPriceChart()
        {

            ItemsChart = new ObservableCollection<KeyValuePair<string, int>>(Items
            .GroupBy(s => s.Name)
            .Select(s => new { name = s.Key, count = s.Sum(a => a.Price) })
            .OrderByDescending(s => s.count).AsEnumerable()
            .Take(20)
            .Select(a => new KeyValuePair<string, int>((string)a.name, (int)a.count)));
        }
        public void GetOxyChartForItemAndPriceold(PlotView view)
        {
            var model = new PlotModel { Title = "Cake Type Popularity" };

            //generate a random percentage distribution between the 5
            //cake-types (see axis below)
            var rand = new Random();
            double[] cakePopularity = new double[5];
            for (int i = 0; i < 5; ++i)
            {
                cakePopularity[i] = rand.NextDouble();
            }
            var sum = cakePopularity.Sum();

            var barSeries = new OxyPlot.Series.BarSeries
            {
                ItemsSource = new List<BarItem>(new[]
                    {
                new BarItem{ Value = (cakePopularity[0] / sum * 100) },
                new BarItem{ Value = (cakePopularity[1] / sum * 100) },
                new BarItem{ Value = (cakePopularity[2] / sum * 100) },
                new BarItem{ Value = (cakePopularity[3] / sum * 100) },
                new BarItem{ Value = (cakePopularity[4] / sum * 100) }
        }),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:.00}%"
            };
            model.Series.Add(barSeries);

            model.Axes.Add(new OxyPlot.Axes.CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "CakeAxis",
                ItemsSource = new[]
                    {
                "Apple cake",
                "Baumkuchen",
                "Bundt Cake",
                "Chocolate cake",
                "Carrot cake"
        }
            });
            view.Model = model;
        }
        public void GetOxyChartForItemAndPrice(PlotView view)
        {
            
            var model = new PlotModel { Title = "Item And Price" };

            //generate a random percentage distribution between the 5
            //cake-types (see axis below)

            List<BarItem> baritems = new List<BarItem>();
            foreach (var item in Items)
            {
                BarItem bitem = new BarItem { Value =(double) item.Price };
                baritems.Add(bitem);
            }
            var barSeries = new OxyPlot.Series.BarSeries
            {
                ItemsSource = baritems,               
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:C}"
            };
            model.Series.Add(barSeries);

            model.Axes.Add(new OxyPlot.Axes.CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "CakeAxis",
                ItemsSource = Items.Select(a=>a.Name)
            });
            view.Model = model;
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
                Items = new ObservableCollection<Beverage>(await Service.GetAllBeveragesForChartAsync());

            }).ContinueWith(a =>
            {
                GetItemAndPriceChart();

            }).ContinueWith(a =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {

                    UserControl uc = obj as UserControl;
                    Chart ch=  uc.FindName("w") as Chart;
                    GetOxyChartForItemAndPrice(uc.FindName("oxy") as PlotView);
                    ch.DataContext = ItemsChart;
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
    }
}

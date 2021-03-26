using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Rms.Data;
using Rms.Classes;
using OxyPlot;
using LiquorShopService.Services;
using OxyPlot.Wpf;
using System.Windows;
using TescoWineShopSql;
using WpfApp1;

namespace Rms.ViewModels.Charts
{
   public class IncomeChartViewModel:ViewModelBase
    {
        IAccountingService Service;
        public IncomeChartViewModel(IAccountingService Service)
        {
            this.Service = Service;
        }

        private ObservableCollection<LedgerAccount> _LedgerAccounts;
        public ObservableCollection<LedgerAccount> LedgerAccounts
        {
            get { return _LedgerAccounts; }
            set
            {
                SetProperty(ref _LedgerAccounts, value);
            }
        }


        private ObservableCollection<LedgerGeneral> _LedgerGenerals;
        public ObservableCollection<LedgerGeneral> LedgerGenerals
        {
            get { return _LedgerGenerals; }
            set
            {
                SetProperty<ObservableCollection<LedgerGeneral>>(ref _LedgerGenerals, value);
            }
        }
        private ObservableCollection<RevenueVm> _Revenues;
        public ObservableCollection<RevenueVm> Revenues
        {
            get { return _Revenues; }
            set
            {
                SetProperty<ObservableCollection<RevenueVm>>(ref _Revenues, value);
            }
        }
        public decimal TotalRevenueBalance { get; set; }
        public decimal TotalExpenseBalance { get; set; }
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
        public void GetIncomeChart()
        {
            Revenues = new ObservableCollection<Rms.Classes.RevenueVm>();
          
            try
            {
                TotalExpenseBalance = 0;
                TotalRevenueBalance = 0;
                var lineSerie = new OxyPlot.Series.LineSeries
                {
                    ToolTip = "Revenue",
                   // LabelFormatString = "Revenue",
                    StrokeThickness = 1,
                    MarkerSize = 3,
                    Color = OxyColors.Green,
                    MarkerFill= OxyColors.Green,
                    MarkerStroke = OxyColors.Green,
                    MarkerType = MarkerType.Diamond,// markerTypes[data.Key],
                    CanTrackerInterpolatePoints = false,
                    Title ="Revenue"
                };
                var ExpenselineSerie = new OxyPlot.Series.LineSeries
                {
                    ToolTip = "Expense",
                   // LabelFormatString = "Expense",
                    StrokeThickness = 1,
                    MarkerSize = 3,
                    Color = OxyColors.Red,
                    MarkerFill = OxyColors.Red,
                    MarkerStroke = OxyColors.Red,
                    MarkerType = MarkerType.Diamond,// markerTypes[data.Key],
                    CanTrackerInterpolatePoints = false,
                    Title = "Expense"
                };
                var NetIncomelineSerie = new OxyPlot.Series.LineSeries
                {
                    ToolTip = "Net Income",
                    // LabelFormatString = "Net Income",
                    StrokeThickness = 1,
                    MarkerSize = 3,
                    Color = OxyColors.Blue,
                    MarkerFill = OxyColors.Blue,
                    MarkerStroke = OxyColors.Blue,
                    MarkerType = MarkerType.Diamond,// markerTypes[data.Key],
                    CanTrackerInterpolatePoints = false,
                    Title = "Net Income"
                };
                for (int i = 1; i <=DateTime.Today.Month+1; i++)
                {
                    
                    int year = DateTime.Today.Year;
                    DateTime interval = DateTime.Parse(string.Format("{0}-{1}-{2}", i, 1, year));
                    interval = interval.AddDays(-1);
                    decimal Revenuebalance = 0;
                    decimal ExpenseBalance = 0;
                    foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 3))
                    {
                        decimal debitbalance, creditbalance;
                        GetDebitCreditTotal(item,interval, out debitbalance, out creditbalance);
                        var v = GetRevenues(item, debitbalance, creditbalance);
                      
                        if (v != null)
                        {
                            Revenuebalance += v.Amount;
                            // Revenues.Add(v);
                        }
                        
                    }

                    foreach (var item in LedgerAccounts.Where(a => a.AccountClassId == 2))
                    {
                        decimal debitbalance, creditbalance;
                        GetDebitCreditTotal(item, interval, out debitbalance, out creditbalance);
                        var v = GetExpenses(item, debitbalance, creditbalance);

                        if (v != null)
                        {
                            ExpenseBalance += v.Amount;
                          
                        }

                    }
                   
                    lineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(interval),(double)Revenuebalance));
                    ExpenselineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(interval), (double)ExpenseBalance));
                    NetIncomelineSerie.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(interval), (double)Revenuebalance-(double)ExpenseBalance));

                }


                PlotModel.Series.Add(lineSerie);
                PlotModel.Series.Add(ExpenselineSerie);
                PlotModel.Series.Add(NetIncomelineSerie);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ExpenseVm GetExpenses(LedgerAccount LedgerAccount, decimal debitbalance, decimal creditbalance)
        {
            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            ExpenseVm evm = new ExpenseVm();
            evm.LedgerAccountName = LedgerAccount.AccountName;
            balance = debitbalance - creditbalance;
            evm.Amount = balance;
            TotalExpenseBalance += balance;
            return evm;
        }

        private RevenueVm GetRevenues(LedgerAccount LedgerAccount, decimal debitbalance, decimal creditbalance)
        {

            if (debitbalance == 0 && creditbalance == 0)
            {
                return null;
            }
            decimal balance = 0;
            RevenueVm rvm = new RevenueVm();
            rvm.LedgerAccountName = LedgerAccount.AccountName;
            balance = creditbalance - debitbalance;
            rvm.Amount = balance;
            TotalRevenueBalance += balance;
            return rvm;
        }
        private void GetDebitCreditTotal(LedgerAccount LedgerAccount,DateTime interval, out decimal debitbalance, out decimal creditbalance)
        {
            debitbalance = LedgerGenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                          .Where(a => a.JournalEntryDate.Date <= interval.Date)
                          .Sum(b => b.Debit);
            creditbalance = LedgerGenerals.Where(b => b.LedgerAccountId == LedgerAccount.LedgerAccountId)
                .Where(a => a.JournalEntryDate.Date <= interval.Date)
               .Sum(b => b.Credit);
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
            { StringFormat = "MMM/dd/yyyy", Title = "Date",  MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot
            , IntervalType=OxyPlot.Axes.DateTimeIntervalType.Months, MinorIntervalType=OxyPlot.Axes.DateTimeIntervalType.Days,
                IntervalLength = 80 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new OxyPlot.Axes.LinearAxis()
            {
                AxisDistance = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Amount in Rupee"
            };
            PlotModel.Axes.Add(valueAxis);
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

        public async void InitLoaded(object obj)
        {
            IsDataLoaded = false;
            IsDataNotLoaded = true;


            await Task.Run(async () =>
             {
                 LedgerAccounts = new ObservableCollection<LedgerAccount>(await Service.GetAllLedgerAccountsAsync());
                 LedgerGenerals = new ObservableCollection<LedgerGeneral>(await Service.GetAllLedgerGeneralsAsync());
                
             }).ContinueWith(a =>
             {
                 App.Current.Dispatcher.Invoke(() =>
                 {
                     PlotModel = new PlotModel();
                     SetUpModelNew();
                     GetIncomeChart();
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
    }
}

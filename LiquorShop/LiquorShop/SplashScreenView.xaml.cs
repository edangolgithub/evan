using LiquorShop.Classes;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Rms.Views
{
    /// <summary>
    /// Interaction logic for SplashScreenView.xaml
    /// </summary>
    public partial class SplashScreenView : MetroWindow, INotifyPropertyChanged
    {
        private RmsDashBoard m_mainWindow;

        private DispatcherTimer splashAnimationTimer;

        private const string Loading = "Loading";
        private ObservableCollection<CompanyJson> _CompanyJsons;
        public ObservableCollection<CompanyJson> CompanyJsons
        {
            get
            {
                return _CompanyJsons;
            }
            set
            {
                if (_CompanyJsons != value)
                {
                    _CompanyJsons = value;
                    notify("CompanyJsons");
                }
            }
        }

        public SplashScreenView()
        {
            InitializeComponent();
            DataContext = this;
            IsValid = false;
            IsNotValid = false;
            string f = System.IO.Path.GetFileName("Companies.json");
            var companies = JsonConvert.DeserializeObject<IEnumerable<CompanyJson>>(File.ReadAllText(f));

            CompanyJsons = new ObservableCollection<CompanyJson>(companies);

            //    CompanyJsons = new ObservableCollection<CompanyJson>(new List<CompanyJson>
            //    {
            //    new CompanyJson{Company="a",Password="a"},
            //     new CompanyJson{Company="b",Password="a"},
            //      new CompanyJson{Company="c",Password="a"},
            //       new CompanyJson{Company="d",Password="a"},
            //        new CompanyJson{Company="e",Password="a"}
            //    });
            //   var data= JsonConvert.SerializeObject(CompanyJsons);
            //    File.WriteAllText("Companies.json", data);
        }

        private int index = 0;
        private int length = 0;
        private void splashAnimationTimer_Tick(object sender, EventArgs e)
        {



            int dotsCount = lblProgress.Content.ToString().Replace(Loading, string.Empty).Length;

            if (dotsCount < 6)
            {
                dotsCount++;
            }
            else
            {
                dotsCount = 0;
            }

            lblProgress.Content = Loading.PadRight(Loading.Length + dotsCount, '.');

            if (length >= index)
            {
                dlllabel.Content = Files[index];
                index++;
            }
        }

        private void m_mainWindow_ReadyToShow(object sender, EventArgs args)
        {

            #region Animate the splash screen fading 

            Storyboard sb = new Storyboard();
            // 
            DoubleAnimation da = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            // 
            Storyboard.SetTarget(da, this);
            Storyboard.SetTargetProperty(da, new PropertyPath(OpacityProperty));
            sb.Children.Add(da);
            // 
            sb.Completed += new EventHandler(sb_Completed);
            // 
            sb.Begin();

            #endregion //  splash screen fading With Animation. 
        }

        private void sb_Completed(object sender, EventArgs e)
        {

            // When the splash screen fades out, we can show the main window..... 
            if (IsValid)
            {
                m_mainWindow.Visibility = System.Windows.Visibility.Visible;
                Close();
            }
        }

        private void m_mainWindow_Closed(object sender, EventArgs e)
        {

            //Close();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify(string p)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(p));
            }
        }

        private string _Pass;
        public string Pass
        {
            get
            {
                return _Pass;
            }
            set
            {
                if (_Pass != value)
                {
                    _Pass = value;
                    notify("Pass");
                }
            }
        }

        private CompanyJson _SelectedCompanyJson;
        public CompanyJson SelectedCompanyJson
        {
            get
            {
                return _SelectedCompanyJson;
            }
            set
            {
                if (_SelectedCompanyJson != value)
                {
                    _SelectedCompanyJson = value;
                    notify("SelectedCompanyJson");
                }
            }
        }

        public List<string> Files { get; set; }
        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            var today = DateTime.Today;

            if (today.Year > 2017)
            {
                Popup w = new Popup();
                StackPanel sp = new StackPanel();

                Label l = new Label();
                l.Content = "this software doesnt function any more\n contact software maker \n or enter serial key";
                l.Foreground = Brushes.Red; 
                    TextBox t = new TextBox();
                t.Foreground = Brushes.RoyalBlue;
                Button b = new Button();
                b.Content = "ok";
                b.Click += (s, er) =>
                {
                    if (t.Text != "continue")
                    {
                        w.IsOpen = false;
                        MessageBox.Show("Wrong Serial Key.\nApplication Will Terminate Now");
                      
                        Environment.Exit(Environment.ExitCode);
                    }
                    else
                    {
                        w.IsOpen = false;
                        Process();
                    }
                };
                sp.Children.Add(l);
                sp.Children.Add(t);
                sp.Children.Add(b);
                w.Child = sp;
                w.PlacementTarget = pop;
                w.Placement = PlacementMode.Bottom;
                w.IsOpen = true;
            }
            // var companies = JsonConvert.DeserializeObject<List<CompanyJson>>(File.ReadAllText("companies.json"));
          
        }

        private void Process()
        {
            var ok = CompanyJsons.Where(a => a.Company == SelectedCompanyJson.Company && a.Password == EvanDangol.Cryptography.CryptoEngine.Encrypt(passwordbox.Text)).FirstOrDefault();
            if (ok != null)
            {
                //Task.Run(() =>
                //{
                IsValid = true;
                IsNotValid = false;
                splashAnimationTimer = new DispatcherTimer();
                splashAnimationTimer.Interval = TimeSpan.FromMilliseconds(500);
                splashAnimationTimer.Tick += new EventHandler(splashAnimationTimer_Tick);
                splashAnimationTimer.Start();


                Files = new List<string>();
                Files.Add("Initializing Wine Shop..");
                Files.Add("Resolving EvanDangol.dll");
                Files.Add("Resolving EvanDangol.dll");
                Files.Add("Resolving EvanDangol.dll");
                Files.Add("Resolving EvanDangol.dll");
                var folder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                foreach (var item in System.IO.Directory.GetFiles(folder).Where(a => a.EndsWith("dll")))
                {
                    string filename = System.IO.Path.GetFileName(item);
                    Files.Add(filename);

                }
                length = Files.Count - 1;
                //});


                m_mainWindow = new RmsDashBoard();
                m_mainWindow.ReadyToShow += new RmsDashBoard.ReadyToShowDelegate(m_mainWindow_ReadyToShow);
                m_mainWindow.Closed += new EventHandler(m_mainWindow_Closed);
            }
            else
            {
                IsValid = false;
                IsNotValid = true;
            }
        }

        private bool _IsValid;
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                if (_IsValid != value)
                {
                    _IsValid = value;
                    notify("IsValid");
                }
            }
        }

        private bool _IsNotValid;
        public bool IsNotValid
        {
            get
            {
                return _IsNotValid;
            }
            set
            {
                if (_IsNotValid != value)
                {
                    _IsNotValid = value;
                    notify("IsNotValid");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}

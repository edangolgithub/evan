using MahApps.Metro.Controls;
using Rms.InfraStructures;
using Rms.ViewModels;
//using Rms.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using Unity;
using WpfApp1;

namespace Rms.Views
{
    /// <summary>
    /// Interaction logic for m.xaml
    /// </summary>
    public partial class RmsDashBoard : MetroWindow
    {
        public delegate void ReadyToShowDelegate(object sender, EventArgs args);

        public event ReadyToShowDelegate ReadyToShow;

        private DispatcherTimer timer;

        RmsDashBoardViewModel me;
       // private IntPtr _windowHandle;
        public RmsDashBoard()
        {
            InitializeComponent();
            App.Current.MainWindow = this;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(7);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            try
            {
                Dispatcher.Invoke(() =>
                {
                    me = ContainerHelper.Container.Resolve<RmsDashBoardViewModel>();
                    DataContext = me;
                });

            }
            catch(Exception ex)
            {
                throw ex;
            }
          //  this.SourceInitialized += MainWindow_SourceInitialized;
        

    }
        void timer_Tick(object sender, EventArgs e)
        {

            timer.Stop();

            if (ReadyToShow != null)
            {
                ReadyToShow(this, null);
            }
        }
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }


       

        private void TileMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (((RmsDashBoardViewModel)DataContext).IsUserLoggedIn == true)
            //    return;
            //MessageBox.Show("You Dont Have Access ");
            //username.Focus();
            //FocusManager.SetFocusedElement(this, username);
            //Dispatcher.BeginInvoke((Action)(() =>
            //{
            //   username.Focus();
            //})
            //);
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}

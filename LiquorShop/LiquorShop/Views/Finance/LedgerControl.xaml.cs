﻿using Rms.InfraStructures;
using Rms.ViewModels.Accounts;
using Rms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace Rms.UserControls.Accounts
{
    /// <summary>
    /// Interaction logic for LedgerControl.xaml
    ///// </summary>
    public partial class LedgerControl : UserControl
    {
      
        public LedgerControl()
        {
            InitializeComponent();
           
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
        }
    }
}

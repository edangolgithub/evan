using Rms.InfraStructures;
using Rms.ViewModels.Accounts;
using System.Windows;
using System.Windows.Controls;
using Unity;
namespace Rms.UserControls.Accounts
{
    /// <summary>
    /// Interaction logic for JournalEntryListControl.xaml
    /// </summary>
    public partial class JournalEntryListControl : UserControl
    {
      
        public JournalEntryListControl()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //EvanDangol.Reporting.WpfDataGridToExcel tt = new EvanDangol.Reporting.WpfDataGridToExcel();
            // tt.btnExport_Click(JournalListGrid);

           
        }
    }
}

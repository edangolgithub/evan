using Rms.Classes;
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

namespace Rms.UserControls.Accounts
{
    /// <summary>
    /// Interaction logic for BalanceSheetControl.xaml
    /// </summary>
    public partial class BalanceSheetControl : UserControl
    {
        public BalanceSheetControl()
        {
            InitializeComponent();
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<BalanceSheetExport> statements = new List<BalanceSheetExport>();
            statements.Add(new BalanceSheetExport { AccountType = "ASSETS" });
            foreach (AssetsVm item in d2.Items)
            {
                statements.Add(new BalanceSheetExport { Account = item.LedgerAccountName, Balance = item.Amount.ToString() });
            }
            foreach (AssetsVm item in d3.Items)
            {
                statements.Add(new BalanceSheetExport { TotalHeading = item.LedgerAccountName, Total = item.Amount.ToString() });
            }
            statements.Add(new BalanceSheetExport { AccountType = "LIABILITIES" });
            foreach (LiabilitiesVm item in d5.Items)
            {
                statements.Add(new BalanceSheetExport { Account = item.LedgerAccountName, Balance = item.Amount.ToString() });
            }
            foreach (LiabilitiesVm item in d6.Items)
            {
                statements.Add(new BalanceSheetExport { TotalHeading = item.LedgerAccountName, Total = item.Amount.ToString() });
            }
             statements.Add(new BalanceSheetExport { AccountType = "EQUITY" });
            foreach (EquityVm item in d8.Items)
            {
                statements.Add(new BalanceSheetExport { Account = item.LedgerAccountName, Balance = item.Amount.ToString() });
            }
            foreach (EquityVm item in d9.Items)
            {
                statements.Add(new BalanceSheetExport { TotalHeading = item.LedgerAccountName, Total = item.Amount.ToString() });
            }
            statements.Add(new BalanceSheetExport { AccountType = "LIABILITIES AND EQUITY" });
            foreach (LiabilitiesAndEquity item in d11.Items)
            {
                statements.Add(new BalanceSheetExport { TotalHeading = "Total", Total = item.Amount.ToString() });
            }
            EvanDangol.Reporting.ExportToExcel<BalanceSheetExport, List<BalanceSheetExport>> report = new EvanDangol.Reporting.ExportToExcel<BalanceSheetExport, List<BalanceSheetExport>>();
            report.dataToPrint = statements;
            report.GenerateReport("Balance Sheet","Balance Sheet");
        }
    }
    public class BalanceSheetExport
    {
        public string AccountType { get; set; }
        public string Account { get; set; }
        public string Balance { get; set; }
        public string TotalHeading { get; set; }
        public string Total { get; set; }
        public string Date { get; set; }
    }
}

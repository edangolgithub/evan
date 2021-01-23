using Rms.Classes;
using Rms.ViewModels.Accounts;
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
    /// Interaction logic for IncomeStatementControl.xaml
    /// </summary>
    public partial class IncomeStatementControl : UserControl
    {
        public IncomeStatementControl()
        {
            InitializeComponent();
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<IncomeStatementExport> statements = new List<IncomeStatementExport>();
            statements.Add(new Accounts.IncomeStatementExport { AccountType = "REVENUE" });
            foreach (RevenueVm item in d2.Items)
            {
                statements.Add(new Accounts.IncomeStatementExport { Account = item.LedgerAccountName, Balance = item.Amount.ToString() });
            }
            foreach (RevenueVm item in d3.Items)
            {
                statements.Add(new Accounts.IncomeStatementExport { TotalHeading = item.LedgerAccountName, Total = item.Amount.ToString() });
            }
            statements.Add(new Accounts.IncomeStatementExport { AccountType = "EXPENSE" });
            foreach (ExpenseVm item in d5.Items)
            {
                statements.Add(new Accounts.IncomeStatementExport { Account = item.LedgerAccountName, Balance = item.Amount.ToString() });
            }
            foreach (ExpenseVm item in d6.Items)
            {
                statements.Add(new Accounts.IncomeStatementExport { TotalHeading = item.LedgerAccountName, Total = item.Amount.ToString() });
            }
          //  statements.Add(new Accounts.IncomeStatementExport { column1 = "Net Income" });
            foreach (NetIncome item in d8.Items)
            {
                statements.Add(new Accounts.IncomeStatementExport { TotalHeading = "Net Income", Total = item.Amount.ToString() });
            }

            EvanDangol.Reporting.ExportToExcel<IncomeStatementExport, List<IncomeStatementExport>> report = new EvanDangol.Reporting.ExportToExcel<IncomeStatementExport, List<IncomeStatementExport>>();
            report.dataToPrint = statements;
            report.GenerateReport("Income Statement as of "+ ((FinanceViewModel)DataContext).IncomeStatementDate.ToLongDateString(),"Income Statement");
        }
       
    }
    public class IncomeStatementExport
    {
        public string AccountType { get; set; }
        public string Account { get; set; }
        public string Balance { get; set; }
        public string TotalHeading { get; set; }
        public string Total { get; set; }
        public string Date { get; set; }
    }
}

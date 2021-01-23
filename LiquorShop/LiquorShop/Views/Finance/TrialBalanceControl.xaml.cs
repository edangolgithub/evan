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
    /// Interaction logic for TrialBalanceControl.xaml
    /// </summary>
    public partial class TrialBalanceControl : UserControl
    {
        public TrialBalanceControl()
        {
            InitializeComponent();
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<TrialBalanceExport> statements = new List<TrialBalanceExport>();
            // statements.Add(new TrialBalanceExport { column1 = "Revenues" });
            foreach (TrialBalanceVm item in d1.Items)
            {
                statements.Add(new TrialBalanceExport
                {
                    Account = item.LedgerAccountName,
                    Debit = item.DebitSide == 0 ? "" : item.DebitSide.ToString(),
                    Credit = item.CreditSide == 0 ? "" : item.CreditSide.ToString()
                });
            }
            foreach (TrialBalanceVm item in d2.Items)
            {
                statements.Add(new TrialBalanceExport
                {
                    Account = item.LedgerAccountName,
                    Debit = item.DebitSide == 0 ? "" : item.DebitSide.ToString(),
                    Credit = item.CreditSide == 0 ? "" : item.CreditSide.ToString()
                });
            }

            if (statements.Count < 1)
            {
                MessageBox.Show("Get Trial Balance First");
                return;
            }
            EvanDangol.Reporting.ExportToExcel<TrialBalanceExport, List<TrialBalanceExport>> report = new EvanDangol.Reporting.ExportToExcel<TrialBalanceExport, List<TrialBalanceExport>>();
            report.dataToPrint = statements;
            report.GenerateReport("Trial Balance as of " +((FinanceViewModel)DataContext).FromTrialBalanceDate.ToLongDateString(),"Trial Balance");
        }
    }
    public class TrialBalanceExport
    {
        public string Account { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }

    }
}

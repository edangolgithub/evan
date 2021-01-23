using LiquorShop.Classes;
using Rms.Data;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using WpfApp1;

namespace Rms.Views.Restaurant
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : Window
    {

        private SaleVm vm;

        public InvoiceView()
        {
            InitializeComponent();
        }


        public InvoiceView(SaleVm vm) : this()
        {
            this.vm = vm;
            DataContext = this;
            invoicetext.Text = "Invoice Number :" + vm.SaleOrderId.ToString();
            usertxt.Text = "User :" + ((App)Application.Current).CurrentUser.UserName;
            date.Text = vm.SaleDate.ToShortDateString();
            cn.Text = ((App)Application.Current).SelectedCompany.CompanyName;
            ca.Text = ((App)Application.Current).SelectedCompany.CompanyAddress;
            cpn.Text = ((App)Application.Current).SelectedCompany.CompanyPanNumber;
            cp.Text = ((App)Application.Current).SelectedCompany.CompanyPhone;
            cc.Text = ((App)Application.Current).SelectedCompany.CompanyCity;
            PrintInvoice();
        }

        public void PrintInvoice()
        {
            // Create the Table...
            Table table1 = new Table();

            Table paymenttable = new Table();
            // ...and add it to the FlowDocument Blocks collection.
            document.Blocks.Add(table1);
            var separator = new Rectangle();
            separator.Stroke = new SolidColorBrush(Colors.Gray);
            separator.StrokeThickness = 1;
            separator.Height = 1;
            separator.Width = double.NaN;

            var lineBlock = new BlockUIContainer(separator);
            document.Blocks.Add(lineBlock);


            document.Blocks.Add(paymenttable);
            var separator1 = new Rectangle();
            separator1.Stroke = new SolidColorBrush(Colors.Gray);
            separator1.StrokeThickness = 1;
            separator1.Height = 1;
            separator1.Width = double.NaN;

            var lineBlock1 = new BlockUIContainer(separator1);
            document.Blocks.Add(lineBlock1);

            Table paymenttable1 = new Table();

            document.Blocks.Add(paymenttable1);
            // Set some global formatting properties for the table.
            table1.CellSpacing = 2;
            table1.Background = Brushes.White;

            paymenttable.CellSpacing = 2;
            paymenttable.Background = Brushes.White;


            table1.RowGroups.Add(new TableRowGroup());


            table1.RowGroups[0].Rows.Add(new TableRow());


            TableRow currentRow = table1.RowGroups[0].Rows[0];


            currentRow.FontWeight = System.Windows.FontWeights.Bold;
            currentRow.Foreground = Brushes.DimGray;
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Beverages"))) { ColumnSpan = 2 });
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[1];

            //// Global formatting for the header row.
            ////currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Light;
            currentRow.Foreground = Brushes.MediumSpringGreen;

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Particulars"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Qty"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Price"))));

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Amount"))));

            TableRowGroup rowGroup = new TableRowGroup();
            int count = 1;
            foreach (var item in vm.BevarageVms)
            {

                var itemRow = new TableRow();
                itemRow.FontWeight = FontWeights.ExtraLight;
                itemRow.Foreground = Brushes.DimGray;

                itemRow.Cells.Add(new TableCell(new Paragraph(new Run(count.ToString() + ")" + item.Name.Substring(0,10)))));// { ColumnSpan = 3, Padding = new Thickness(5) });
                itemRow.Cells.Add(new TableCell(new Paragraph(new Run(item.Quantity.ToString())))); // { Padding = new Thickness(5) });

                itemRow.Cells.Add(new TableCell(new Paragraph(new Run((item.Price??0).ToString("C")))));// { ColumnSpan = 2, Padding = new Thickness(5) });

                itemRow.Cells.Add(new TableCell(new Paragraph(new Run((item.Total??0).ToString("C")))));// { Padding = new Thickness(5) });

                rowGroup.Rows.Add(itemRow);
                count++;
            }
            table1.RowGroups.Add(rowGroup);

            TableRowGroup PaymentRows = new TableRowGroup();
            PaymentRows.Foreground = Brushes.Gray;
            paymenttable.RowGroups.Add(PaymentRows);





            //var ServiceChargeRow = new TableRow();
            //ServiceChargeRow.Cells.Add(new TableCell(new Paragraph(new Run("ServiceCharge"))));
            //ServiceChargeRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.ServiceCharge.ToString("C"))))
            //{ Padding = new Thickness(4), Foreground = Brushes.DarkGray });
            //PaymentRows.Rows.Add(ServiceChargeRow);



           

            var TaxableRow = new TableRow();
            TaxableRow.Cells.Add(new TableCell(new Paragraph(new Run("Total Amount"))));
            TaxableRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.TotalAmount.ToString("C"))))
            { Padding = new Thickness(4), Foreground = Brushes.DarkGray });
            PaymentRows.Rows.Add(TaxableRow);

            if (vm.Discount > 0)
            {
                var DiscountRow = new TableRow();
                DiscountRow.Cells.Add(new TableCell(new Paragraph(new Run("Discount"))));
                DiscountRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.Discount.ToString("C"))))
                { Padding = new Thickness(4), Foreground = Brushes.DarkGray });
                PaymentRows.Rows.Add(DiscountRow);
            }

            if (vm.TotalAmount != vm.AmountAfterDiscount)
            {
                var TotalAmountRow = new TableRow();
                TotalAmountRow.Cells.Add(new TableCell(new Paragraph(new Run("Sub Total"))));
                TotalAmountRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.AmountAfterDiscount.ToString("C"))))
                { Padding = new Thickness(4), Foreground = Brushes.Black });
                PaymentRows.Rows.Add(TotalAmountRow);
            }

           


            TableRowGroup PaymentRows1 = new TableRowGroup();
            PaymentRows1.Foreground = Brushes.DarkGray;
            paymenttable1.RowGroups.Add(PaymentRows1);
            var CurrentRow = new TableRow();
            CurrentRow.Cells.Add(new TableCell(new Paragraph(new Run("AmountPaid"))) { Padding = new Thickness(2) });
            CurrentRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.AmountPaid.ToString("C"))))
            { Padding = new Thickness(4), Foreground = Brushes.DarkMagenta });
            PaymentRows1.Rows.Add(CurrentRow);

            if (vm.Due > 0)
            {
                var TotalDueRow = new TableRow();
                TotalDueRow.Cells.Add(new TableCell(new Paragraph(new Run("Due"))));
                TotalDueRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.Due.ToString("C"))))
                { Padding = new Thickness(4), Foreground = Brushes.Black });
                PaymentRows.Rows.Add(TotalDueRow);
            }
            if (vm.Change > 0)
            {
                var ChangeRow = new TableRow();
                ChangeRow.Cells.Add(new TableCell(new Paragraph(new Run("Change"))) { Padding = new Thickness(2) });
                ChangeRow.Cells.Add(new TableCell(new Paragraph(new Run(vm.Change.ToString("C"))))
                { Padding = new Thickness(4), Foreground = Brushes.DarkMagenta });
                PaymentRows1.Rows.Add(ChangeRow);
            }


        }

      
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = System.IO.Path.GetRandomFileName();
            using (XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                writer.Write(view);
            }
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                int margin = 5;
                Size pageSize = new Size(dlg.PrintableAreaWidth - margin * 2, dlg.PrintableAreaHeight - margin * 2);
                IDocumentPaginatorSource paginator = document as IDocumentPaginatorSource;
                paginator.DocumentPaginator.PageSize = pageSize;
                dlg.PrintDocument(paginator.DocumentPaginator, "Print output");
            }
        }
    }
}

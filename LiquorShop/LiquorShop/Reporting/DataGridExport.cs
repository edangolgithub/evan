
using System.Windows.Controls;
using XL = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace EvanDangol.Reporting
{
  public  class WpfDataGridToExcel
    {
        private XL._Application appExcel = null;
        private XL.Workbook wbk = null;
        private XL.Worksheet sheet = null;


        public void btnExport_Click(DataGrid Grille)
        {
            appExcel = new XL.Application();
            wbk = appExcel.Workbooks.Add();
            sheet = wbk.Worksheets.Add();
            appExcel.Visible = true;
            int colExcel = 1;
            int rowExcel = 1;
            XL.Range rng = null;
            // Headers  Colonnes
            foreach (DataGridColumn ob in Grille.Columns)
            {
                rng = this.sheet.Cells[rowExcel, colExcel];
                rng.Value = ob.Header.ToString();
                colExcel++;
            }
            //Contenu Colonnes
            //Contenu caste -vers TextBlock - car nous utilisons DataGridTextColumn 
            rowExcel++;
            for (int i = 0; i < Grille.Items.Count; i++)
            {
                DataGridRow dRow = (DataGridRow)Grille.ItemContainerGenerator.ContainerFromIndex(i);
                if (dRow == null) continue;
                for (int j = 0; j < Grille.Columns.Count; j++)
                {
                    colExcel = j + 1;
                    DataGridColumn dCol = Grille.Columns[j];
                    rng = sheet.Cells[rowExcel, colExcel];
                    rng.Value = ((TextBlock)dCol.GetCellContent(dRow)).Text;

                }
                rowExcel++;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";

            saveFileDialog.FilterIndex = 0;

            saveFileDialog.RestoreDirectory = true;

            saveFileDialog.CreatePrompt = true;

            saveFileDialog.Title = "Export Excel File To";


                wbk.SaveCopyAs(saveFileDialog.FileName);
            

            if (this.appExcel != null)
            {
                if (this.sheet != null) Marshal.ReleaseComObject(this.sheet);
                if (this.wbk != null) Marshal.ReleaseComObject(this.wbk);
                this.appExcel.Quit();
                Marshal.ReleaseComObject(this.appExcel);

            }
        }
    }
}
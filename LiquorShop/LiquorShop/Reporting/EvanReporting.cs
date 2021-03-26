using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
//using GemBox.Spreadsheet;
namespace EvanDangol.Reporting
{
    //-----------------------------------------------------------------------------------------------------
    public class EvanReporting
    {

        public static void WpfExportDatagridToExcel(System.Windows.Controls.DataGrid dataGrid)
        {

        }

        //-----------------------------------------------------------------------------------------------------
        public static void ExportDatagridToExcel(DataGridView dataGridView1)
        {
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = (Excel._Worksheet)workbook.Sheets["Sheet1"];
            worksheet = (Excel._Worksheet)workbook.ActiveSheet;
            // changing the name of active sheet
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.FileName = "sms " + DateTime.Now.ToLongDateString();
            saveFileDialog1.Filter = "Text files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // save the application
                workbook.SaveAs(saveFileDialog1.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            // Exit from the application
            app.Quit();
        }
        public static DataTable Readexcelfile()
        {
            string filename = getexcelfile();
            string sheetname = Interaction.InputBox("Please Enter The Excel Sheet Name", "Sheet To Show", "Sheet1");
            string constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='Excel 12.0;HDR=yes'";

            OleDbConnection conn = new OleDbConnection(constring);
            try
            {
                
                conn.Open();
                //MessageBox.Show(conn.State.ToString());
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand cmd = new OleDbCommand("select * from [" + sheetname + "$]", conn);
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                //DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //MessageBox.Show(Sheets.TableName[1].ToString());
                //DataTable objSheetNames = conn.GetSchema("Tables");
                //MessageBox.Show(objSheetNames.TableName[1].ToString());
                DataTable sheetTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });

                DataTable dt = new DataTable();
                adapter.Fill(dt);
              
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public static string getexcelfile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Excel Files |*.xlsx";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                return fd.FileName;
            }
            else
            {
                return null;
            }
        }

    }
    //-----------------------------------------------------------------------------------------------------
    public static class EvanReportingExtended
    {
        //-----------------------------------------------------------------------------------------------------
        public static void ExportDatagridToExcel(this DataGridView dataGridView1)
        {
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = (Excel._Worksheet)workbook.Sheets["Sheet1"];
            worksheet = (Excel._Worksheet)workbook.ActiveSheet;
            // changing the name of active sheet
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.FileName = "sms " + DateTime.Now.ToLongDateString();
            saveFileDialog1.Filter = "Text files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // save the application
                workbook.SaveAs(saveFileDialog1.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            // Exit from the application
            app.Quit();
        }

        public static DataTable ReadExcelFile(this DataGridView dg)
        {





            try
            {
                string filename = getexcelfile();
                Excel.Application excelApp = new Excel.Application();
                // excelApp.Visible = true;

                Excel.Workbook workbook = excelApp.Workbooks.Open(filename,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                string sheetnames = "";
                List<string> sheetlist = new List<string>();
                foreach (Excel.Worksheet item in workbook.Worksheets)
                {
                    sheetnames += item.Name + "\n";
                    sheetlist.Add(item.Name);
                }
                //choosesheet f = new choosesheet(sheetlist);
                //f.ShowDialog();

                string sheetname = Interaction.InputBox("Please Enter The Excel Sheet Name\nSheet Names are\n" + sheetnames, "Sheet To Show", "9abc");
                if (sheetname.Length < 1)
                {
                    return null;
                }
                string constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='Excel 12.0;HDR=yes'";
                OleDbConnection conn = new OleDbConnection(constring);
                conn.Open();
                //MessageBox.Show(conn.State.ToString());
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                OleDbCommand cmd = new OleDbCommand("select * from [" + sheetname + "$]", conn);
                cmd.Connection = conn;
                adapter.SelectCommand = cmd;
                //DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //MessageBox.Show(Sheets.TableName[1].ToString());
                //DataTable objSheetNames = conn.GetSchema("Tables");
                //MessageBox.Show(objSheetNames.TableName[1].ToString());
                DataTable sheetTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dg != null)
                {
                    dg.DataSource = dt;
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string getexcelfile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Excel Files |*.xlsx";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                return fd.FileName;
            }
            else
            {
                return null;
            }
        }

        public class choosesheet : Form
        {
            System.Windows.Forms.ComboBox cb = new System.Windows.Forms.ComboBox();
            public choosesheet(List<string> sheets)
            {

                cb.SelectedIndexChanged += cb_SelectedIndexChanged;
                cb.Items.AddRange(sheets.ToArray());
                this.Controls.Add(cb);
            }
            public string filename { get; set; }
            void cb_SelectedIndexChanged(object sender, EventArgs e)
            {
                filename = cb.SelectedItem.ToString();
            }

            public string choose()
            {

                return filename;
            }
        }

    }
}



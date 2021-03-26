using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Odbc;
using System.Data.SqlClient;
//using DevExpress.XtraEditors;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
namespace EvanDangol.Data.Sql
{
   public class DataBackup
    {
        SqlConnection conn;
        public SqlConnection openconn()
        {
            #region
            string CS = @"Data Source=.\sqlexpress;Initial Catalog=master;Integrated Security=True";
            //string CS = "DSN=raideit; UID=sa; Pwd=1234";
            conn = new SqlConnection(CS);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    return conn;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Failed to Connect with Database.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return conn;
            #endregion
        }
        SqlCommand cmd;
        SqlDataReader dr;
        public List<string> GetDatabases()
        {
          DataBackup dbc = new DataBackup();
            List<string> databases = new List<string>();
            // select * from sys.databases getting all database name from sql server
            cmd = new SqlCommand("select * from sys.databases", dbc.openconn());
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                databases.Add(dr[0].ToString());
            }
            dr.Close();
            return databases;
        }
        public List<string> GetserverNames()
        {
            try
            {
                List<string> servers = new List<string>();
               DataBackup dbc = new DataBackup();
                // select *  from sys.servers getting server names that exist
                cmd = new SqlCommand("select *  from sys.servers", dbc.openconn());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    servers.Add(dr[1].ToString());
                }
                dr.Close();
                return servers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void query(string que)
        {
            DataBackup dbc = new DataBackup();
            cmd = new SqlCommand(que, dbc.openconn());
            cmd.ExecuteNonQuery();
        }
        public void newquery(string que)
        {
           DataBackup dbc = new DataBackup();
            cmd = new SqlCommand(que, dbc.openconn());
            cmd.CommandTimeout = 50;
            cmd.ExecuteNonQuery();
        }
        // Backup Database
        public void DoBackup(bool Backupyes, string servername, string databasename)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(servername) | string.IsNullOrEmpty(databasename))
                {
                    MessageBox.Show("Server Name & Database can not be Blank", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (Backupyes)
                    {
                        saveFileDialog1.FileName = "library " + DateTime.Now.ToLongDateString();
                        saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            // the below query get backup of database you specified in combobox
                            query("Backup database " + databasename + " to disk='" + saveFileDialog1.FileName + "'");
                            MessageBox.Show("Database BackUp has been created successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Report this to Fix it.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
            }
        }
        // Restore database
        public void Restore(SqlConnection sqlcon, string DatabaseFullPath, string backUpPath)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (sqlcon)
                {
                    string UseMaster = "USE master";
                    SqlCommand UseMasterCommand = new SqlCommand(UseMaster, sqlcon);
                    UseMasterCommand.ExecuteNonQuery();
                    // The below query will rollback any transaction which is running on that database and brings SQL Server database in a single user mode.
                    string Alter1 = @"ALTER DATABASE [" + DatabaseFullPath + "] SET Single_User WITH Rollback Immediate";
                    SqlCommand Alter1Cmd = new SqlCommand(Alter1, sqlcon);
                    Alter1Cmd.ExecuteNonQuery();
                    // The below query will restore database file from disk where backup was taken ....
                    string Restore = @"RESTORE DATABASE [" + DatabaseFullPath + "] FROM DISK = N'" + backUpPath + @"' WITH  FILE = 1,  NOUNLOAD,  STATS = 10,REPLACE";
                    SqlCommand RestoreCmd = new SqlCommand(Restore, sqlcon);
                    RestoreCmd.ExecuteNonQuery();
                    // the below query change the database back to multiuser
                    string Alter2 = @"ALTER DATABASE [" + DatabaseFullPath + "] SET Multi_User";
                    SqlCommand Alter2Cmd = new SqlCommand(Alter2, sqlcon);
                    Alter2Cmd.ExecuteNonQuery();
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                string Alter2 = @"ALTER DATABASE [" + DatabaseFullPath + "] SET Multi_User";
                SqlCommand Alter2Cmd = new SqlCommand(Alter2, sqlcon);
                Alter2Cmd.ExecuteNonQuery();
                MessageBox.Show(ex.Message, "Report this to Fix it.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cursor.Current = Cursors.Default;
            }
        }
        public void DoRestore(string servername, string databasename)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (string.IsNullOrEmpty(servername) | string.IsNullOrEmpty(databasename))
            {
                MessageBox.Show("Server Name & Database can not be Blank", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                DataBackup dbc = new DataBackup();
                Restore(dbc.openconn(), databasename, openFileDialog1.FileName);
                MessageBox.Show("Database Backup file has been restored successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

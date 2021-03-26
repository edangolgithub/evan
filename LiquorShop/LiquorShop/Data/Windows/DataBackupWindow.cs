using EvanDangol.Data.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvanDangol.Data
{
    public partial class DataBackupWindow : Form
    {
        DataBackup db;
        private string databasename;
        public DataBackupWindow()
        {
            InitializeComponent();
            db = new DataBackup();
           
        }

        public DataBackupWindow(string databasename)
        {
            // TODO: Complete member initialization
            this.databasename = databasename;
            InitializeComponent();
            db = new DataBackup();
        }

        private void DataBackupWindow_Load(object sender, EventArgs e)
        {
            ServercomboBox.Items.AddRange(db.GetserverNames().ToArray());
            ServercomboBox.SelectedIndex = 0;
            if (databasename != string.Empty)
            {
                DatabasecomboBox.Text = databasename;
            }
        }

        private void ServercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           DatabasecomboBox.Items.AddRange(db.GetDatabases().ToArray());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            db.DoBackup(true, ServercomboBox.Text, DatabasecomboBox.Text);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            db.DoRestore(ServercomboBox.Text, DatabasecomboBox.Text);
        }
    }
}

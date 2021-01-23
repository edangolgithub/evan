using EvanDangol.Core;
using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
namespace EvanDangol.Data.Common
{
    public class EvanDbConnection
    {
        IDbConnection con;
        public void GetProvider()
        {
            this.dataProvString = ConfigurationManager.ConnectionStrings["cnstr"].ProviderName;
            this.cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
        }
        public EvanDbConnection()
        {
            GetProvider();
            InitializeConnection();
            GetConnection();
        }
        private void InitializeConnection()
        {
            this.con = InitializeConnection((EvanDataProvider)Enum.Parse(typeof(EvanDataProvider), dataProvString));
        }
        public IDbConnection GetConnection()
        {
            if (con != null)
            {
                return this.con;
            }
            else
            {
                con = null;
                throw new EvanException("Cannot Connect to server");
            }
        }
        private IDbConnection InitializeConnection(EvanDataProvider dp)
        {
            switch (dp)
            {
                case EvanDataProvider.SqlClient:
                case EvanDataProvider.SqlServer:
                case EvanDataProvider.SqlExpress:
                    con = new SqlConnection();
                    break;
                case EvanDataProvider.OleDb:
                    con = new OleDbConnection();
                    break;
                case EvanDataProvider.Odbc:
                    con = new OdbcConnection();
                    break;
                //case EvanDataProvider.SqlServerCe:
                //    con = new SqlCeConnection();
                //    break;
                case EvanDataProvider.MySql:
                    //con = new MySqlConnection();
                    break;
                case EvanDataProvider.Access12:
                    con = new OleDbConnection();
                    break;
            }
            this.con.ConnectionString = this.cnStr;
            return this.con;
        }
        public void Entry()
        {
            EvanDataProvider provider = EvanDataProvider.None;
            this.GetProvider();
            provider = (EvanDataProvider)Enum.Parse(typeof(EvanDataProvider), dataProvString);
            IDbConnection con = InitializeConnection(provider);
            con.ConnectionString = cnStr;
            con.Open();
            IDbCommand cmd = con.CreateCommand();
            string sql = "select * from Student";
            cmd.CommandText = sql;
            IDataReader id = cmd.ExecuteReader();
            while (id.Read())
            {
                Console.WriteLine(id.GetString(2));
            }
            con.Close();
        }
        public string dataProvString { get; set; }
        public string cnStr { get; set; }
    }
}

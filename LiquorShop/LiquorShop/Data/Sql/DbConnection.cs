using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace EvanDangol.Data.Sql
{
    public abstract class DbConnection
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        public DbConnection()
        {
            myAdapter = new SqlDataAdapter();
            AppDomain.CurrentDomain.SetData("DataDirectory",System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory+"DataAccess\\Data\\"));
            
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        }
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter = null)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                {
                    myCommand.Parameters.AddRange(sqlParameter);
                }
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }
            return dataTable;
        }
        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;// new Exception("Error - Connection.executeInsertQuery - Query: " + _query + " \nException: \n" + e.StackTrace.ToString());
                
            }
            finally
            {
            }
            return true;
        }
        public bool executeStoredProcedure(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = openConnection();
                cmd.CommandText = _query;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParameter);
                //myAdapter.InsertCommand = cmd;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }
            return true;
        }
        public bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
            }
            return true;
        }
    }
}

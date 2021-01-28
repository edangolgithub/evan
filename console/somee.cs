using System;
using System.Data.SqlClient;

namespace console
{
    class Data
    {
       public static void getconnection()
        {
            
            Console.WriteLine("Getting Connection ...");

            var datasource = @"DESKTOP-PC\SQLEXPRESS";//your server
            var database = "Students"; //your database name
            var username = "sa"; //username of server to connect
            var password = "password"; //password

            //your connection string 
            string connString = @"workstation id=wineshop.mssql.somee.com;packet size=4096;user id=edangol_SQLLogin_1;pwd=qpnpz5zqqj;data source=wineshop.mssql.somee.com;persist security info=False;initial catalog=wineshop";

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);
           

            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.Read();
        
        }
    }
}

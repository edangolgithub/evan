using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EvanDangol.Data.Sql
{
    public static class SqlServerConnection
    {

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
        public static bool CheckSqlExpressConnection()
        {

            string myServiceName = "MSSQL$SQLEXPRESS"; //service name of SQL Server Express
            string status; //service status (For example, Running or Stopped)

            Console.WriteLine("Service: " + myServiceName);

            //display service status: For example, Running, Stopped, or Paused
            ServiceController mySC = new ServiceController(myServiceName);

            try
            {
                status = mySC.Status.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                Console.ReadLine();

                throw ex;

            }

            //display service status: For example, Running, Stopped, or Paused
            Console.WriteLine("Service status : " + status);

            //if service is Stopped or StopPending, you can run it with the following code.
            if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {


                    Console.WriteLine("Starting the service...");
                    mySC.Start();
                    mySC.WaitForStatus(ServiceControllerStatus.Running);
                    Console.WriteLine("The service is now " + mySC.Status.ToString());
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message == "Access is denied")
                    {
                        throw ex.InnerException;
                    }
                    throw ex;
                }

            }

            return true;


        }

        public static bool CheckSqlServerConnection()
        {

            string myServiceName = "MSSQL$SQLSERVER"; //service name of SQL Server Express
            string status; //service status (For example, Running or Stopped)

            Console.WriteLine("Service: " + myServiceName);

            //display service status: For example, Running, Stopped, or Paused
            ServiceController mySC = new ServiceController(myServiceName);

            try
            {
                status = mySC.Status.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service not found. It is probably not installed. [exception=" + ex.Message + "]");
                Console.ReadLine();

                throw ex;

            }

            //display service status: For example, Running, Stopped, or Paused
            Console.WriteLine("Service status : " + status);

            //if service is Stopped or StopPending, you can run it with the following code.
            if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
            {
                try
                {


                    Console.WriteLine("Starting the service...");
                    mySC.Start();
                    mySC.WaitForStatus(ServiceControllerStatus.Running);
                    Console.WriteLine("The service is now " + mySC.Status.ToString());
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message == "Access is denied")
                    {
                        throw ex.InnerException;
                    }
                    throw ex;
                }

            }

            return true;


        }
        public static bool CheckDatabaseExists(string serverName, string databaseName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=true", @serverName, @databaseName));
                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Message.IndexOf("Error Locating Server/Instance Specified") > -1)
                {
                    throw ex;
                }
                else if (ex.Message.IndexOf(String.Format("Cannot open database \"{0}\"", databaseName)) > -1)
                {
                    return false;
                }
            }
            return false;
            
        }
    }
}

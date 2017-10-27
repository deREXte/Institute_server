using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Server.BDDataSetTableAdapters;
using System.Threading;

namespace Server
{
    static class DataBaseOperations
    {

        private static Mutex mut = new Mutex();
        private static SqlConnection Connection = new SqlConnection();

        public static void INIT()
        {
            SqlConnectionStringBuilder connect =
               new SqlConnectionStringBuilder();
            connect.InitialCatalog = "BD";
            connect.DataSource = @"(local)\SQLEXPRESS";
            connect.ConnectTimeout = 30;
            connect.IntegratedSecurity = true;
            Connection.ConnectionString = connect.ConnectionString;
            Connection.Open();
        }

        public static string GetUserPassword(string userName)
        {          
            SqlCommand command = new SqlCommand("SELECT * FROM [Users] WHERE UserName = @UserName", Connection);
            command.Parameters.AddWithValue("@UserName", userName);
            SqlDataReader sqlReader = null;
            try
            {
                Execute(ref sqlReader, command);
                if (!sqlReader.HasRows)
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            sqlReader.Read();
            return Convert.ToString(sqlReader["Password"]);
        }

        private static void Execute(ref SqlDataReader sqlReader,SqlCommand command)
        {
            mut.WaitOne();
            sqlReader = command.ExecuteReader();
            mut.ReleaseMutex();
        }

    }
}

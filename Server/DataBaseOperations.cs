using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Threading;
using System.Collections;

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
                execute(ref sqlReader, command);
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

        public static IEnumerator Execute(string command)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Users]", Connection);
            SqlDataReader sqlReader = null;
            try
            {
                execute(ref sqlReader, cmd);
                if (!sqlReader.HasRows)
                    return null;
            }catch(Exception)
            {
                return null;
            }
            return sqlReader.GetEnumerator();
        }

        private static void execute(ref SqlDataReader sqlReader,SqlCommand command)
        {
            mut.WaitOne();
            sqlReader = command.ExecuteReader();
            mut.ReleaseMutex();
        }

    }
}

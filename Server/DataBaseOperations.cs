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

        private static bool _connected;

        public static bool Connected
        {
            get { return _connected; }
        }
        

        public static void InitConnectionToDB()
        {
            SqlConnectionStringBuilder connect =
               new SqlConnectionStringBuilder();
            connect.InitialCatalog = "BD";
            connect.DataSource = @"(local)\SQLEXPRESS";
            connect.ConnectTimeout = 30;
            connect.IntegratedSecurity = true;
            connect.UserID = "abcd";
            //connect.Password = "1";
            Connection.ConnectionString = connect.ConnectionString;
            Connection.Open();
            SqlCommand sql = new SqlCommand("INSERT INTO [Users] (UserName, Privilege, Password) VALUES ('user','User','1')", Connection);
            sql.ExecuteNonQuery();
            _connected = true;
        }

        //изменить
        public static bool CheckUserLoginData(string userName, string password)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Users] WHERE UserName = @UserName AND Password = @Password", Connection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            SqlDataReader sqlReader = null;
            executeReader(ref sqlReader, command);
            return sqlReader.HasRows;
        }

        private static void executeReader(ref SqlDataReader sqlReader,SqlCommand command)
        {
            mut.WaitOne();
            sqlReader = command.ExecuteReader();
            mut.ReleaseMutex();
        }

    }
}

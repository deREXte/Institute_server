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
        private static SqlDataReader SqlReader;

        private static bool _connected;

        public static bool Connected
        {
            get { return _connected; }
        }
        

        public static void InitConnectionToDB()
        {
            SqlConnectionStringBuilder connect = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "BD",
                DataSource = @"(local)\SQLEXPRESS",
                ConnectTimeout = 30,
                IntegratedSecurity = true
            };
            Connection.ConnectionString = connect.ConnectionString;
            Connection.Open();
            _connected = true;
        }

        public static bool CheckUserLoginData(string userName, string password)
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM [Users] WHERE UserName " +
                "= @UserName AND Password = @Password"
                , Connection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            using (SqlDataReader sqlReader = command.ExecuteReader())
            {
                return sqlReader.HasRows;
            }
        }

        public static bool ChangeUserLogin(string newLogin, string oldLogin)
        {
            SqlCommand command = new SqlCommand(
                "UPDATE [Users] SET UserName = @UserName WHERE UserName = @oldLogin"
                , Connection);
            command.Parameters.AddWithValue("@UserName", newLogin);
            command.Parameters.AddWithValue("@oldLogin", oldLogin);
            try
            {
                command.ExecuteNonQuery();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool ChangeUserPassword(string text, string login)
        {
            SqlCommand command = new SqlCommand(
                "UPDATE [Users] SET Password = @Password WHERE UserName = @Login"
                , Connection);
            command.Parameters.AddWithValue("@Password", text);
            command.Parameters.AddWithValue("@Login", login);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool CreateUserProfile(string name, string password)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO [Users] (UserName,Password,Privilage) VALUES (@UserName, @Password, @Privilage)",
                Connection);
            command.Parameters.AddWithValue("@UserName", name);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Privilage", "User");
            try
            {
                command.ExecuteNonQuery();
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteProfile(string login)
        {
            SqlCommand command = new SqlCommand(
                "DELETE FROM [Users] WHERE UserName = @UserName"
                , Connection);
            command.Parameters.AddWithValue("@UserName", login);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static string ExecuteSqlReader(string text)
        {
            SqlCommand command = new SqlCommand(text, Connection);
            StringBuilder buffer = new StringBuilder();
            mut.WaitOne();
            using (var sqlReader = command.ExecuteReader())
            {
                while (sqlReader.Read())
                {
                    for (int i = 0; i < sqlReader.FieldCount; i++)
                    {
                        buffer.Append(sqlReader.GetString(i) + "|");
                    }
                    buffer.Append("\n");
                }
            }
            mut.ReleaseMutex();
            buffer.Append("\0");
            return buffer.ToString();
        } 

        public static void ExecuteNonSqlReader(string text)
        {
            SqlCommand command = new SqlCommand(text, Connection);
            try
            {
                command.ExecuteNonQuery();
            }catch(Exception e)
            {

            }
        }
    }
}

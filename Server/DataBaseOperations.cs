using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Threading;
using System.Collections;
using ServerClientClassLibrary;
using System.Data;

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

        public static bool CreateUserProfile(string name, string password)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO [Users] (UserName,Password,Privilege) VALUES (@UserName, @Password, @Privilege)",
                Connection);
            command.Parameters.AddWithValue("@UserName", name);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Privilege", "User");
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
                "UPDATE Users SET Hidden=0 WHERE login = @UserName"
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

        public static object locker = new object();

        public static SelectJson ExecuteSqlReader(string text)
        {
            SqlCommand command = new SqlCommand(text, Connection);
            SelectJson selectjson = new SelectJson()
            {
                Rows = new List<RowJson>(),
                ColumnName = new List<string>()
            };

            lock (locker)
                using (var sqlReader = command.ExecuteReader())
                {
                    var columns = sqlReader.FieldCount;
                    for (int i = 0; i < columns; i++)
                    {
                        selectjson.ColumnName.Add(sqlReader.GetName(i));
                    }
                    while (sqlReader.Read())
                    {
                        RowJson row = new RowJson()
                        {
                            Row = new List<string>()
                        };
                        for (int i = 0; i < columns; i++)
                        {
                            var col = sqlReader.GetSchemaTable();
                            //DataSet set = new DataSet();
                            //SqlDataAdapter sqladapter = new SqlDataAdapter();
                            //foreach(DataRelation c in col)
                            //    c.DataSet.WriteXml(Console.Out);
                            row.Row.Add(sqlReader.GetValue(i).ToString());
                        }
                        selectjson.Rows.Add(row);
                    }
                }
            return selectjson;
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

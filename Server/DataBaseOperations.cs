using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Threading;
using System.Collections;
using ServerClientClassLibrary.JSONTypes;
using System.Data;
using System.IO;
using System.Xml;

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
                "UPDATE Users SET Hidden=1 WHERE login=@UserName"
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

        public static DataTableJson ExecuteDataTable(string command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command, Connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            StringBuilder xmlString = new StringBuilder();
            var xmlWriter = XmlWriter.Create(xmlString);
            dataTable.WriteXml(xmlWriter);
            DataTableJson dataTableJson = new DataTableJson()
            {
                Code = ServerClientClassLibrary.Code.OperationCode.AnswerOK,
                DataTable = xmlString.ToString(),
            };
            return dataTableJson;
        }

        public static SelectJson ExecuteSqlReader(string text)
        {
            SqlCommand command = new SqlCommand(text, Connection);
            SelectJson selectjson = new SelectJson()
            {
                Rows = new List<RowJson>(),
                TableSchema = new TableSchema(),
            };
            lock (locker)
                using (var sqlReader = command.ExecuteReader())
                {
                    var columns = sqlReader.FieldCount;
                    for (int i = 0; i < columns; i++)
                    {
                        selectjson.TableSchema.Columns.Add(new Column()
                        {
                            Name = sqlReader.GetName(i),
                            TypeName = sqlReader.GetDataTypeName(i),
                        });
                    }
                    while (sqlReader.Read())
                    {
                        RowJson row = new RowJson();
                        for (int i = 0; i < columns; i++)
                        {
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

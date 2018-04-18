using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ServerClientClassLibrary;
using Newtonsoft.Json;

namespace Server
{
    class Operations
    {
        IODialog Handler;
        Log ServerLog, UserLog;

        public string UserName
        {
            get { return UserLog.UserName; }
        } 

        public Operations(Log serverLog, IODialog dialog)
        {
            ServerLog = serverLog;
            Handler = dialog;
        }

        public bool CheckConnection()
        {
            return Handler.ReceiveMessage(out Code.OperationCode code) == Handler.PassPhrase;
        }

        public void ExecuteCommand(string command, Code.OperationCode code)
        {
            Execute(command, code);
        }

        private void Execute(string command, Code.OperationCode code)
        {
            switch (code)
            {
                case Code.OperationCode.UPDATE:
                    UserLog.Write("Update command: " + command);
                    DataBaseOperations.ExecuteNonSqlReader(command);
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.SELECT:
                    UserLog.Write("Select command: " + command);
                    string buf = DataBaseOperations.ExecuteSqlReader(command);
                    Handler.SendMessage(buf, Code.OperationCode.AnswerOK);
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.INSERT:
                    UserLog.Write("Insert command: " + command);
                    DataBaseOperations.ExecuteNonSqlReader(command);
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.DELETE:
                    UserLog.Write("Delete command: " + command);
                    DataBaseOperations.ExecuteNonSqlReader(command);
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.ConnectionRefuse:
                    UserLog.Write(UserLog.UserName + " disconnected!");
                    UserLog.CloseLogFile();
                    Thread.CurrentThread.Abort();
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.Login:
                    {
                        string userName, password;
                        userName = SupportClass.SubEnv(command, "UserName", ';');
                        password = SupportClass.SubEnv(command, "Password", ';');
                        if (DataBaseOperations.CheckUserLoginData(userName, password))
                        {
                            try
                            {
                                UserLog = new Log(userName);
                                UserLog.Write(UserName + " Connected!");
                                Handler.SendMessage(Code.OperationCode.AnswerOK);
                            }catch(Exception e)
                            {
                                ServerLog.Write("Error: " + e.Message);
                                Thread.CurrentThread.Abort();
                            }
                        }
                        else
                        {
                            Handler.SendMessage(Code.OperationCode.AnswerError);
                        }
                    } 
                    break;
                /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.InitConnection:
                    {

                    }
                    break;
                case Code.OperationCode.GenerateUserData:
                    Random rand = new Random(DateTime.Now.Millisecond);
                    string stringrandom = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890";
                    int len = stringrandom.Length;
                    int count = int.Parse(command);
                    GenUserDataJson users = new GenUserDataJson();
                    string log, pass;
                    for (int i = 0; i < count; i++)
                    {
                        log = ""; pass = "";
                        for (int j = 0; j < 10; j++)
                            log += stringrandom[rand.Next(0, len)];
                        for(int j = 0; j < 10; j++)
                            pass += stringrandom[rand.Next(0, len)];
                        users.Users.Add(new UserData { Login = log, Password = pass });
                        DataBaseOperations.CreateUserProfile(log, pass);
                    }
                    Handler.SendMessage(JsonConvert.SerializeObject(users), (byte)Code.OperationCode.AnswerOK);
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
                case Code.OperationCode.DeleteUsers:
                    UserLog.Write("DeleteUsers command: " + command);
                    try
                    {
                        string[] logins = command.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var l in logins)
                            DataBaseOperations.ExecuteNonSqlReader(l);
                    }catch(Exception e)
                    {
                        Handler.SendMessage(Code.OperationCode.AnswerError);
                    }
                    Handler.SendMessage(Code.OperationCode.AnswerOK);
                    break;
                    /////////////////////////////////////////////////////////////////////////////////
            }
        }
    }
}

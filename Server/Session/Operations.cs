using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ServerClientClassLibrary;


namespace Server
{
    class Operations
    {
        IODialog Handler;
        Log ServerLog, UserLog;
        IODialog IODialogWithClient;
        delegate void Operation(Operations op, string command);
        static Dictionary<Code.OperationCode, Operation> OperationFuncs = new Dictionary<Code.OperationCode, Operation>()
        {
            [Code.OperationCode.SELECT] = (op,command) => {
                op.UserLog.Write("Select command: " + command);
                var buffer = DataBaseOperations.ExecuteSqlReader(command);
                op.Handler.SendMessage(buffer, (byte)Code.OperationCode.AnswerOK);
            },
            [Code.OperationCode.INSERT] = (op, command) => {
                op.UserLog.Write("Insert command: " + command);
                DataBaseOperations.ExecuteNonSqlReader(command);
                op.Handler.SendMessage("", (byte)Code.OperationCode.AnswerOK);
            },
    };

        static Operations()
        {
            
        }
       
       

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
            return Handler.ReceiveMessage(out byte code) == Handler.PassPhrase;
        }

        public void ExecuteCommand(string command, int code)
        {
            Execute(command, code / 10);
        }

        private void Execute(string command, int code)
        {
            switch ((Code.OperationPurpose_1)(code % 10))
            {
                case Code.OperationPurpose_1.DataBase:
                    ExecuteDBOperation_10(command, code / 10);
                    break;
                case Code.OperationPurpose_1.Server:
                    ExecuteServerOperation(command, code / 10);
                    break;
            }
        }

        private void ExecuteDBOperation_10(string command, int code)
        {
            switch ((Code.DBOperation_10)(code % 10))
            {
                case Code.DBOperation_10.UserData:
                    ExecuteUserDataOperation(command, code);
                    break;
                case Code.DBOperation_10.Update:
                    UserLog.Write("Update command: " + command);
                    DataBaseOperations.ExecuteNonSqlReader(command);
                    break;
                case Code.DBOperation_10.Select:
                    
                    break;
                case Code.DBOperation_10.Insert:
                    
                    break;
                case Code.DBOperation_10.Delete:
                    UserLog.Write("Delete command: " + command);
                    DataBaseOperations.ExecuteNonSqlReader(command);
                    break;
            }
        }

        private void ExecuteServerOperation(string command, int code)
        {
            switch ((Code.ServerOperation_10)(code % 10))
            {
                case Code.ServerOperation_10.ConnectionRefused:
                    UserLog.Write(UserLog.UserName + " disconnected!");
                    UserLog.CloseLogFile();
                    Thread.CurrentThread.Abort();
                    break;
            }
        }

        private void ExecuteUserDataOperation(string command, int code)
        {
            switch ((Code.UserDataOperation_100)(code % 10))
            {
                case Code.UserDataOperation_100.ChangeLogin:
                    {
                        var userName = SupportClass.SubEnv(command, "UserName", ';');
                        var password = SupportClass.SubEnv(command, "Password", ';');
                        var newUserName = SupportClass.SubEnv(command, "NewUserName", ';');
                        if (DataBaseOperations.CheckUserLoginData(userName, password))
                        {
                            try
                            {
                                if (DataBaseOperations.ChangeUserLogin(newUserName, userName))
                                {
                                    Handler.SendMessage((byte)Code.OperationPurpose_1.Answer + ((int)Code.Answer_10.Success * 10));
                                    UserLog.Write(UserLog.UserName + "changed his login from \"" + userName + "\" + to \"" + newUserName + "\"!");
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    break;
                case Code.UserDataOperation_100.ChangePassword:
                    {
                        var userName = SupportClass.SubEnv(command, "UserName", ';');
                        var password = SupportClass.SubEnv(command, "Password", ';');
                        var newUserPassword = SupportClass.SubEnv(command, "NewUserPassword", ';');
                        if (DataBaseOperations.CheckUserLoginData(userName, password))
                        {
                            try
                            {
                                if (DataBaseOperations.ChangeUserPassword(newUserPassword, userName))
                                {
                                    Handler.SendMessage((byte)Code.OperationPurpose_1.Answer + ((int)Code.Answer_10.Success * 10));
                                    UserLog.Write(UserLog.UserName + "changed his password from \"" + userName + "\" + to \"" + newUserPassword + "\"!");
                                }
                            }
                            catch (Exception)
                            {

                case Code.UserDataOperation_100.Auto:
                    {
                        string userName, password, buffer;
                        while (true)
                        {
                            buffer = Handler.ReceiveMessage(out byte bufcode);
                            userName = SupportClass.SubEnv(buffer, "UserName", ';');
                            password = SupportClass.SubEnv(buffer, "Password", ';');
                            if (DataBaseOperations.CheckUserLoginData(userName, password))
                            {
                                UserLog = new Log(userName);
                                Handler.SendMessage((byte)Code.OperationPurpose_1.Answer + ((int)Code.Answer_10.Success * 10));
                            }
                            else
                            {
                            }
                        }
                    }
            }
        }
    }
}

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

        public void ExecuteCommand(string command,int code)
        {
            Execute(command, code/10);
        }

        private void Execute(string command, int code)
        {
            switch ((Code.OperationPurpose)(code%10))
            {
                case Code.OperationPurpose.DataBase:
                    ExecuteDBOperation(command, code/10);
                    break;
                case Code.OperationPurpose.Server:
                    ExecuteServerOperation(command, code/10);
                    break;
            }
        }

        private void ExecuteDBOperation(string command, int code)
        {
            switch((Code.DBOperation)(code%10))
            {
                case Code.DBOperation.UserData:
                    break;
                case Code.DBOperation.Update:
                    break;
                case Code.DBOperation.Select:
                    break;
                case Code.DBOperation.Insert:
                    break;
                case Code.DBOperation.Delete:
                    break;
            }
        }
         
        private void ExecuteServerOperation(string command, int code)
        {
            switch((Code.ServerOperation)(code%10))
            {
                case Code.ServerOperation.ConnectionRefused:
                    Thread.CurrentThread.Abort(); 
                    break;
            }
        }

        private void ExecuteDataUserOperation(string command, int code)
        {
            switch((Code.UserDataOperation)(code % 10))
            {
                case Code.UserDataOperation.ChangeLogin:
                    break;
                case Code.UserDataOperation.ChangePassword:
                    break;
                case Code.UserDataOperation.DeleteProfile:
                    break;
            }
        }
    }
}

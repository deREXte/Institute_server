using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
        public enum OperationCode 
        {
            /*0 - 19: Answers*/
            AnswerOK = 0,
            AnswerError = 1,
            AnswerAccessDenied = 2,
            ConnectionRefuse = 3,

            /*20 - 39: Requests*/
            SELECT = 20,
            INSERT = 21,
            UPDATE = 22,
            DELETE = 23,
            GetTableList = 24,

            /*40 - 59: UserData*/
            Login = 40,
            GenerateUserData = 41,
            CreateUsers = 42,
            DeleteUsers = 43,

            InitConnection = 60,
            UserAuthorized = 70,
            AdminAuthorized = 71,
    }
}

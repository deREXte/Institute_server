using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.UserSession;
using ServerClientClassLibrary.JSONTypes;
using ServerClientClassLibrary;

namespace Server.NewOperations.Operations
{
    class LoginOperation : AOperations
    {

        const string WRONG_LOGIN_OR_PASS = "Что-то не так! Вероятно, неправильно указан логин или пароль";

        public LoginOperation(Rights rights) : base(rights)
        {
        }

        protected override void ApplyCommand(Session session, QueryJson query)
        {
            string userName, password;
            userName = GetVariable(query.Message, "UserName"); 
            password = GetVariable(query.Message, "Password");
            string userPriv = DataBaseOperations.CheckUserLoginData(userName, password);
            OperationCode code = OperationCode.UserAuthorized;
            if (userPriv == "Admin")
            {
                session.UserRights = Rights.Admin;
                code = OperationCode.AdminAuthorized;
            }
            else if (userPriv == "User")
            {
                session.UserRights = Rights.User;
                code = OperationCode.UserAuthorized; 
            }
            else
            {
                session.Dialog.SendMessage(
                    new Msg(OperationCode.AnswerError, WRONG_LOGIN_OR_PASS));
            }
            session.CreateLog(userName);
            session.WriteLog(userName + " connected!");
            session.Dialog.SendMessage(
                new Msg(code, "Connected"));
        }
    }
}

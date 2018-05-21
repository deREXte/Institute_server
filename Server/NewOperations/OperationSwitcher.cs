using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.NewOperations.Operations;
using ServerClientClassLibrary;
using Server.UserSession;
using ServerClientClassLibrary.JSONTypes;
using Server.DataTableInfo;

namespace Server.NewOperations
{
    class OperationSwitcher
    {
        
        public static readonly Dictionary<OperationCode, AOperations> Operations = new Dictionary<OperationCode, AOperations>()
        {
            { OperationCode.SELECT,             new SelectOperation        (Rights.Admin | Rights.User | Rights.Other) },
            { OperationCode.INSERT,             new InsertOperation        (Rights.Admin | Rights.User) },
            { OperationCode.UPDATE,             new UpdateOperation        (Rights.Admin | Rights.User) },
            { OperationCode.DELETE,             new DeleteOperation        (Rights.Admin) },
            { OperationCode.GenerateUserData,   new GenerateUsersOperation (Rights.Admin) },
            { OperationCode.DeleteUsers,        new DeleteUsersOperation   (Rights.Admin) },
            { OperationCode.ConnectionRefuse,   new ConnectionRefuse       (Rights.Admin | Rights.User | Rights.Other) },
            { OperationCode.Login,              new LoginOperation         (Rights.Other) },
        };

        public static void Execute(Session userSession, QueryJson query)
        {
            if (Operations[query.Code].CheckRights(userSession.UserRights))
                Operations[query.Code].Execute(userSession, query);
            else
                userSession.Dialog.SendMessage(OperationCode.AnswerAccessDenied);
        }
    }
}

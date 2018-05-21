using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.UserSession;
using ServerClientClassLibrary.JSONTypes;

namespace Server.NewOperations.Operations
{
    class DeleteUsersOperation : AOperations
    {
        public DeleteUsersOperation(Rights rights) : base(rights)
        {
        }

        protected override void ApplyCommand(Session session, QueryJson query)
        {
            string[] logins = GetVariable(query.Message, "Logins").
                Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var login in logins)
                DataBaseOperations.DeleteProfile(login);
        }
    }
}

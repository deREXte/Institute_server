using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.UserSession;
using ServerClientClassLibrary.JSONTypes;

namespace Server.NewOperations.Operations
{
    class UpdateOperation : AOperations
    {
        public UpdateOperation(Rights rights) : base(rights)
        {
        }

        protected override void ApplyCommand(Session session, QueryJson query)
        {
            session.WriteLog("Update command: " + query.Message);
            DataBaseOperations.ExecuteNonSqlReader(query.Message);
        }
    }
}

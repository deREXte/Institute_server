using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Server.UserSession;
using ServerClientClassLibrary.JSONTypes;

namespace Server.NewOperations.Operations
{
    class ConnectionRefuse : AOperations
    {
        public ConnectionRefuse(Rights rights) : base(rights)
        {
        }

        protected override void WrapperApplyCommand(Session session, QueryJson query)
        {
            ApplyCommand(session, query);
        }

        protected override void ApplyCommand(Session session, QueryJson query)
        {
            session.Close();
            Thread.CurrentThread.Abort();
        }
    }
}

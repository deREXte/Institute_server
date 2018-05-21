using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary;
using ServerClientClassLibrary.JSONTypes;
using Server.UserSession;
using Server.DataTableInfo;

namespace Server.NewOperations.Operations
{
    class SelectOperation : AOperations
    {
        public SelectOperation(Rights rights) : base(rights)
        {
        }

        protected override void ApplyCommand(Session session, QueryJson query)
        {
            session.WriteLog("Select command: " + query.Message);
            DataTableJson buf = DataBaseOperations.ExecuteDataTable(query);
            buf.Dependence = DataTableDependeces.GetTableDependence(query.TableName);
            session.Dialog.SendMessage(buf);
        }
    }
}

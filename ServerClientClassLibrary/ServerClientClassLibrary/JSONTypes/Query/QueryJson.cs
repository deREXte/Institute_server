using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary.JSONTypes
{
    public class QueryJson : Msg
    {
        public string TableName;

        public QueryJson() { }

        public QueryJson(OperationCode code, string msg) : base(code, msg) { }

        public QueryJson(OperationCode code, string msg, string tableName) : base(code, msg)
        {
            TableName = tableName;
        }
    }
}

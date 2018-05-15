using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary.JSONTypes
{
    public class QueryJson : Msg
    {
        public QueryJson(Code.OperationCode code, string msg) : base(code, msg)
        { }

        public string TableName;
    }
}

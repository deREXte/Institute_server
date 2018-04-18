using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    class MsgJson : StructJson
    {
        public Code.OperationCode Code    { get; set; }
        public string             Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    public class Msg
    {

        public Msg() { }

        public Msg(Code.OperationCode code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public Code.OperationCode Code { get; set; }
        public string Message { get; set; }
    }
}

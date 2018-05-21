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

        public Msg(OperationCode code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public OperationCode Code { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Server
{


    class MThreadAbortedException : Exception
    {
        public new string Message
        {
            get;
            private set;
        }

        public MThreadAbortedException(string text)
        {
            Message = text;
        }

        public MThreadAbortedException()
        {
            Message = "Unknown error!";
        }
    }

    class NewClient
    {
        

    }
}

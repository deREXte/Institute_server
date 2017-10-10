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
    class NewClient
    {
        Thread thread;
        ThreadAbortException threadabortexception;
        Socket handler;

        public NewClient(Thread thread,Socket handler)
        {
            this.thread = thread;
            this.handler = handler;
        }

        void StartListen()
        {

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Data;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server();
            s.StartServer();
         
        }
    }
}

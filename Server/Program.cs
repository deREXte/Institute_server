using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(55000);
            server.StartServer();
        }
    }
}

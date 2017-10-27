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
            try
            {
                DataBaseOperations.INIT();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
           // DataRow dr = DataBaseOperations.GetUserPassword("deREXte");
            Server server = new Server(55000);
            server.StartServer();
        }
    }
}

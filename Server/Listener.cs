using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Listener
    {
        Log ServerLog; 

        public Listener()
        {
            ServerLog = new Log("ServerLog");
        }

        public void StartListen()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            ServerLog.Write("Server is started!");
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                listener.ReceiveTimeout = -1;
                while (true)
                {
                    Socket sock = listener.Accept();
                    new Thread(new ParameterizedThreadStart(CreateNewSession)).Start(sock);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private void CreateNewSession(Object socket)
        {
            new Session((Socket)socket, ServerLog).CreateSession();
        }
    }

}

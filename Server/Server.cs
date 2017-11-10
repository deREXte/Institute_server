using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Server
    {
        Socket socket;

        List<NewClient> clients;

        public Server(int port)
        {
            IPHostEntry iphost = Dns.GetHostEntry("192.168.1.33");
            IPAddress ipAddr = iphost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 55000);

            socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEndPoint);

            clients = new List<NewClient>();
        }

        public void StartServer()
        {
            if (socket == null)
                throw new NullReferenceException();
            socket.Listen(20);
            while (true)
            {
                Socket handler = socket.Accept();
                Thread thread = new Thread(new ParameterizedThreadStart(AcceptConnection));
                thread.Start(handler);
                Console.WriteLine("Accepted new Connection");
            }
        }

        void AcceptConnection(object h)
        {
            NewClient newClient =  new NewClient((Socket)h);
            clients.Add(newClient);
            newClient.StartListen();
        }
    }
}

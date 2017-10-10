﻿using System;
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


        public Server(int port)
        {
            IPHostEntry iphost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = iphost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 55000);

            socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEndPoint);
        }

        public void StartServer()
        {
            if (socket == null)
                throw new NullReferenceException();
            socket.Listen(10);
            int connections = 0;
            while (connections < 10)
            {
                Socket handler = socket.Accept();
                byte[] buffer = new byte[] { 99 };
                connections++;
                Thread thread = new Thread(new ParameterizedThreadStart(AcceptConnection));
                thread.Name = connections + "";
                thread.Start(handler);
                Console.WriteLine("Accepted new Connection");
            }
            socket.Close();
        }

        void AcceptConnection(object h)
        {
            new NewClient(Thread.CurrentThread, (Socket)h);
        }

        Exception SendMessage(Socket handler,string text)
        {
            try
            {
                handler.Send(Encoding.UTF8.GetBytes(text));
            }catch(Exception ex)
            {
                return ex;
            }
            return null;
        }

        void ReportErrorMessage(Exception ex)
        {
            Console.Write(ex.Message);
            Thread.CurrentThread.Abort();
        }
        
    }
}

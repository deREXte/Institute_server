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
        Socket handler;
        Thread thread;

        public NewClient()
        {
            thread = Thread.CurrentThread;
        }

        public NewClient(Socket handle)
        {
            handler = handle;
            thread = Thread.CurrentThread;
        }


        public void StartListen()
        {
            handler.ReceiveTimeout = 1000;
            if (!CheckClient())
                return;

            handler.ReceiveTimeout = 150000;
            if (!Authorization())
                return;
            
            handler.ReceiveTimeout = 1500000;

        }

        private bool Authorization()
        {
            string login = "", password = "";
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    handler.Receive(buffer, 1024, SocketFlags.None);
                }catch(Exception e)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + " " + e.Message);
                    return false;
                }
                string bufpass;
                int logLength = buffer[0], passlength = buffer[1];
                string buf = Encoding.UTF8.GetString(buffer);
                string bufferlog = buf.Substring(2, logLength);
                if(bufferlog != login)
                {
                    password = DataBaseOperations.GetUserPassword(bufferlog);
                    login = bufferlog;
                }
                bufpass = buf.Substring(logLength + 2, passlength);
                if(bufpass == password)
                {
                    handler.Send(new byte[1] { 101 }, 1, SocketFlags.None);
                    return true;
                }
            }
        }



        private bool CheckClient()
        {
            int ins = DateTime.Today.Minute;
            int orse = DateTime.Today.Hour;
            int ppp = DateTime.Today.Day * DateTime.Today.Month * DateTime.Today.Year;
            ppp += ((ppp & 1) == 1) ? 1 : 0;
            byte[] len = new byte[1];
            int leng;
            handler.Receive(len, 1, SocketFlags.None);
            leng = int.Parse(Encoding.UTF8.GetString(len)) + 7;
            byte[] buffer = new byte[leng];
            handler.Receive(buffer, leng, SocketFlags.None);
            handler.Receive(len, 1, SocketFlags.None);
            leng = int.Parse(Encoding.UTF8.GetString(len)) + 7;
            byte[] buffer2 = new byte[leng];
            handler.Receive(buffer2, leng, SocketFlags.None);
            string n = Encoding.UTF8.GetString(buffer);
            string h = "";
            for(int i = 0,length = n.Length; i < length; i++)
            {
                h += (char)(n[i] + orse - ins);
            }
            long par = 0;
            if(!Int64.TryParse(h,out par))
            {
                return false;
            }
            par = ((((par - 4) / 2) + 10) / ppp);
            n = Encoding.UTF8.GetString(buffer2);
            h = "";
            for (int i = 0, length = n.Length; i < length; i++)
            {
                h += (char)(n[i] + orse - ins);
            }
            long l = 0;
            if (!Int64.TryParse(h, out l))
            {
                return false;
            }
            l = ((((l - 4) / 2) + 10) / ppp);
            if (l + par == ppp)
            {
                Console.WriteLine("OK");
                handler.Send(Encoding.UTF8.GetBytes("OK"), 2, SocketFlags.None);
                return true;
            }
            else
            {
                Console.WriteLine("NOPE");
                return false;
            }
        }


    }
}

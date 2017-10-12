using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        Socket sender;

        public Form1()
        {
            InitializeComponent();
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 55000);

            // Create a TCP/IP  socket.
            sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(remoteEP);
                sender.ReceiveTimeout = -1;
                sender.SendTimeout = -1;
                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());
                if (function())
                    Console.WriteLine("OK");

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        bool function()
        {
            int ins = DateTime.Today.Minute;
            int orse = DateTime.Today.Hour;
            int ppp = DateTime.Today.Day * DateTime.Today.Month * DateTime.Today.Year;
            //int ppp = 31 * 12 * 2100;
            //int ppp = 1 * 1 * 2017;
            ppp += ((ppp & 1) == 1) ? 1 : 0;
            long n1, n2;
            n1 = n2 = ppp / 2;
            Random r = new Random(ppp);
            int ran = r.Next(ppp / 2 / 2 - 1);
            if (((ran & 1) == 1))
            {
                n1 -= ran;
                n2 += ran;
            }
            else
            {
                n2 -= ran;
                n1 += ran;
            }
            n1 = ((((n1 * ppp) - 10) * 2) + 4);
            n2 = ((((n2 * ppp) - 10) * 2) + 4);
            string bn1 = n1.ToString(),bn2 = n2.ToString();
            string resbn1 = "", resbn2 = "";
            for(int i = 0, length = bn1.Length; i < length; i++)
            {
                resbn1 += (char)(bn1[i] + ins - orse);
            }
            for (int i = 0, length = bn2.Length; i < length; i++)
            {
                resbn2 += (char)(bn2[i] + ins - orse);
            }
            Console.WriteLine(resbn1.Length);
            Console.WriteLine(resbn2.Length);
            sender.Send(Encoding.UTF8.GetBytes((resbn1.Length - 7).ToString()), 1, SocketFlags.None);
            sender.Send(Encoding.UTF8.GetBytes(resbn1), resbn1.Length, SocketFlags.None);
            sender.Send(Encoding.UTF8.GetBytes((resbn2.Length - 7).ToString()), 1, SocketFlags.None);
            sender.Send(Encoding.UTF8.GetBytes(resbn2), resbn2.Length, SocketFlags.None);
            byte[] answer = new byte[2];         
            sender.Receive(answer, 2, SocketFlags.None);
            if (Encoding.UTF8.GetString(answer) == "OK")
                return true; 
            else
                return false;
        }
    }
}

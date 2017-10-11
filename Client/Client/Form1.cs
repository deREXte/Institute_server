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
        Thread t;
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
                t = new Thread(new ThreadStart(check));
                t.Start();

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

        void check()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                sender.Receive(buffer);
                string str = Encoding.UTF8.GetString(buffer);
                if (str.Substring(0, 4) == "PING")
                {
                    sender.Send(Encoding.UTF8.GetBytes("PING|NULL"));
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }
    }
}

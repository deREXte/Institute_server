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
    public partial class AuthorizationForm : Form
    {
        Socket sender;

        public AuthorizationForm()
        {
            InitializeComponent();
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry("192.168.1.33");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 55000);

            // Create a TCP/IP  socket.
            Data.connection = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Data.connection.Connect(remoteEP);
                Data.connection.ReceiveTimeout = -1;
                Data.connection.SendTimeout = -1;
                /*MessageBox.Show("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());*/
                if (function())
                    Console.WriteLine("OK");

            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Unexpected exception : {0}", e.ToString());
            }
        }

        bool function()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}

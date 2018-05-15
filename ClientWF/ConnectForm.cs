using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using ServerClientClassLibrary;
using ServerClientClassLibrary.JSONTypes;

namespace ClientWF
{
    public partial class ConnectForm : Form
    {
        Socket Handler;
        IODialog IODialog;
        SQLExecuter Executer;
        bool Connected, Authorization;

        public ConnectForm()
        {
            InitializeComponent();
            Executer = new SQLExecuter(IODialog, (msg) => MessageBox.Show(msg));
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry("192.168.1.33");
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
            // Create a TCP/IP  socket.
            Handler = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Handler.Connect(remoteEP);
                Handler.ReceiveTimeout = -1;
                Handler.SendTimeout = -1;
                /*MessageBox.Show("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());*/
                if (PassConnectionTest())
                {
                    Connected = true;
                    TB_Connection.Text = "OK";
                }
                else
                    TB_Connection.Text = "Error";
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

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            if (Connect() == true)
            {
                MessageBox.Show("OK");
                Authorization = true;
                Close();
            }
            else
                MessageBox.Show("error");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (!Connected || !Authorization)
                Handler = null;
        }

        private bool Connect()
        {
            string login = "UserName=", password = "Password=";
            bool ok = false;
            login += TextBox_Login.Text + ";";
            password += TextBox_Password.Text + ";";
            Executer.ApplyCommand<QueryJson>(new QueryJson(Code.OperationCode.Login, password + login),
                (msg) => ok = msg.Code == Code.OperationCode.AnswerOK);
            return ok; 
        }

        public bool PassConnectionTest()
        {
            IODialog = new IODialog(Handler);
            Executer.ApplyCommand<QueryJson>(
                new QueryJson(Code.OperationCode.InitConnection, IODialog.PassPhrase),
                null);
            return IODialog.ReceiveCode() == Code.OperationCode.AnswerOK;
        }
        
        public IODialog GetInformation()
        {
            return IODialog;
        }

    }
}

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
        const string SOCKET_EXCEPTION = "Ошибка подключения к серверу!";
        const string FORMAT_EXCEPTION = "Ошибка формата IP адреса";

        public SQLExecuter Executer
        {
            get;
            private set;
        } 

        int TypeUser;

        public ConnectForm()
        {
            InitializeComponent();
            TypeUser = 0;
            DialogResult = DialogResult.None;
            try
            {
                Connect();
            }
            catch (SocketException)
            {
                MessageBox.Show(SOCKET_EXCEPTION);
            }
            catch (FormatException)
            {
                MessageBox.Show(FORMAT_EXCEPTION);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            TypeUser = Login();
            if (TypeUser > 0)
            {
                MessageBox.Show("OK");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("error");
        }

        private void Connect()
        {
            Socket handler = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);;
            var address = IPAddress.Parse("192.168.1.33");
            IPEndPoint remoteEP = new IPEndPoint(address, 11000);
            handler.Connect(remoteEP);
            handler.ReceiveTimeout = -1;
            handler.SendTimeout = -1;
            Executer = new SQLExecuter(new IODialog(handler), 
                (msg) => MessageBox.Show(msg));
        }

        protected override void OnClosed(EventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }

        private int Login()
        {
            string login = "UserName=", password = "Password=";
            int ok = 0;
            login += TextBox_Login.Text + ";";
            password += TextBox_Password.Text + ";";
            Executer.ApplyCommand<QueryJson>(new QueryJson(Code.OperationCode.Login, password + login),
                (msg) =>
                {
                    if (msg.Code == Code.OperationCode.AdminAuthorized)
                        ok = 2;
                    else if (msg.Code == Code.OperationCode.UserAuthorized)
                        ok = 1;
                    else
                        ok = -1;
                });
            return ok; 
        }

        /*public bool PassConnectionTest()
        {
            IODialog = new IODialog(Handler);
            Executer.ApplyCommand<QueryJson>(
                new QueryJson(Code.OperationCode.InitConnection, IODialog.PassPhrase),
                null);
            return IODialog.ReceiveMessage<QueryJson>().Code == Code.OperationCode.AnswerOK;
        }*/

    }
}

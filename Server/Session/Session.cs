using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ServerClientClassLibrary;

namespace Server
{
    class Session
    {
        Socket Handler;
        IODialog IODialogWithClient;
        
        Log ServerLog, UserLog;

        public Session(Socket handler, Log serverLog)
        {
            Handler = handler;
            IODialogWithClient = new IODialog(Handler);
            ServerLog = serverLog;
        }

        public void CreateSession()
        {
            if (!AcceptNewClient())
                return;
            StartDialog();
        }

        private void StartDialog()
        {
            string msg;
            while (true)
            {  //
                msg = IODialogWithClient.ReceiveMessage(out byte code);
                if (code == 99)
                    return;
               // DBOperations.ExecuteCommand(msg, code);
                IODialogWithClient.SendMessage(msg, 51);
            }
        }

        private bool AcceptNewClient()
        {
            AcceptNewConnection anc = new AcceptNewConnection(IODialogWithClient);
            try
            {
                Handler.ReceiveTimeout = 25000;
                if (!anc.Accept())
                    return false;
            }catch(SocketException)
            {
                ServerLog.Write("Connection failed!");
                return false;
            }
            string name = anc.UserName;
            UserLog = new Log(name);
            ServerLog.Write(name + " connected!");
            return true;
        }
    }
}

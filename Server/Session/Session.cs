using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using ServerClientClassLibrary;

namespace Server
{
    class Session
    {
        Log ServerLog;
        IODialog IODialogWithClient;
        Operations Operations;

        public Session(Socket handler, Log serverLog)
        {
            ServerLog = serverLog;
            IODialogWithClient = new IODialog(handler);
            Operations = new Operations(ServerLog, IODialogWithClient);
        }

        public void CreateSession()
        {
            if (!Operations.CheckConnection())
                return;
            StartDialog();
        }

        private void StartDialog()
        {
            string msg;
            while (true)
            {  //
                try
                {
                    msg = IODialogWithClient.ReceiveMessage(out byte code);
                    Operations.ExecuteCommand(msg, code);
                }
                catch (SocketException e)
                {
                    ServerLog.Write(Operations.UserName + " disconnected with error!");
                    return;
                }
            }
        }

    }
}


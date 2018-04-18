using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using ServerClientClassLibrary;
using Newtonsoft.Json;

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
            {
                IODialogWithClient.SendMessage(Code.OperationCode.AnswerError);
                return;
            }
            IODialogWithClient.SendMessage(Code.OperationCode.AnswerOK);
            StartDialog();
        }

        private void StartDialog()
        {
            string msg;
            while (true)
            {  //
                msg = IODialogWithClient.ReceiveMessage(out Code.OperationCode code);
                Operations.ExecuteCommand(msg, code);
            }
        }

    }
}


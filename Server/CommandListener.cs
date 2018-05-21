using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using ServerClientClassLibrary;
using ServerClientClassLibrary.JSONTypes;
using Newtonsoft.Json;
using Server.UserSession;
using Server.NewOperations;

namespace Server
{
    class CommandListener
    {
        Session Session;

        public CommandListener(Socket socket)
        {
            Session = new Session(socket);
        }

        public void StartDialog()
        {
            while (true)
            { 
                var msg = Session.Dialog.ReceiveMessage<QueryJson>();
                OperationSwitcher.Execute(Session, msg);
            }
        }

    }
}
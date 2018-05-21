using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.NewOperations.Operations;
using ServerClientClassLibrary;
using System.Net.Sockets;

namespace Server.UserSession
{
    class Session
    {
        public Rights UserRights
        {
            get;
            set;
        }

        public IODialog Dialog
        {
            get;
            private set;
        }

        Log UserLog;

        public Session(Socket socker)
        {
            Dialog = new IODialog(socker);
            UserRights = Rights.Other;
        }

        public void CreateLog(string userName)
        {
            UserLog = new Log(userName);
        }

        public void WriteLog(string text)
        {
            if(UserLog != null)
                UserLog.Write(text);
        }

        public void Close()
        {
            UserLog.Close();
        }
    }
}

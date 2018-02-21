using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ServerClientClassLibrary;

namespace Server
{
    class AcceptNewConnection
    {
        IODialog Handler;

        public string UserName
        {
            get;
            private set;
        }

        public AcceptNewConnection(IODialog dialog)
        {
            Handler = dialog;
        }

        public bool Accept()
        {
            try
            {
                if (!CheckConnection())
                {
                    return false;
                }
                if (!Authorization())
                {
                    return false;
                }
            }
            catch (SocketException)
            {
                return false;
            }
            return true;
        }

        // опасно,конечно, передовать кодовое слово
        
        

 
    }
}

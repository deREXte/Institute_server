using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace Client
{
    static class Data
    {

        public static Socket connection;

        public static bool IsConnected
        {
            get { return connection.Connected; }
        }

        private static bool authorized;
        public static bool Authorized
        {
            get { return authorized; }
            set { authorized = value; }
        }

    }
}

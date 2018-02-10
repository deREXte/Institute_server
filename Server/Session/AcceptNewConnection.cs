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
            }catch(SocketException)
            {
                return false;
            }
            return true;
        }

        // опасно,конечно, передовать кодовое слово
        private bool CheckConnection()
        {
            return Handler.ReceiveMessage(out byte code) == Handler.PassPhrase;
        }
        
        private bool Authorization()
        {
            string userName, password, buffer;
            Handler.SendMessage(10);
            while (true)
            {
                buffer = Handler.ReceiveMessage(out byte code);
                userName = SubEnv(buffer, "UserName", ';');
                password = SubEnv(buffer, "Password", ';');
                if (DataBaseOperations.CheckUserLoginData(userName, password))
                {
                    Handler.SendMessage(10);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            UserName = userName;
            return true;
        }

        private string SubEnv(string text, string word, char symbol)
        {
            int indexofword,indexofsymbol, length = word.Length;
            indexofword = text.IndexOf(word);
            indexofsymbol = text.IndexOf(symbol, indexofword);
            return text.Substring(indexofword + length + 1, indexofsymbol - (length + 1) - indexofword);
        }
    }
}

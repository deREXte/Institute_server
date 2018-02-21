using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Configuration;
using System.Collections.Specialized;


namespace ServerClientClassLibrary
{

    public class IODialog
    {
        const int PacketLength = 4096;

        Socket Handler;

        public string PassPhrase
        {
            get;
            private set;
        }

        public IODialog(Socket socket)
        {
            Handler = socket;
            PassPhrase = Crypt.GeneratePassPhrase();
        }

        public void SendMessage(byte code)
        {
            Handler.Send(new byte[1] { code }, 1, SocketFlags.None);
        }

        public void SendMessage(string text, byte code)
        {
            text = Crypt.Encrypt(text, PassPhrase);
            SendMessage(Encoding.UTF8.GetBytes(text), code);
        }

        private void SendMessage(byte[] text, byte code)
        {
            byte[] sendbuffer = new byte[text.Length + 1];
            sendbuffer[0] = code;
            text.CopyTo(sendbuffer, 0);
            Handler.Send(sendbuffer, sendbuffer.Length, SocketFlags.None);
        }

        public string ReceiveMessage(out byte code)
        {
            code = 0;
            StringBuilder result = new StringBuilder();
            var buffer = new byte[4096];
            var first_message = true;
            var netstream = new NetworkStream(Handler, true);
            var numberOfBytesRead = 0;
            do
            {
                numberOfBytesRead = netstream.Read(buffer, 0, buffer.Length);
                result.Append(Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead - 1));
                if (first_message)
                {
                    first_message = false;
                    code = (byte)result[0];
                }
            } while (netstream.DataAvailable);
            return Crypt.Decrypt(result.ToString(), PassPhrase);
        }
    }
}

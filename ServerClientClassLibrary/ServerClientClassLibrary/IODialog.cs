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
        Socket Handler;
        const int PacketLength = 4096;
        string _PassPhrase;

        public string PassPhrase
        {
            get { return _PassPhrase; }
        }

        public IODialog(Socket socket)
        {
            Handler = socket;
            _PassPhrase = Crypt.GeneratePassPhrase();
        }

        public void SendMessage(byte code)
        {
            Handler.Send(new byte[1] { code }, 1, SocketFlags.None);
        }

        public void SendMessage(string text, byte code)
        {
            text = Crypt.Encrypt(text, _PassPhrase);
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
            byte[] buffer = new byte[4096];
            bool first_message = true;
            var netstream = new NetworkStream(Handler, true);
            int numberOfBytesRead = 0;
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

        private byte[] Subarray(byte[] array, int from, int length)
        {
            byte[] buffer = new byte[length];
            for (int i = 0; i < length && i + from < array.Length; i++)
            {
                buffer[i] = array[from + i];
            }
            return buffer;
        }
    }
}

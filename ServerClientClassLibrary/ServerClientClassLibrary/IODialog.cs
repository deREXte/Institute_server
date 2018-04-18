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

        /// <summary>
        /// Сокет для общения между клиентом и сервером
        /// </summary>
        Socket Handler;

        /// <summary>
        /// Ключ шифрования
        /// </summary>
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

        /// <summary>
        /// Отпраляет лишь код.
        /// </summary>
        /// <param name="code">Код</param>
        public void SendMessage(Code.OperationCode code)
        {
            Handler.Send(new byte[1] { (byte)code }, 1, SocketFlags.None);
        }

        /// <summary>
        /// Отправляет строку text с кодом code
        /// </summary>
        /// <param name="text">Сообщение для отправки</param>
        /// <param name="code">Код сообщения</param>
        public void SendMessage(string text, Code.OperationCode code)
        {
            text = Crypt.Encrypt(text, PassPhrase);
            SendMessage(Encoding.UTF8.GetBytes(text), (byte)code);
        }

        private void SendMessage(byte[] text, byte code)
        {
            byte[] sendbuffer = new byte[text.Length + 1];
            sendbuffer[0] = code;
            text.CopyTo(sendbuffer, 1);
            Handler.Send(sendbuffer, sendbuffer.Length, SocketFlags.None);
        }

        /// <summary>
        /// Принимает сообщение от отправителя
        /// </summary>
        /// <param name="code">Код оперции полученный от отправителя</param>
        /// <returns>Возращает сообщение полученное от отправителя</returns>
        public string ReceiveMessage(out Code.OperationCode code)
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
                result.Append(Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead ));
                if (first_message)
                {
                    first_message = false;
                    code = (Code.OperationCode)result[0];
                    result.Remove(0, 1);
                }
            } while (netstream.DataAvailable);
            string s = result.ToString();
            if (s.Length == 0)
                return s;
            return Crypt.Decrypt(s, PassPhrase);
        }

        /// <summary>
        /// Получает код операции
        /// </summary>
        /// <returns>Возращает код</returns>
        public Code.OperationCode ReceiveCode()
        {
            byte[] buffer = new byte[1];
            Handler.Receive(buffer);
            return (Code.OperationCode)buffer[0];
        }
    }
}

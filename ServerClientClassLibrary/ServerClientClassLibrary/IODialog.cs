using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;

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
            //PassPhrase = Crypt.GeneratePassPhrase();
        }

        /// <summary>
        /// Отпраляет лишь код.
        /// </summary>
        /// <param name="code">Код</param>
        public void SendMessage(OperationCode code)
        {
            Msg msg = new Msg(code, "");
            byte[] buf = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));
            Handler.Send(buf, buf.Length, SocketFlags.None);
        }

        /// <summary>
        /// Отправляет строку text с кодом code
        /// </summary>
        /// <param name="text">Сообщение для отправки</param>
        /// <param name="code">Код сообщения</param>
        public void SendMessage<T>(T msg) where T : Msg
        {
            string text = JsonConvert.SerializeObject(msg);
            //text = Crypt.Encrypt(text, PassPhrase);
            SendMessage(Encoding.UTF8.GetBytes(text));
        }

        private void SendMessage(byte[] text)
        {
            Handler.Send(text, text.Length, SocketFlags.None);
        }

        /// <summary>
        /// Принимает сообщение от отправителя
        /// </summary>
        /// <param name="code">Код оперции полученный от отправителя</param>
        /// <returns>Возращает сообщение полученное от отправителя</returns>
        public T ReceiveMessage<T>() where T: Msg
        {
            StringBuilder result = new StringBuilder();
            var buffer = new byte[4096];
            var netstream = new NetworkStream(Handler, true);
            var numberOfBytesRead = 0;
            do
            {
                numberOfBytesRead = netstream.Read(buffer, 0, buffer.Length);
                result.Append(Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead ));
            } while (netstream.DataAvailable);
            string s = result.ToString();
            if (s.Length == 0)
                return null;
            return JsonConvert.DeserializeObject<T>(/*Crypt.Decrypt(*/s/*, PassPhrase)*/);
        }

        public void Close()
        {
            Handler.Close();
        }
    }
}

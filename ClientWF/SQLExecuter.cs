using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary;
using Newtonsoft.Json;

namespace ClientWF
{
    class SQLExecuter
    {

        public delegate void Print<T>(Code.OperationCode c,T js) where T : StructJson;
        public delegate void PrintError(string msg);

        IODialog Handler;
        PrintError PrintErr;

        public SQLExecuter(IODialog handler, PrintError prerr)
        {
            Handler = handler;
            PrintErr = prerr;
        }

        /// <summary>
        /// Отправляет команду command через iodialog с операцией "c" и выводит информацию в print. 
        /// </summary>
        /// <param name="command">Текст команды</param>
        /// <param name="c">Код операции.Код операции будет изменен в данной функции</param>
        /// <param name="data">Будут ли данные от сервера</param>
        /// <param name="print">Делегат для вывода информации</param>
        public void ApplyCommand<T>(string command, Code.OperationCode c, bool data, Print<T> print) where T : StructJson
        {
            string msg = SendCommandAndReceive(command,ref c);
            if(c == Code.OperationCode.AnswerError)
            {
                PrintErr(msg);
                return;
            }
            if (data)
            {
                var js = JsonConvert.DeserializeObject<T>(msg);
                print?.Invoke(c, js);
            }
            else
                print?.Invoke(c, null);
        }


        public void ApplyCommand<T>(Code.OperationCode c, Print<T> print) where T : StructJson
        {
            string msg = SendCommandAndReceive("", ref c);
            if(c == Code.OperationCode.AnswerError)
            {
                PrintErr(msg);
                return;
            }
            print?.Invoke(c, null);
        }

        public void ApplyCommand(Code.OperationCode c)
        {
            SendCommand("", c);
        }

        public void ApplyCommand(string command, Code.OperationCode c)
        {
            
        }

        private string SendCommandAndReceive(string command, ref Code.OperationCode c)
        {
            Handler.SendMessage(command, c);
            return Handler.ReceiveMessage(out c);
        }

        private void SendCommand(string command, Code.OperationCode c)
        {
            Handler.SendMessage(command, c);
        }
    }
}

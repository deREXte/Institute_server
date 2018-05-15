using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary;
using Newtonsoft.Json;
using ServerClientClassLibrary.JSONTypes;

namespace ClientWF
{
    class SQLExecuter
    {

        public delegate void Print<T>(T js) where T : Msg;
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
        public void ApplyCommand<T>(QueryJson msg, Print<T> print) where T : Msg
        {
            var rec = SendCommandAndReceive<T>(msg);
            if(rec.Code == Code.OperationCode.AnswerError)
            {
                PrintErr(msg.Message);
                return;
            }
            print?.Invoke(rec);
        }

        public void ApplyCommand(Msg msg)
        {
            Send(msg);
        }

        private T SendCommandAndReceive<T>(Msg msg) where T : Msg
        {
            Handler.SendMessage(msg);
            return Handler.ReceiveMessage<T>();
        }

        private void Send(Msg msg)
        {
            Handler.SendMessage(msg);
        }
    }
}

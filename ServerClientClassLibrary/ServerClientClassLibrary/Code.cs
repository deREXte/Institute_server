using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    /*byte bin 1 1
          oct 7
          hex ff
      Дейсвтия с базой данной клиентом:
        Изменение учетных данных;
            Логин;
            Пароль;
            Удаление профиля;
        Получение таблицы либо выбранные записи;
        Удаление строк;
        Вставка строк;
        Изменение строк;
         */
    public class Code
    {
        public enum OperationCode : byte
        {
            /*0 - 19: Answers*/
            AnswerOK = 0,
            AnswerError = 1,

            /*20 - 39: Requests*/
            SELECT = 20,
            INSERT = 21,
            UPDATE = 22,
            DELETE = 23,

            /*40 - 59: UserData*/
            Login = 40,
            GenerateUserData = 41,
        }
    }
}

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
        public enum OperationCode 
        {
            /*0 - 19: Answers*/
            AnswerOK = 0,
            AnswerError = 1,
            ConnectionRefuse = 2,

            /*20 - 39: Requests*/
            SELECT = 20,
            INSERT = 21,
            UPDATE = 22,
            DELETE = 23,

            /*40 - 59: UserData*/
            Login = 40,
            GenerateUserData = 41,
            CreateUsers = 42,
            DeleteUsers = 43,

            InitConnection = 60,

            UserAuthorized = 70,
            AdminAuthorized = 71,
        }
    }
}

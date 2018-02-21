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
        public enum OperationPurpose_1
        {
            DataBase = 0,
            Server = 1,
            Answer = 2,
        }

        public enum Answer_10
        {
            Error = 0,
            Success = 1,
        }

        public enum DBOperation_10
        {
            Select = 0,
            Update = 1,
            Insert = 2,
            Delete = 3,
            UserData = 4,
        }

        public enum UserDataOperation_100
        {
            ChangeLogin = 0,
            ChangePassword = 1,
            DeleteProfile = 2,
            Auto = 3,
            Registration = 4,
        }

        public enum ServerOperation_10
        {
            ConnectionRefused = 0,
        }
    }
}

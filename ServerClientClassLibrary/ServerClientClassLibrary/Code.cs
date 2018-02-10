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
        public enum OperationPurpose 
        {
            DataBase = 0,
            Server = 1,
        }

        public enum DBOperation 
        {
            Select = 0,
            Update = 1,
            Insert = 2,
            Delete = 3,
            UserData = 4,
        }

        public enum UserDataOperation 
        {
            ChangeLogin = 0,
            ChangePassword = 1,
            DeleteProfile = 2,
        }

        public enum ServerOperation 
        {
            ConnectionRefused = 0,
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientClassLibrary
{
    public class GenUserDataJson : StructJson
    {
        public List<UserData> Users;

        public GenUserDataJson()
        {
            Users = new List<UserData>();
        }

        public GenUserDataJson(Code.OperationCode code, string msg) : base(code, msg)
        {
            Users = new List<UserData>();
        }
    }

    public class UserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

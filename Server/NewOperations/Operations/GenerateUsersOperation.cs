using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.UserSession;
using ServerClientClassLibrary.JSONTypes;

namespace Server.NewOperations.Operations
{
    class GenerateUsersOperation : AOperations
    {
        public GenerateUsersOperation(Rights rights) : base(rights)
        {
        }

        protected override void ApplyCommand(Session session, QueryJson query)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            string lettersRandom = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890";
            int len = lettersRandom.Length;
            int count = int.Parse(GetVariable(query.Message, "Count"));
            GenUserDataJson users = new GenUserDataJson();
            string log, pass;
            for (int i = 0; i < count; i++)
            {
                log = ""; pass = "";
                for (int j = 0; j < 10; j++)
                    log += lettersRandom[rand.Next(0, len)];
                for (int j = 0; j < 10; j++)
                    pass += lettersRandom[rand.Next(0, len)];
                users.Users.Add(new UserData { Login = log, Password = pass });
                DataBaseOperations.CreateUserProfile(log, pass);
            }
            session.Dialog.SendMessage(users);
        }
    }
}

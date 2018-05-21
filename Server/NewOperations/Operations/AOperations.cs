using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary;
using ServerClientClassLibrary.JSONTypes;
using Server.UserSession;
using System.Data.SqlClient;

namespace Server.NewOperations.Operations
{
    abstract class AOperations
    {
        public Rights Rights { get; set; }

        public AOperations(Rights rights)
        {
            Rights = rights;
        }

        public bool CheckRights(Rights userRights)
        {
            return (Rights & userRights) == userRights;
        }

        public void Execute(Session session, QueryJson query)
        {
            WrapperApplyCommand(session, query);
        }

        protected virtual void WrapperApplyCommand(Session session, QueryJson query)
        {
            try
            {
                ApplyCommand(session, query);
            }
            catch(SqlException e)
            {
                session.Dialog.SendMessage(new Msg(OperationCode.AnswerError, "Server error: " + e.Message));
            }
            catch (IndexOutOfRangeException e)
            {
                session.Dialog.SendMessage(new Msg(OperationCode.AnswerError, "Server error: " + e.Message));
            }
            catch(Exception e)
            {
                session.Dialog.SendMessage(new Msg(OperationCode.AnswerError, "Server error: " + e.Message));
            }
        } 

        protected abstract void ApplyCommand(Session session, QueryJson query);


        protected string GetVariable(string text, string word)
        {
            int indexOfWord = text.IndexOf(word) + word.Length + 1;
            StringBuilder result = new StringBuilder();
            for (; indexOfWord < text.Length && text[indexOfWord] != '&'; indexOfWord++)
                result.Append(text[indexOfWord]);
            return result.ToString();
        }
    }
}

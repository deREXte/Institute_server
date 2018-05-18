using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWF.SqlCreater
{
    public static class SqlConditionJoiner
    {
        public static string Join(List<SqlFieldValuePair> pairs, SqlConditions condition)
        {
            return string.Join(" " + condition.ToString() + " ", pairs);
        }

        public static string Join(List<SqlFieldValuePair> pairs, List<SqlConditions> conditions)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0, len = pairs.Count; i < len; i++)
            {
                result.AppendFormat("{0}", pairs[i]);
                if (i + 1 != len)
                    result.AppendFormat(" {0} ", conditions[i]);

            }
            return result.ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWF.SqlCreater
{
    public class SqlFieldValuePair
    {
        public KeyValuePair<string, object> Pair { get; set; }
        public string CompareOperator { get; set; }

        public SqlFieldValuePair(KeyValuePair<string, object> pair, string compareOperator)
        {
            Pair = pair;
            CompareOperator = compareOperator;
        }

        public override string ToString()
        {
            return $"{ Pair.Key }{ CompareOperator }{ Pair.Value.ToString() }";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientWF.SqlCreater;

namespace ClientWF
{
    public static class SqlCommandCreater
    {
        public static string CreateSelectCommand(string tableName)
        {
            return $"SELECT * FROM {tableName}"; 
        }

        public static string CreateSelectCommand(string tableName, IEnumerable<string> fields)
        {
            return $"SELECT {string.Join(",", fields)} FROM {tableName}";
        }

        public static string CreateSelectCommand(string tableName, SqlFieldValuePair pair)
        {
            return $"SELECT * FROM {tableName} WHERE {pair}";
        }

        public static string CreateSelectCommand(string tableName, IEnumerable<string> fields, SqlFieldValuePair pair)
        {
            return $"SELECT {string.Join(",", fields)} FROM {tableName} WHERE {pair}";
        }

        public static string CreateSelectCommand(string tableName, IEnumerable<string> fields, IEnumerable<SqlFieldValuePair> pairs, SqlConditions condition)
        {
            return $"SELECT {string.Join(",", fields)} FROM {tableName} WHERE {SqlConditionJoiner.Join(pairs.ToList(), condition)}";
        }

        public static string CreateSelectCommand(string tableName, IEnumerable<string> fields, IEnumerable<SqlFieldValuePair> pairs, IEnumerable<SqlConditions> conditions)
        {
            return $"SELECT {string.Join(",", fields)} FROM {tableName} WHERE {SqlConditionJoiner.Join(pairs.ToList(), conditions.ToList())}";
        }

        public static string CreateInsertCommand(string tableName, Dictionary<string, string> fields)
        {
            return $"INSERT INTO {tableName} ({string.Join(",", fields.Keys)}) VALUES ({string.Join(",", fields.Values)})";
        }

        public static string CreateInsertCommand(string tableName, IEnumerable<string> fields, IEnumerable<string> values)
        {
            return $"INSERT INTO {tableName} ({string.Join(",", fields)}) VALUES ({string.Join(",", values)})";
        }

        public static string CreateInsertCommand(string tableName, IEnumerable<SqlFieldValuePair> pairs)
        {
            return $"INSERT INTO {tableName} ({string.Join(",", pairs.Keys())}) VALUES ({string.Join(",", pairs.Values())})";
        }
        public static string CreateUpdateCommand(string tableName, IEnumerable<SqlFieldValuePair> pairs)
        {
            return $"UPDATE {tableName} SET {string.Join(",", pairs)}";
        }

        public static string CreateUpdateCommand(string tableName, IEnumerable<SqlFieldValuePair> set, IEnumerable<SqlFieldValuePair> conditionsPairs, SqlConditions conditions)
        {
            return $"UPDATE {tableName} SET {string.Join(",", set)} WHERE {SqlConditionJoiner.Join(conditionsPairs.ToList(), conditions)}";
        }

        public static string CreateUpdateCommand(string tableName, IEnumerable<SqlFieldValuePair> set, IEnumerable<SqlFieldValuePair> conditionsPairs, IEnumerable<SqlConditions> conditions)
        {
            return $"UPDATE {tableName} SET {string.Join(",", set)} WHERE {SqlConditionJoiner.Join(conditionsPairs.ToList(), conditions.ToList())}";
        }

        private static IEnumerable<string> Values(this IEnumerable<SqlFieldValuePair> pairs)
        {
            List<string> values = new List<string>();
            foreach (var pair in pairs)
                values.Add(pair.Pair.Value.ToString());
            return values;
        }

        private static IEnumerable<string> Keys(this IEnumerable<SqlFieldValuePair> pairs)
        {
            List<string> keys = new List<string>();
            foreach (var pair in pairs)
                keys.Add(pair.Pair.Key);
            return keys;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWF
{
    class CellValue
    {

        private delegate string StringEqType(object value);

        private static readonly Dictionary<Type, StringEqType> @switch = new Dictionary<Type, StringEqType>()
        {
            { typeof(string), (value) => { return $"'{value.ToString()}'"; } },
            { typeof(int), (value) => { return $"{value.ToString()}"; } },
        };

        public object OldValue;
        public object NewValue;
        public Type Type;

        public CellValue(Type type)
        {
            Type = type;
        }

        public CellValue(Type type, object oldValue, object newValue)
        {
            Type = type;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string GetValue()
        {
            if(@switch.ContainsKey(Type))
                return @switch[Type](NewValue);
            return NewValue.ToString();
        }
    }
}

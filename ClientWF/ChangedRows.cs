using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWF
{
    class ChangedRows
    {
        public Dictionary<int, ChangedRow> Rows;

        public ChangedRows()
        {
            Rows = new Dictionary<int, ChangedRow>();
        }

        public ChangedRows(int index, ChangedRow row)
        {
            Rows = new Dictionary<int, ChangedRow>()
            {
                { index, row },
            };
        }

        public void AddNewCell(int index, string columnName, Type type, object oldValue, object newValue)
        {
            if (!Rows.ContainsKey(index))
                Rows[index] = new ChangedRow();
            Rows[index].UpdateCell(columnName, type, oldValue, newValue);
        }

        public object ReturnValue(int index, string columnName)
        {
            if (Rows.ContainsKey(index))
            {
                object value =  Rows[index].ReturnValueToOld(columnName);
                if (Rows[index].Cells.Count == 0)
                    Rows.Remove(index);
                return value;
            }
            return null;
        }
    }
}

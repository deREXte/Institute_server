using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWF
{
    class ChangedRow
    {
        public Dictionary<string, CellValue> Cells; 

        public ChangedRow()
        {
            Cells = new Dictionary<string, CellValue>();
        }

        public ChangedRow(string columnName, CellValue value)
        {
            Cells = new Dictionary<string, CellValue>()
            {
                { columnName, value },
            };
        }

        public void UpdateCell(string columnName, Type type, object oldValue, object newValue)
        {
            if (Cells.ContainsKey(columnName))
                Cells[columnName].NewValue = newValue;
            else
                Cells[columnName] = new CellValue(type, oldValue, newValue);
        }

        public object ReturnValueToOld(string columnName)
        {
            if (Cells.ContainsKey(columnName))
                return Cells[columnName].OldValue;
            return null;
        }
    }
}

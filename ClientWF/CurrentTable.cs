using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerClientClassLibrary;
using ServerClientClassLibrary.JSONTypes;
using System.Data;
using System.Windows.Forms;
using ClientWF.SqlCreater;
using System.Drawing;

namespace ClientWF
{
    class CurrentTable
    {
        public string Name { get; set; }
        public DataTable CurTable { get; set; }
        public Dependences Dependences { get; set; }
        private ChangedRows ChangedRows;
        private object _TempValue;

        public void UpdateCell(int index, string columnName, Type type, object newValue)
        {
            if (ChangedRows == null)
                ChangedRows = new ChangedRows();
            ChangedRows.AddNewCell(index, columnName, type, _TempValue, newValue);
        }

        public void ReturnRowValue(DataGridView d, int index)
        {
            if (ChangedRows == null)
                return;
            foreach (DataColumn c in CurTable.Columns)
            {
                ReturnCell(index, c.ColumnName);
                PaintDefaultColor(d, index, c.ColumnName);
            }
        }

        public void ReturnCellValue(DataGridView d, int index, string columnName)
        {
            if (ChangedRows == null)
                return;
            ReturnCell(index, columnName);
            PaintDefaultColor(d, index, columnName);
        }

        private void ReturnCell(int index, string columnName)
        {
            object value = ChangedRows.ReturnValue(index, columnName);
            if (value != null)
                CurTable.Rows[index][columnName] = value;
        }

        public void ReturnAllCellsToValue(DataGridView dataGridView)
        {
            if (ChangedRows == null)
                return;
            foreach(int index in ChangedRows.Rows.Keys)
            {
                foreach(string columnName in ChangedRows.Rows[index].Cells.Keys)
                {
                    CurTable.Rows[index][columnName] = ChangedRows.Rows[index]
                                                       .Cells[columnName].OldValue;
                    PaintDefaultColor(dataGridView, index, columnName);
                }
            }
            ChangedRows = null;
        }

        public List<string> UpdateRows()
        {
            List<string> list = new List<string>();
            foreach(int index in ChangedRows.Rows.Keys)
            {
                List<SqlFieldValuePair> pairs = new List<SqlFieldValuePair>();
                foreach(string columnName in ChangedRows.Rows[index].Cells.Keys)
                {
                    pairs.Add(new SqlFieldValuePair(
                        new KeyValuePair<string, object>(columnName, 
                        ChangedRows.Rows[index].Cells[columnName].GetValue()), 
                        SqlCompareOperator.Equal));
                }
                list.Add(SqlCommandCreater.CreateUpdateCommand(Name, pairs,
                    new SqlFieldValuePair(
                        new KeyValuePair<string, object>("ID", CurTable.Rows[index]["Id"]),
                        SqlCompareOperator.Equal)));
            }
            return list;
        }

        private void PaintDefaultColor(DataGridView dataGridView, int index, string columnName)
        {
            dataGridView.Rows[index]
                .Cells[columnName].Style.BackColor = Color.White;
        }

        public void SetTempCellValue(object value)
        {
            _TempValue = value; 
        }

        public bool CompareValues(object newValue)
        {
            return _TempValue.Equals(newValue);
        }
    }
}

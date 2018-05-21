using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerClientClassLibrary.JSONTypes;
using ServerClientClassLibrary;
using ClientWF;
using ClientWF.SqlCreater;

namespace ClientWF.AddtionalForms
{
    partial class CreateNewRow : Form
    {

        SQLExecuter Executer;
        CurrentTable CurrentTable;

        public CreateNewRow()
        {
            InitializeComponent();
        }

        public CreateNewRow(SQLExecuter executer, DataGridView dataGridView, CurrentTable currentTable)
        {
            InitializeComponent();
            Executer = executer;
            CurrentTable = currentTable;
            DialogResult = DialogResult.Cancel;
            dataGridView_AddRow.AllowUserToAddRows = false;
            dataGridView_AddRow.AllowUserToDeleteRows = false;
            string columnName;
            for(int i = 0; i < dataGridView.ColumnCount; i++)
            {
                columnName = dataGridView.Columns[i].Name;
                if (currentTable.Dependences.Dependence.ContainsKey(columnName))
                    dataGridView_AddRow.Columns.Add(
                        new DataGridViewComboBoxColumn()
                        {
                            DataSource = currentTable.Dependences.Dependence[columnName]
                        });
                else
                    dataGridView_AddRow.Columns.Add(dataGridView.Columns[i].Name, dataGridView.Columns[i].Name);
            }
            dataGridView_AddRow.Rows.Add();
        }

        private void button_AddRow_Click(object sender, EventArgs e)
        {
            string command = CreateCommand();
            QueryJson query = new QueryJson(OperationCode.INSERT, command, CurrentTable.Name);
            var msg = Executer.ApplyCommand<QueryJson>(query);
            MessageBox.Show(msg.Message);
        }

        private string CreateCommand()
        {
            var row = dataGridView_AddRow.Rows[0];
            List<SqlFieldValuePair> pairs = new List<SqlFieldValuePair>();
            foreach(DataGridViewColumn column in dataGridView_AddRow.Columns)
            {
                int index = column.Index;
                if(row.Cells[index].Value != null)
                    pairs.Add(new SqlFieldValuePair(
                        new KeyValuePair<string, object>(column.Name, row.Cells[index]), SqlCompareOperator.Equal));
            }
            return SqlCommandCreater.CreateInsertCommand(CurrentTable.Name, pairs);
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

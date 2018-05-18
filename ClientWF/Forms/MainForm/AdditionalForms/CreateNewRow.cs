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

namespace ClientWF.AddtionalForms
{
    partial class CreateNewRow : Form
    {

        SQLExecuter Executer;
        string TableName;

        public CreateNewRow()
        {
            InitializeComponent();
        }

        public CreateNewRow(SQLExecuter executer, string tableName, DataGridView dataGridView, Dependences dependences)
        {
            InitializeComponent();
            Executer = executer;
            TableName = tableName;
            DialogResult = DialogResult.Cancel;
            string columnName;
            for(int i = 0; i < dataGridView.ColumnCount; i++)
            {
                columnName = dataGridView.Columns[i].Name;
                if (dependences.Dependence.ContainsKey(columnName))
                {
                    dataGridView_AddRow.Columns.Add(
                        new DataGridViewComboBoxColumn()
                        {
                            DataSource = dependences.Dependence[columnName]
                        });
                }
                else
                {
                    dataGridView_AddRow.Columns.Add(dataGridView.Columns[i]);
                }
            }
        }

        private void button_AddRow_Click(object sender, EventArgs e)
        {
            string command = CreateCommand();
            QueryJson query = new QueryJson(Code.OperationCode.INSERT, command);
            Executer.ApplyCommand<QueryJson>(query, 
                (answr) => MessageBox.Show(answr.Message));
        }

        private string CreateCommand()
        {
            string result = $"INSERT INTO {TableName} (";
            string values = "VALUES (";
            var row = dataGridView_AddRow.Rows[0];
            bool first = true;
            foreach(DataGridViewColumn column in dataGridView_AddRow.Columns)
            {
                int index = column.Index;
                if(row.Cells[index].Value.ToString() != "")
                {
                    if (first)
                        first = false;
                    else
                    {
                        result += " , ";
                        values += " , ";
                    }
                        
                    result += column.Name;
                    values += row.Cells[index].Value;
                }
            }
            return result + ")" + values + ")";
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

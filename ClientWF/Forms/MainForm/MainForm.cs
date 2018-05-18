using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ServerClientClassLibrary;
using ServerClientClassLibrary.JSONTypes;
using System.Xml;
using Newtonsoft.Json;


namespace ClientWF
{
    public partial class MainForm : Form
    {
        SQLExecuter SQLExecuter;
        CurrentTable CurrentTable;
        BindingSource Binding = new BindingSource();

        public MainForm()
        {
            
            ConnectForm connectForm = new ConnectForm();
            if (connectForm.ShowDialog() != DialogResult.OK)
            {
                connectForm.Dispose();
                Close();    
            }
            InitializeComponent();
            SQLExecuter = connectForm.Executer;
            DataGridView_MainView.DataSource = Binding;
        }

        public void PrintOnError(string msg)
        {
            MessageBox.Show(msg);
        }

        public void PrintUserData(GenUserDataJson js)
        {
            GenerateUsersResult gnr = new GenerateUsersResult(js);
            gnr.ShowDialog(this);
        }

        public void PrintDataGridViewDataTable(DataTableJson data)
        {
            DataTable dataTable = new DataTable();
            StringBuilder xmlString = new StringBuilder();
            var reader = XmlReader.Create(data.DataTable);
            dataTable.ReadXml(reader);
            Binding.DataSource = dataTable;
            DataGridView_MainView.AutoResizeColumns(
                DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }

        public void PrintDataGridView(SelectJson js)
        {
            DataGridView_MainView.Rows.Clear();
            DataGridView_MainView.Columns.Clear();
            foreach (var t in js.TableSchema.Columns)
                DataGridView_MainView.Columns.Add(t.Name, t.Name);
            int i = 0;
            foreach (var r in js.Rows)
                DataGridView_MainView.Rows.Insert(i++, r.Row.ToArray());
        }

        public void PrintListTables(SelectJson js)
        {
            CurrentTable.Dependences = js.Dependence;
            ListBox_Tables.Items.Clear();
                foreach (var t in js.Rows)
                    ListBox_Tables.Items.Add(t.Row[0]);
        }

        private void ButtonGetTableList_Click(object sender, EventArgs e)
        {
            GetTableList();
        }

        private void GetTableList()
        {
            SQLExecuter.ApplyCommand<SelectJson>(
                new QueryJson(Code.OperationCode.SELECT, "SELECT name FROM dbo.sysobjects where xtype = 'U' order by name"),
                PrintListTables);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SQLExecuter.ApplyCommand(new QueryJson(Code.OperationCode.ConnectionRefuse, ""));
        }

        private void ListBox_Tables_DoubleClick(object sender, EventArgs e)
        {
            var table = ListBox_Tables.Items[ListBox_Tables.SelectedIndex].ToString();
            SQLExecuter.ApplyCommand<DataTableJson>(
                new QueryJson(Code.OperationCode.SELECT, "SELECT * FROM " + table)
                {
                    TableName = table
                },
                PrintDataGridViewDataTable);
            CurrentTable.Name = table;
            textBox_CurrentTable.Text = table;
        }

        private void GenUserDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateUsers gu = new GenerateUsers();
            if(gu.ShowDialog(this) == DialogResult.OK)
            {
                SQLExecuter.ApplyCommand<GenUserDataJson>(
                    new QueryJson(Code.OperationCode.GenerateUserData, gu.textBox_Input.Text),
                        PrintUserData);
            }
        }

        private void DataGridView_MainView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = ((DataGridView)sender).Rows[e.RowIndex];
            var cols = ((DataGridView)sender).Columns;
            QueryJson msg = new QueryJson()
            {
                Code = Code.OperationCode.UPDATE,
                TableName = CurrentTable.Name,  
                Message =
                    $"UPDATE {CurrentTable} " +
                    $"SET {cols[e.ColumnIndex].Name}={row.Cells[e.ColumnIndex].Value} " +
                    $"WHERE {cols[0].Name}={row.Cells[0].Value}"
            };
            SQLExecuter.ApplyCommand<QueryJson>(msg, null);
        }

        private void button_CreateNewRow_Click(object sender, EventArgs e)
        {
            new AddtionalForms.CreateNewRow(SQLExecuter, 
                                            CurrentTable.Name, 
                                            DataGridView_MainView, 
                                            CurrentTable.Dependences)
                                            .ShowDialog();
        }
    }
}

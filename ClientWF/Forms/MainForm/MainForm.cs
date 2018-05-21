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
using System.IO;

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
            }
            InitializeComponent();
            SQLExecuter = connectForm.Executer;
            CurrentTable = new CurrentTable();
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
            CurrentTable.Dependences = data.Dependence;
            DataSet dataSet = new DataSet();
            StringReader xmlreader = new StringReader(data.DataTable);
            dataSet.ReadXml(xmlreader);
            CurrentTable.CurTable = dataSet.Tables[CurrentTable.Name];
            Binding.DataSource = CurrentTable.CurTable;
            DataGridView_MainView.AutoResizeColumns(
                DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewColumn c in DataGridView_MainView.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void PrintListTables(string tableName, DataTableJson dt)
        {
            ListBox_Tables.Items.Clear();
            DataTable dataTable = DataTableJson.Deserialize(tableName, dt);
            foreach (DataRow row in dataTable.Rows)
                ListBox_Tables.Items.Add(row[0].ToString());
        }

        private void ButtonGetTableList_Click(object sender, EventArgs e)
        {
            GetTableList();
        }

        private void GetTableList()
        {
            string tableName = "sysobjects";
            var dataTable = SQLExecuter.ApplyCommand<DataTableJson>(
                new QueryJson(OperationCode.SELECT, 
                "SELECT name FROM dbo.sysobjects where xtype='U' order by name")
                {
                    TableName = tableName,
                });
            PrintListTables(tableName, dataTable);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SQLExecuter.ApplyCommand(new QueryJson(OperationCode.ConnectionRefuse, ""));
        }

        private void ListBox_Tables_DoubleClick(object sender, EventArgs e)
        {
            var table = ListBox_Tables.Items[ListBox_Tables.SelectedIndex].ToString();
            CurrentTable.Name = table;
            textBox_CurrentTable.Text = table;
            var dataTable = SQLExecuter.ApplyCommand<DataTableJson>(
                new QueryJson(OperationCode.SELECT, "SELECT * FROM " + table)
                {
                    TableName = table
                });
            PrintDataGridViewDataTable(dataTable);
        }

        private void GenUserDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateUsers gu = new GenerateUsers();
            if(gu.ShowDialog(this) == DialogResult.OK)
            {
                var userData = SQLExecuter.ApplyCommand<GenUserDataJson>(
                    new QueryJson(OperationCode.GenerateUserData, "Count=" + gu.textBox_Input.Text));
                PrintUserData(userData);
            }
        }

        private void button_CreateNewRow_Click(object sender, EventArgs e)
        {
            new AddtionalForms.CreateNewRow(SQLExecuter, 
                                            DataGridView_MainView, 
                                            CurrentTable)
                                            .ShowDialog();
        }

        private void DataGridView_MainView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataTable currentTable = CurrentTable.CurTable;
            if (CurrentTable.CompareValues(currentTable.Rows[e.RowIndex][e.ColumnIndex]))
            {
                DataGridView_MainView.Rows[e.RowIndex]
                    .Cells[e.ColumnIndex].Style.BackColor = Color.White;
                return;
            }
            DataGridView_MainView.Rows[e.RowIndex]
                .Cells[e.ColumnIndex].Style.BackColor = Color.LightGray;
            CurrentTable.UpdateCell(e.RowIndex, 
                currentTable.Columns[e.ColumnIndex].ColumnName, 
                currentTable.Columns[e.ColumnIndex].DataType,
                currentTable.Rows[e.RowIndex][e.ColumnIndex]);
        }

        private void DataGridView_MainView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CurrentTable.SetTempCellValue(CurrentTable.CurTable.Rows[e.RowIndex][e.ColumnIndex]);
        }

        private void button_ReturnValues_Click(object sender, EventArgs e)
        {
            CurrentTable.ReturnAllCellsToValue(DataGridView_MainView);
        }

        private void button_UpdateRows_Click(object sender, EventArgs e)
        {
            foreach (string l in CurrentTable.UpdateRows())
                Console.WriteLine(l);

        }

        private void DataGridView_MainView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            e.ContextMenuStrip = new ContextMenuStrip();
            e.ContextMenuStrip.Items.Add("Вернуть значения для этой строки", null, (obj, args) => 
            {
                CurrentTable.ReturnRowValue(
                    DataGridView_MainView,
                    e.RowIndex);
            });
            e.ContextMenuStrip.Items.Add("Вернуть значение для этой ячейки", null, (obj, args) => 
            {
                CurrentTable.ReturnCellValue(
                    DataGridView_MainView, 
                    e.RowIndex, 
                    CurrentTable.CurTable.Columns[e.ColumnIndex].ColumnName);
            });
            e.ContextMenuStrip.Show();
        }

    }
}

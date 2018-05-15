﻿using System;
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
using Newtonsoft.Json;


namespace ClientWF
{
    public partial class MainForm : Form
    {
        SQLExecuter SQLExecuter;

        public MainForm(IODialog iodialog)
        {
            InitializeComponent();
            SQLExecuter = new SQLExecuter(iodialog, PrintOnError);
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Добавить строку", null, AddRow_Click);
            cms.Items.Add("Обновить строку по условию", null, AddRowWithCondition_Click);
            cms.Items.Add("Обновить строки", null, UpdateRow_Click);
            DataGridView_MainView.ContextMenuStrip = cms;
        }

        #region DataGridContexMenu

        private void AddRow_Click(object o, EventArgs e)
        {

        } 

        private void AddRowWithCondition_Click(object o, EventArgs e)
        {

        } 

        private void UpdateRow_Click(object o, EventArgs e)
        {

        } 

        #endregion

        public void PrintOnError(string msg)
        {
            MessageBox.Show(msg);
        }

        public void PrintUserData(GenUserDataJson js)
        {
            GenerateUsersResult gnr = new GenerateUsersResult(js);
            gnr.ShowDialog(this);
        }

        public void PrintDataGridView(SelectJson js)
        {
            DataGridView_MainView.Rows.Clear();
            DataGridView_MainView.Columns.Clear();
            foreach (var t in js.ColumnName)
                DataGridView_MainView.Columns.Add(t, t);
            int i = 0;
            foreach (var r in js.Rows)
                DataGridView_MainView.Rows.Insert(i++, r.Row.ToArray());
        }

        public void PrintListTables(SelectJson js)
        {
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
            SQLExecuter.ApplyCommand<SelectJson>(
                new QueryJson(Code.OperationCode.SELECT, "SELECT * FROM " + table)
                {
                    TableName = table
                },
                PrintDataGridView);
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
            // SQLExecuter.ApplyCommand();
        }
    }
}

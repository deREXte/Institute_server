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
using System.IO;

namespace ClientWF
{
    public partial class GenerateUsersResult : Form
    {
        public GenerateUsersResult(GenUserDataJson js)
        {
            InitializeComponent();
            richTextBox.Text = "Login\t\tPassword\n";
            foreach(var j in js.Users)
            {
                richTextBox.Text += j.Login + "\t" + j.Password + Environment.NewLine;
            } 
        }

        private void button_SaveInFile_Click(object sender, EventArgs e)
        {
            Stream stream;
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if((stream = saveFileDialog.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(stream);
                    sw.Write(richTextBox.Text);
                    sw.Close();
                }
            }
            DialogResult = DialogResult.OK;
        }
    }
}

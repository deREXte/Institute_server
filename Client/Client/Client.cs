using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1024];
            Concat(0, buffer, Encoding.UTF8.GetBytes(textBox1.Text));
            Data.connection.Send(buffer, 1024, SocketFlags.None);
        }

        private void Concat(int n, byte[] to, byte[] from)
        {
            int tolength = to.Length;
            int fromlength = from.Length;
            for (int i = 0; n < tolength && i < fromlength; i++, n++)
            {
                to[n] = from[i];
            }
        }
    }
}

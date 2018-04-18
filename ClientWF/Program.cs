using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConnectForm authof = new ConnectForm();
            Application.Run(authof);
            ServerClientClassLibrary.IODialog handler = authof.GetInformation();
            if(handler == null)
            {
                authof.Dispose();
                return;
            }
            else
            {
                Application.Run(new MainForm(handler));
            }
        }
    }
}

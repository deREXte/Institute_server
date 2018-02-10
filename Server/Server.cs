using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using System.Collections.Specialized;


namespace Server
{
    class Server
    {
        Timer TimerRetryConnectToDB;
        int RetryConnectionTime;

        public void StartServer()
        {
            DataBaseOperations.InitConnectionToDB();
            if (!DataBaseOperations.Connected)
            {
                // нужно добавить дефолт
                RetryConnectionTime = int.Parse(ConfigurationManager.AppSettings.Get("RetryConnectionTime"));
                Console.WriteLine(RetryConnectionTime);
                Console.Read();
                TimerRetryConnectToDB = new Timer(RetryConnectionTime); // 1 min
                TimerRetryConnectToDB.Elapsed += RetryConnectToDB;
            }
            new Listener().StartListen();
        }


        private void RetryConnectToDB(object sender, EventArgs e)
        {
            DataBaseOperations.InitConnectionToDB();
            if(DataBaseOperations.Connected)
                TimerRetryConnectToDB.Close();
        }
    }
}

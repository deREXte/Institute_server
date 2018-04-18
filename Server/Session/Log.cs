using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace Server
{
    class Log
    {
        static string SavePath = ConfigurationManager.AppSettings.Get("SavePathLogFiles");

        StreamWriter LogFile;
        string FullSavePath;
        bool FileOpened;
        
        public string UserName
        {
            get;
            private set;
        }

        public Log(string userName)
        {
            UserName = userName;
            FullSavePath = SavePath + UserName + ".txt";
            OpenLogFile();
            LogFile.AutoFlush = true;
        }

        void OpenLogFile()
        {
            if (!File.Exists(FullSavePath)) 
                File.Create(FullSavePath).Close();
            LogFile = new StreamWriter(FullSavePath, true);
            FileOpened = true;
        }

        public void Write(string text)
        {
            DateTime dateTime = DateTime.Now;
            LogFile.WriteLine("[" + dateTime.GetDateTimeFormats('g')[0] + "] " + text);
        }

        public void CloseLogFile()
        {
            LogFile.Close();
        }
    }
}

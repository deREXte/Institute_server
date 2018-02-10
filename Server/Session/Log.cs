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
        string UserName;
        string FullSavePath;
        bool FileOpened;
        
        public Log(string userName)
        {
            UserName = userName;
            FullSavePath = SavePath + UserName + ".txt";
            OpenLogFile();
        }

        void OpenLogFile()
        {
            if (!File.Exists(FullSavePath)) 
                File.Create(FullSavePath).Close();
            try
            {
                LogFile = new StreamWriter(FullSavePath, true);
            }
            catch (IOException)
            {
                return;
            }
            FileOpened = true;
        }

        public void Write(string text)
        {
            LogFile.WriteLine(text);
        }

        public void Write(byte[] text)
        {
            LogFile.WriteLine(Encoding.UTF8.GetString(text));
        }

        public void CloseLogFile()
        {
            LogFile.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    class Log
    {

        private StreamWriter _file;

        private bool _file_is_opened;
        public bool FileIsOpened
        {
            get { return _file_is_opened; }
        }


        public Log(string Name)
        {
            if(createFile(StartApp.Default.LogFilePath + "/" + Name))
            {
                _file_is_opened = false;
            }
        }

        public void Write(string text)
        {
            string date = "[" + DateTime.Today.Year + ":" +
                DateTime.Today.Month + ":" +
                DateTime.Today.Day + "|" +
                DateTime.Today.Hour + ":" +
                DateTime.Today.Minute + ":" +
                DateTime.Today.Second + "] ";
            _file.WriteLine(date + text);
        }

        public bool OpenLogFile(string pathAndName)
        {
            if (File.Exists(pathAndName))
            {
                if (_file_is_opened)
                {
                    _file.Flush();
                    _file.Close();
                    if (!createFile(pathAndName))
                    {
                        _file_is_opened = false;
                        return false;
                    }

                }
                else
                {
                    if(!createFile(pathAndName))
                    {
                        _file_is_opened = false;
                        return false;
                    }
                }
            }
            else
            {
                if (!createFile(pathAndName))
                {
                    _file_is_opened = false;
                    return false;
                }
            }

            _file_is_opened = true;
            return true;
        }

        private bool createFile(string pathAndName)
        {
            try
            {
                _file = new StreamWriter(pathAndName + "log.txt");
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace Server
{

    class ClientException : Exception
    {

        bool _is_null;
        public bool IsNull
        {
            get { return _is_null; }
        }

        string _additional_information;
        public string AdditionalInformation
        {
            get { return _additional_information; }
            set {
                _is_null = false;
                _additional_information = value;
            }
        }

        public ClientException()
        {
            _additional_information = "";
        }
        
        public void Clear()
        {
            _additional_information = "";
            _is_null = true;
        }

    }

    class NewClient
    {

        string _login_name;

        ClientException error;
        Log log;
        Socket handler;
        Thread thread;

        public NewClient()
        {
            thread = Thread.CurrentThread;
            error = new ClientException();
        }

        public NewClient(Socket handle)
        {
            handler = handle;
            thread = Thread.CurrentThread;
            error = new ClientException();
        }

        ~NewClient()
        {
            if (log != null)
            {
                if (!error.IsNull)
                    log.Write(error.Message + ":" + error.AdditionalInformation);
                log.Flush();
            }
            if(handler.Connected)
                handler.Close();
        }

        public void StartListen()
        {
            handler.ReceiveTimeout = 1000;
            if (!CheckClient())
                return;

            handler.ReceiveTimeout = 150000;
            if (!Authorization())
                return;

            log = new Log(_login_name);
            log.Write("User " + _login_name + " was joined.");
            handler.ReceiveTimeout = 1500000;
            receiveCommands();
        }

        private void receiveCommands()
        {
            byte[] buffer = new byte[1024];
            string cmd;
            int received_msgs;
            while (true)
            {
                cmd = "";
                received_msgs = 0;
                while (received_msgs < 25) {
                    try
                    {
                        handler.Receive(buffer, 1024, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(Thread.CurrentThread + ":" + ex.Message);
                        return;
                    }
                    received_msgs++;
                    cmd += Encoding.UTF8.GetString(buffer);
                    int index = cmd.IndexOf('\0');
                    if(index != -1)
                    {
                        cmd = cmd.Substring(0, index);
                        break;
                    }
                }
                var ien = DataBaseOperations.Execute(cmd);
                while (ien.MoveNext())
                {
                    object obj = ien.Current;
                    Console.WriteLine(obj.ToString());
                }
                try
                {
                    log.Write(cmd);
                }
                catch (Exception)
                {
                    log = new Log(_login_name);
                    log.Write(cmd);
                }

            }
        }

        private bool Authorization()
        {
            string login = "", password = "";
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    handler.Receive(buffer, 1024, SocketFlags.None);
                }catch(Exception e)
                {
                    error = (ClientException)e;
                    error.AdditionalInformation = "While authorization";
                    return false;
                }
                string bufpass;
                int logLength = buffer[0], passlength = buffer[1];
                string buf = Encoding.UTF8.GetString(buffer);
                string bufferlog = buf.Substring(2, logLength);
                if(bufferlog != login)
                {
                    password = DataBaseOperations.GetUserPassword(bufferlog);
                    login = bufferlog;
                }
                bufpass = buf.Substring(logLength + 2, passlength);
                if(bufpass == password)
                {
                    handler.Send(new byte[1] { 101 }, 1, SocketFlags.None);
                    _login_name = login;
                    return true;
                }
            }
        }



        private bool CheckClient()
        {
            int ins = DateTime.Today.Minute;
            int orse = DateTime.Today.Hour;
            int ppp = DateTime.Today.Day * DateTime.Today.Month * DateTime.Today.Year;
            ppp += ((ppp & 1) == 1) ? 1 : 0;
            byte[] len = new byte[1];
            int leng;
            handler.Receive(len, 1, SocketFlags.None);
            leng = int.Parse(Encoding.UTF8.GetString(len)) + 7;
            byte[] buffer = new byte[leng];
            handler.Receive(buffer, leng, SocketFlags.None);
            handler.Receive(len, 1, SocketFlags.None);
            leng = int.Parse(Encoding.UTF8.GetString(len)) + 7;
            byte[] buffer2 = new byte[leng];
            handler.Receive(buffer2, leng, SocketFlags.None);
            string n = Encoding.UTF8.GetString(buffer);
            string h = "";
            for(int i = 0,length = n.Length; i < length; i++)
            {
                h += (char)(n[i] + orse - ins);
            }
            long par = 0;
            if(!Int64.TryParse(h,out par))
            {
                return false;
            }
            par = ((((par - 4) / 2) + 10) / ppp);
            n = Encoding.UTF8.GetString(buffer2);
            h = "";
            for (int i = 0, length = n.Length; i < length; i++)
            {
                h += (char)(n[i] + orse - ins);
            }
            long l = 0;
            if (!Int64.TryParse(h, out l))
            {
                return false;
            }
            l = ((((l - 4) / 2) + 10) / ppp);
            if (l + par == ppp)
            {
                Console.WriteLine("OK");
                handler.Send(Encoding.UTF8.GetBytes("OK"), 2, SocketFlags.None);
                return true;
            }
            else
            {
                Console.WriteLine("NOPE");
                return false;
            }
        }


    }
}

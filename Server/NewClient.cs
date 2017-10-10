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


    class MThreadAbortedException : Exception
    {
        public new string Message
        {
            get;
            private set;
        }

        public MThreadAbortedException(string text)
        {
            Message = text;
        }

        public MThreadAbortedException()
        {
            Message = "Unknown error!";
        }
    }

    class NewClient
    {
        Thread thread;
        MThreadAbortedException threadAbortException;
        Socket handler;
        Timer checkConnection;

        public NewClient(Thread thread,Socket handler)
        {
            this.thread = thread;
            this.handler = handler;
            checkConnection = new Timer(new TimerCallback(Ping), new AutoResetEvent(false), 10000, 60000);
            this.handler.SendTimeout = -1;
            this.handler.ReceiveTimeout = -1;
        }

        public void StartListen()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    handler.Receive(buffer, buffer.Length, SocketFlags.None);
                    Console.WriteLine("1");
                }
                catch (Exception) { }
            }
        }

        public bool IsAlive()
        {
            return Thread.CurrentThread.IsAlive;
        }

        void Ping(object obj)
        {
            if (handler == null)
                AbortThread("Connection lost");
            Console.WriteLine("Ping");
            var autoEvent = new AutoResetEvent(false);
            Timer timer = new Timer(new TimerCallback(AbortThread), autoEvent, 25000, 25000);
            byte[] buffer = new byte[] { 99 };
            try
            {
                handler.Send(buffer, buffer.Length, SocketFlags.None);
                handler.Receive(buffer, buffer.Length, SocketFlags.None);
                Console.WriteLine("2");
            }
            catch(Exception)
            {
                return;
            }
            timer.Dispose();
        }

        void AbortThread(string text)
        {
            threadAbortException = new MThreadAbortedException(text);
            exitThread(threadAbortException);
        }
        
        void AbortThread()
        {
            threadAbortException = new MThreadAbortedException("Thread was aborted!");
            exitThread(threadAbortException);
        }

        void AbortThread(object obj)
        {
            threadAbortException = new MThreadAbortedException("Thread was aborted!");
            exitThread(threadAbortException);
        }

        void AbortThread(Exception ex)
        {
            threadAbortException = new MThreadAbortedException(ex.Message);
            exitThread(threadAbortException);
        }

        void exitThread(MThreadAbortedException ex)
        {
            Console.WriteLine("Exit thread");
            checkConnection.Dispose();         
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            thread.Abort();
        }

    }
}

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
            checkConnection = new Timer(new TimerCallback(Ping), new AutoResetEvent(false), 10000, 45000);
        }

        void StartListen()
        {
            
        }

        public bool IsAlive()
        {
            return Thread.CurrentThread.IsAlive;
        }

        void Ping(object obj)
        {
            if (handler == null)
                AbortThread("Connection lost");
            var autoEvent = new AutoResetEvent(false);
            Timer timer = new Timer(new TimerCallback(AbortThread), autoEvent, 25000, 25000);
            byte[] buffer = new byte[] { 99 };
            try
            {
                handler.Send(buffer, buffer.Length, SocketFlags.None);
                handler.Receive(buffer, buffer.Length, SocketFlags.None);
            }
            catch(Exception ex)
            {
                timer.Dispose();
                AbortThread(ex);
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
            checkConnection.Dispose();         
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            thread.Abort();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class MTask
    {
        public MTask()
        {
            value = new Queue<string>();
            e = new EventResetMode();
            handle = new EventWaitHandle(false, e);
        }

        public Queue<string> value;
        public EventResetMode e;
        public EventWaitHandle handle;
    } 

    class Receiver
    {
        Dictionary<string,MTask> tasks;
        Socket handler;
        Thread thread;

        public Receiver(Socket handler)
        {
            tasks = new Dictionary<string,MTask>();
            this.handler = handler;
        }

        public Receiver(Socket handler,string key,string command)
        {
            tasks = new Dictionary<string,MTask>();
            Add(key, command);
            this.handler = handler;
        }

        public string GetValue(EventWaitHandle handle,string tkey)
        {
            handle.WaitOne();
            handle.Reset();
            return tasks[tkey].value.Dequeue();
        }

        public void Abort()
        {
            thread.Abort();
        }

        void start()
        {
            thread = new Thread(StartReceive);
            thread.Start();
        }

        public void Add(string key,string command)
        {
            tasks.Add(key,new MTask());
        }

        void StartReceive()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                handler.Receive(buffer, 1024, SocketFlags.None);
                string line = Encoding.UTF8.GetString(buffer);
                string[] command = line.Split('|'); 
                tasks[command[0]].value.Enqueue(command[1]);
                tasks[command[0]].handle.Set();
            }
        }

    }
}

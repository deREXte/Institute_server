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
        public MTask(string command)
        {
            this.command = command;
            value = new Queue<string>();
        }

        public string command;
        public Queue<string> value;
    } 

    class Receiver
    {
        List<MTask> tasks;
        Socket handler;
        Thread thread;

        public Receiver(Socket handler)
        {
            tasks = new List<MTask>();
            this.handler = handler;
        }

        public Receiver(Socket handler,string command)
        {
            tasks = new List<MTask>();
            Add(command);
            this.handler = handler;
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

        public void Add(string command)
        {
            tasks.Add(new MTask(command));
        }

        void StartReceive()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                handler.Receive(buffer, 1024, SocketFlags.None);
                string line = Encoding.UTF8.GetString(buffer);
                string command = line.Substring(0, line.IndexOf(' '));
                for (int i = 0, length = tasks.Count; i < length; i++)
                {
                    if (tasks[i].command == command)
                        tasks[i].value.Enqueue(command);
                }
            }
        }

    }
}

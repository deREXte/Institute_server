﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    public partial class AuthorizationForm : Form
    {
        Socket sender;

        public AuthorizationForm()
        {
            InitializeComponent();
            byte[] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.GetHostEntry("192.168.1.33");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 55000);

            // Create a TCP/IP  socket.
            Data.connection = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Data.connection.Connect(remoteEP);
                Data.connection.ReceiveTimeout = -1;
                Data.connection.SendTimeout = -1;
                /*MessageBox.Show("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());*/
                if (function())
                    Console.WriteLine("OK");

            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Unexpected exception : {0}", e.ToString());
            }
        }

        bool function()
        {
            int ins = DateTime.Today.Minute;
            int orse = DateTime.Today.Hour;
            int ppp = DateTime.Today.Day * DateTime.Today.Month * DateTime.Today.Year;
            //int ppp = 31 * 12 * 2100;
            //int ppp = 1 * 1 * 2017;
            ppp += ((ppp & 1) == 1) ? 1 : 0;
            long n1, n2;
            n1 = n2 = ppp / 2;
            Random r = new Random(ppp);
            int ran = r.Next(ppp / 2 / 2 - 1);
            if (((ran & 1) == 1))
            {
                n1 -= ran;
                n2 += ran;
            }
            else
            {
                n2 -= ran;
                n1 += ran;
            }
            n1 = ((((n1 * ppp) - 10) * 2) + 4);
            n2 = ((((n2 * ppp) - 10) * 2) + 4);
            string bn1 = n1.ToString(),bn2 = n2.ToString();
            string resbn1 = "", resbn2 = "";
            for(int i = 0, length = bn1.Length; i < length; i++)
            {
                resbn1 += (char)(bn1[i] + ins - orse);
            }
            for (int i = 0, length = bn2.Length; i < length; i++)
            {
                resbn2 += (char)(bn2[i] + ins - orse);
            }
            Data.connection.Send(Encoding.UTF8.GetBytes((resbn1.Length - 7).ToString()), 1, SocketFlags.None);
            Data.connection.Send(Encoding.UTF8.GetBytes(resbn1), resbn1.Length, SocketFlags.None);
            Data.connection.Send(Encoding.UTF8.GetBytes((resbn2.Length - 7).ToString()), 1, SocketFlags.None);
            Data.connection.Send(Encoding.UTF8.GetBytes(resbn2), resbn2.Length, SocketFlags.None);
            byte[] answer = new byte[2];
            Data.connection.Receive(answer, 2, SocketFlags.None);
            if (Encoding.UTF8.GetString(answer) == "OK")
                return true; 
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string Password = textBox2.Text;
            byte[] buffer = new byte[1024];
            buffer[0] = (byte)login.Length;
            buffer[1] = (byte)Password.Length;
            Concat(2, buffer, Encoding.UTF8.GetBytes(login));
            Concat(buffer[0] + 2, buffer, Encoding.UTF8.GetBytes(Password));
            Data.connection.Send(buffer, 1024, SocketFlags.None);
            Data.connection.Receive(buffer, 1, SocketFlags.None);
            if(buffer[0] == 101)
            {
                Data.Authorized = true;
                Close();
            }
        }

        private void Concat(int n,byte[] to,byte[] from)
        {
            int tolength = to.Length;
            int fromlength = from.Length;
            for(int i = 0; n < tolength && i < fromlength; i++,n++)
            {
                to[n] = from[i];
            }
        }
    }
}

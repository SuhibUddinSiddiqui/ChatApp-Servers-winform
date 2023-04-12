using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp_Servers_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<TcpClient> clients = new List<TcpClient>();
        TcpListener server;
        private void button2_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            server = new TcpListener(IPAddress.Loopback, 8001);
            server.Start(10);
            Thread t2 = new Thread(AcceptClient);
            t2.Start();
        }
        public void AcceptClient()
        {
            while (true)
            {
                TcpClient c = server.AcceptTcpClient();
                clients.Add(c);
                Thread t = new Thread(asd => ReadMessage(c));
                t.Start();
            }
        }

        public void ReadMessage(TcpClient client)
        {
            while (true)
            {

                NetworkStream stream = client.GetStream();
                StreamReader sdr = new StreamReader(stream);
                string msg = sdr.ReadLine();
                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText("Client: " + msg);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in clients)
            {
                textBox1.AppendText(Environment.NewLine);
                textBox1.AppendText("Me: " + textBox3.Text);
                NetworkStream stream = item.GetStream();
                StreamWriter sdr = new StreamWriter(stream);
                sdr.WriteLine(textBox3.Text);
                sdr.Flush();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

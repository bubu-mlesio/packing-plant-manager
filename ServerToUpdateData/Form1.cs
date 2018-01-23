using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messages;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using Lidgren.Network;


//NOT WORK, GETTING WORK
namespace ServerToUpdateData
{
    public partial class Form1 : Form
    {
        Thread startOperations;
        int port = 2061;


        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
            //
            //Thread.Sleep(2000);
            //test();
            //connectConfigDownloader.RunWorkerAsync();
            //waitForConnections();
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            //ThreadStart ts = new ThreadStart(tcpConnect);
            //startOperations = new Thread(ts);
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            NetworkClient server = new NetworkClient();
            server.StartServer();
            server.ReadMessages();
            /*while ((message = server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        // handle custom messages
                        var data = message.ReadString();
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        // handle connection status messages
                        switch (message.SenderConnection.Status)
                        {
                            /* .. */
            /* }
             break;

         case NetIncomingMessageType.DebugMessage:
             // handle debug messages
             // (only received when compiled in DEBUG mode)
             Console.WriteLine(message.ReadString());
             break;

         /* .. */
            /* default:
                 Console.WriteLine("unhandled message with type: "
                     + message.MessageType);
                 break;
         }
     }*/
        }
    }

}

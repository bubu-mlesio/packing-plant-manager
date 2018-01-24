using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;


//NOT WORK, GETTING WORK
namespace ServerToUpdateData
{
    public partial class Form1 : Form
    {
        int PORT_NO = 2201;
        const string SERVER_IP = "127.0.0.1";
        static Socket serverSocket; //put here as static


        public Form1()
        {
            InitializeComponent();
            //backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            logbox.Items.Add("Nasłuchiwanie...");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT_NO));
            serverSocket.Listen(20); //the maximum pending client, define as you wish
            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);

        }
        private const int BUFFER_SIZE = 4096;
        private static byte[] buffer = new byte[BUFFER_SIZE]; //buffer size is limited to BUFFER_SIZE per message
        private static List<Socket> clientSockets = new List<Socket>(); //may be needed by you
        private void acceptCallback(IAsyncResult result)
        { //if the buffer is old, then there might already be something there...
            System.Net.Sockets.Socket socket = null;
            try
            {
                socket = serverSocket.EndAccept(result); // To get your client socket
                clientSockets.Add(socket); //may be needed later
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket);
                serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null); //to receive another client

            }
            catch (Exception e)
            { // this exception will happen when "this" is be disposed...        
                logbox.Items.Add("BŁĄD: " + e.ToString());
            }
        }
        const int MAX_RECEIVE_ATTEMPT = 10;
        static int receiveAttempt = 0; //this is not fool proof, obviously, since actually you must have multiple of this for multiple clients, but for the sake of simplicity I put this
        private void receiveCallback(IAsyncResult result)
        {
            Socket socket = null;
            try
            {
                socket = (Socket)result.AsyncState; //this is to get the sender
                if (socket.Connected)
                { //simple checking
                    int received = socket.EndReceive(result);
                    if (received > 0)
                    {
                        byte[] data = new byte[received]; //the data is in the byte[] format, not string!
                        Buffer.BlockCopy(buffer, 0, data, 0, data.Length); //There are several way to do this according to https://stackoverflow.com/questions/5099604/any-faster-way-of-copying-arrays-in-c in general, System.Buffer.memcpyimpl is the fastest
                        if (Encoding.UTF8.GetString(data) == "hello")//DO SOMETHING ON THE DATA IN byte[] data!! Yihaa!!
                        {
                            string msg = "OK";
                            socket.Send(Encoding.ASCII.GetBytes(msg)); //Note that you actually send data in byte[]
                        }
                        if (Encoding.UTF8.GetString(data) == "Requier")//DO SOMETHING ON THE DATA IN byte[] data!! Yihaa!!
                        {
                            string msg = server.Text;
                            socket.Send(Encoding.ASCII.GetBytes(msg)); //Note that you actually send data in byte[]
                        }
                        //Console.WriteLine(Encoding.UTF8.GetString(data)); //Here I just print it, but you need to do something else
                        receiveAttempt = 0; //reset receive attempt
                        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket); //repeat beginReceive
                    }
                    else if (receiveAttempt < MAX_RECEIVE_ATTEMPT)
                    { //fail but not exceeding max attempt, repeats
                        ++receiveAttempt; //increase receive attempt;
                        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), socket); //repeat beginReceive
                    }
                    else
                    { //completely fails!
                        logbox.Items.Add("receiveCallback error");
                        
                        receiveAttempt = 0; //reset this for the next connection
                    }
                }
                else
                {
                    logbox.Items.Add("Disconnect");
                }
            }
            catch (Exception e)
            { // this exception will happen when "this" is be disposed...
                logbox.Items.Add("receiveCallback fails with exception! " + e.ToString());
            }
        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            try
            {
                PORT_NO = Int32.Parse(port_number.Text);
                if (PORT_NO < 65536)
                {
                    if (PORT_NO < 0)
                    {
                        PORT_NO = 2201;
                        logbox.Items.Add("Problem z portem, używam 2201");
                    }
                }
                else
                {
                    PORT_NO = 2201;
                    logbox.Items.Add("Problem z portem, używam 2201");
                }
                    
                }
            catch
            {
                PORT_NO = 2201;
                logbox.Items.Add("Problem z portem, używam 2201");
            }
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            logbox.Items.Add("Przerywam nasłuchiwanie");
            backgroundWorker1.CancelAsync();
        }
    }

}

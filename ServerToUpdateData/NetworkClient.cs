using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerToUpdateData
{
    class NetworkClient
    {
        private NetServer server;
        private List<NetPeer> clients;

        public void StartServer()
        {
            var config = new NetPeerConfiguration("hej") { Port = 14242 };
            server = new NetServer(config);
            server.Start();

            if (server.Status == NetPeerStatus.Running)
            {
                MessageBox.Show("Server is running on port " + config.Port, "404", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Server not started...", "404", MessageBoxButtons.OK);
            }
            clients = new List<NetPeer>();
        }

        public void ReadMessages()
        {
            NetIncomingMessage message;
            var stop = false;

            while (!stop)
            {
                while ((message = server.ReadMessage()) != null)
                {
                    switch (message.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            {
                                MessageBox.Show("I got smth!", "404", MessageBoxButtons.OK);
                                var data = message.ReadString();
                                MessageBox.Show(data, "404", MessageBoxButtons.OK);

                                if (data == "exit")
                                {
                                    stop = true;
                                }

                                break;
                            }
                        case NetIncomingMessageType.DebugMessage:
                            MessageBox.Show(message.ReadString());
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            MessageBox.Show(message.SenderConnection.Status.ToString(), "404", MessageBoxButtons.OK);
                            if (message.SenderConnection.Status == NetConnectionStatus.Connected)
                            {
                                clients.Add(message.SenderConnection.Peer);
                                MessageBox.Show(message.SenderConnection.Peer.Configuration.LocalAddress + " has connected.", "404", MessageBoxButtons.OK);
                            }
                            if (message.SenderConnection.Status == NetConnectionStatus.Disconnected)
                            {
                                clients.Remove(message.SenderConnection.Peer);
                                MessageBox.Show(message.SenderConnection.Peer.Configuration.LocalAddress + " has disconnected.", "404", MessageBoxButtons.OK);
                            }
                            break;
                        default:
                            MessageBox.Show("Unhandled message type: {message.MessageType}", "404", MessageBoxButtons.OK);
                            break;
                    }
                    server.Recycle(message);
                }
            }


        }
    }
}

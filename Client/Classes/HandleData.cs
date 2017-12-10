using Lidgren.Network;
using static System.Console;
using static System.Convert;

namespace Client.Classes
{
    public class HandleData
    {
        public string ipAddress;
        public int port;
        public int myIndex;

        public void HandleDataMessage(NetClient g_Client)
        {
            NetIncomingMessage incMSG;
            ipAddress = "10.16.1.174";
            port = 14242;

            if ((incMSG = g_Client.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG, g_Client);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Connection:
                                WriteLine("Connected to server!");
                                break;

                            case (byte)Packet.Login:

                                break;
                        }
                        break;
                }
            }
        }

        private void HandleDiscoveryResponse(NetIncomingMessage incMSG, NetClient g_Client)
        {
            WriteLine("Found Server: " + incMSG.ReadString() + " @ " + incMSG.SenderEndPoint);
            NetOutgoingMessage outMSG = g_Client.CreateMessage();
            outMSG.Write((byte)Packet.Connection);
            outMSG.Write("scorched");
            g_Client.Connect(ipAddress, ToInt32(port), outMSG);
        }
    }

    public enum Packet : byte
    {
        Connection,
        Register,
        Login,
        Error,
        Notification
    }
}

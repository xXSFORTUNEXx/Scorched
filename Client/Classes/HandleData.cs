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

        public void HandleDataMessage(NetClient g_Client, UserInterface g_UserInterface)
        {
            NetIncomingMessage incMSG;
            ipAddress = "10.16.0.2";
            port = 14242;

            if ((incMSG = g_Client.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        HandleDiscoveryResponse(incMSG, g_Client);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, g_Client);
                        break;

                    case NetIncomingMessageType.Data:
                        switch (incMSG.ReadByte())
                        {
                            case (byte)Packet.Connection:
                                Logging.WriteMessageLog("Connected to server!");
                                break;

                            case (byte)Packet.Login:
                                HandleLoginResponse(incMSG, g_Client, g_UserInterface);
                                break;
                        }
                        break;
                }
            }
        }

        private void HandleLoginResponse(NetIncomingMessage incMSG, NetClient g_Client, UserInterface g_UserInterface)
        {
            byte Response = incMSG.ReadByte();
            Logging.WriteMessageLog("Successful login! Response:" + Response, true, g_Client.ServerConnection.ToString());
            g_UserInterface.CharactersMenu();
        }

        private void HandleDiscoveryResponse(NetIncomingMessage incMSG, NetClient g_Client)
        {
            Logging.WriteMessageLog("Found Server: " + incMSG.ReadString() + " @ " + incMSG.SenderEndPoint);
            NetOutgoingMessage outMSG = g_Client.CreateMessage();
            outMSG.Write((byte)Packet.Connection);
            outMSG.Write("scorched");
            g_Client.Connect(ipAddress, ToInt32(port), outMSG);
        }

        private void HandleStatusChange(NetIncomingMessage incMSG, NetClient g_Client)
        {
            Logging.WriteMessageLog(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
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

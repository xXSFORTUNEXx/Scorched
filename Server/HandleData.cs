using Lidgren.Network;
using static System.Console;
using static System.Threading.Thread;

namespace Server
{
    public class HandleData
    {
        public void HandleDataMessage(NetServer g_Server)
        {
            NetIncomingMessage incMSG;

            if ((incMSG = g_Server.ReadMessage()) != null)
            {
                switch (incMSG.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        HandleDiscoveryRequest(incMSG, g_Server);
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApproval(incMSG, g_Server);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(incMSG, g_Server);
                        break;

                    case NetIncomingMessageType.Data:
                        switch(incMSG.ReadByte())
                        {

                        }
                        break;
                }
            }
            g_Server.Recycle(incMSG);
        }

        private void HandleDiscoveryRequest(NetIncomingMessage incMSG, NetServer g_Server)
        {
            WriteLine("Client Discovered @ " + incMSG.SenderEndPoint.ToString());
            NetOutgoingMessage outMSG = g_Server.CreateMessage();
            outMSG.Write("Scorched Server");
            g_Server.SendDiscoveryResponse(outMSG, incMSG.SenderEndPoint);
        }

        private void HandleConnectionApproval(NetIncomingMessage incMSG, NetServer g_Server)
        {
            if (incMSG.ReadByte() == (byte)Packet.Connection)
            {
                string Connect = incMSG.ReadString();
                if (Connect == "scorched")
                {
                    incMSG.SenderConnection.Approve();
                    Sleep(500);
                    NetOutgoingMessage outMSG = g_Server.CreateMessage();
                    outMSG.Write((byte)Packet.Connection);
                    g_Server.SendMessage(outMSG, incMSG.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                }
                else { incMSG.SenderConnection.Deny(); }
            }
        }

        private void HandleStatusChange(NetIncomingMessage incMSG, NetServer g_Server)
        {
            WriteLine(incMSG.SenderConnection.ToString() + " status changed. " + incMSG.SenderConnection.Status);
            if (incMSG.SenderConnection.Status == NetConnectionStatus.Disconnected || incMSG.SenderConnection.Status == NetConnectionStatus.Disconnecting)
            {
                WriteLine("Disconnected, clearing data...");
                //Clear data
                WriteLine("Data cleared, connection now open.");
            }
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

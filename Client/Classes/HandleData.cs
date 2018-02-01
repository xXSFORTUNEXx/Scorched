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

        public void HandleDataMessage(NetClient g_Client, UserInterface g_UserInterface, Account[] accounts)
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

                            case (byte)Packet.CharacterData:
                                HandleCharacterData(incMSG, g_Client, g_UserInterface, accounts);
                                break;
                        }
                        break;
                }
            }
        }
        
        private void HandleCharacterData(NetIncomingMessage incMSG, NetClient g_Client, UserInterface g_Userinterface, Account[] accounts)
        {
            string name;
            int x;
            int y;
            int id = incMSG.ReadVariableInt32();

            for (int i = 0; i < GlobalVariables.MAX_CHARACTER_SLOTS; i++)
            {
                accounts[id].Character_Id[i] = incMSG.ReadVariableInt32();
                name = incMSG.ReadString();
                x = incMSG.ReadVariableInt32();
                y = incMSG.ReadVariableInt32();
                accounts[id].character[i].Z = incMSG.ReadVariableInt32();
                accounts[id].character[i].Direction = incMSG.ReadVariableInt32();
                accounts[id].character[i].Step = incMSG.ReadVariableInt32();
                accounts[id].character[i].Aim_Direction = incMSG.ReadVariableInt32();
                accounts[id].character[i].Sprite = incMSG.ReadVariableInt32();
                accounts[id].character[i].Id = accounts[id].Character_Id[i];
                accounts[id].character[i].Name = name;
                accounts[id].character[i].X = x;
                accounts[id].character[i].Y = y;

                if (accounts[id].character[i].Id > 0)
                {
                    g_Userinterface.AddCharacterToList(name, x, y);
                }
            }

            Logging.WriteMessageLog("Character data received!", true, g_Client.ServerConnection.ToString());
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
        Notification,
        CharacterData
    }
}

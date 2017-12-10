﻿using System;
using Lidgren.Network;
using static System.Console;
using static System.Threading.Thread;
using MySql.Data.MySqlClient;

namespace Server.Classes
{
    public class HandleData
    {
        public void HandleDataMessage(NetServer g_Server, Account[] accounts)
        {
            NetIncomingMessage incMSG;
            NetEncryption encryptionKey = new NetXtea(g_Server, "c45450A558B");

            if ((incMSG = g_Server.ReadMessage()) != null)
            {
                incMSG.Decrypt(encryptionKey);

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
                            case (byte)Packet.Register: //1
                                HandleRegistrationRequest(incMSG, g_Server, accounts);
                                break;

                            case (byte)Packet.Login:    //2
                                HandleLoginRequest(incMSG, g_Server, accounts);
                                break;
                        }
                        break;
                }
            }
            g_Server.Recycle(incMSG);
        }

        private void HandleLoginRequest(NetIncomingMessage incMSG, NetServer g_Server, Account[] accounts)
        {
            string name = incMSG.ReadString();
            string password = incMSG.ReadString();

            if (AccountExist(name) && CheckPassword(name, password))
            {
                WriteLine("Valid login!");
            }
            else { WriteLine("Invalid username or password!"); }

            WriteLine("Username: " + name + " Password: " + password);
        }

        private void HandleRegistrationRequest(NetIncomingMessage incMSG, NetServer g_Server, Account[] accounts)
        {
            string name = incMSG.ReadString();
            string password = incMSG.ReadString();

            if (!AccountExist(name))
            {
                int openPlayerslot = OpenPlayerSlot(accounts);
                if (openPlayerslot < (GlobalVariables.MAX_PLAYERS + 1))
                {
                    accounts[openPlayerslot].Name = name;
                    accounts[openPlayerslot].Password = password;
                    accounts[openPlayerslot].Last_Logged = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                    accounts[openPlayerslot].CreateAccountInDatabase();
                    WriteLine("Username: " + name + " Password: " + password);
                }
                else { WriteLine("Server is full!"); return; }
            }
            else { WriteLine("Account already exists!"); }
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

        private static bool AccountExist(string name)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM accounts WHERE name='" + name + "'";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sql_Name = reader.GetString(1);
                            if (sql_Name == name)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }

        private static bool CheckPassword(string name, string pass)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM accounts WHERE name='" + name + "'";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sql_Pass = reader.GetString(2);
                            if (sql_Pass == pass)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }

        private static int OpenPlayerSlot(Account[] accounts)
        {
            for (int i = 0; i < GlobalVariables.MAX_PLAYERS; i++)
            {
                if (accounts[i].Name == null)
                {
                    return i;
                }
            }
            return GlobalVariables.MAX_PLAYERS + 1;
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
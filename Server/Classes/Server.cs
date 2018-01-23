using System;
using Lidgren.Network;
using static System.Console;
using static System.Environment;
using static System.Threading.Thread;
using MySql.Data.MySqlClient;

namespace Server.Classes
{
    class Program
    {
        static NetServer g_Server;
        static NetPeerConfiguration g_Config;

        static void Main(string[] args)
        {
            Title = "Scorched Server - Loading Please Wait";
            MainLogo();
            Logging.WriteMessageLog("Loading Please Wait...");
            Logging.WriteMessageLog("Enabling message types...");
            g_Config = new NetPeerConfiguration("scorched") { Port = 14242 };
            g_Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            g_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            g_Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            g_Config.UseMessageRecycling = true;
            Logging.WriteMessageLog("Setting global variables...");
            g_Config.MaximumConnections = GlobalVariables.MAX_PLAYERS;
            g_Config.EnableUPnP = false;
            g_Config.ConnectionTimeout = 5.0f;
            Logging.WriteMessageLog("MySQL Connection: " + DatabaseConnection());
            SQLDatabase.DatabaseExists();
            Logging.WriteMessageLog("Starting server...");
            g_Server = new NetServer(g_Config);
            g_Server.Start();
            Logging.WriteMessageLog("Server started!");
            Logging.WriteMessageLog("Starting server loop...");
            Server s_Server = new Server();
            s_Server.Loop(g_Server);
        }

        static void MainLogo()
        {
            Logging.WriteLoglessMessage(@" (                                         ", false);
            Logging.WriteLoglessMessage(@" )\ )                       )        (     ", false);
            Logging.WriteLoglessMessage(@"(()/(          (         ( /(    (   )\ )  ", false);
            Logging.WriteLoglessMessage(@" /(_)) (   (   )(    (   )\())  ))\ (()/(  ", false);
            Logging.WriteLoglessMessage(@"(_))   )\  )\ (()\   )\ ((_)\  /((_) ((_)) ", false);
            Logging.WriteLoglessMessage(@"/ __| ((_)((_) ((_) ((_)| |(_)(_))   _| |  ", false);
            Logging.WriteLoglessMessage(@"\__ \/ _|/ _ \| '_|/ _| | ' \ / -_)/ _` |  ", false);
            Logging.WriteLoglessMessage(@"|___/\__|\___/|_|  \__| |_||_|\___|\__,_|  ", false);
            Logging.WriteLoglessMessage("             Created by: Steven M. Fortune  ", false);
        }

        static bool DatabaseConnection()
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            bool sql_Conn = false;
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        sql_Conn = true;
                    }
                }
                catch
                {
                    sql_Conn = false;
                }
                return sql_Conn;
            }
        }
    }

    class Server
    {
        static int lastTick;
        static int lastFrameRate;
        static int frameRate;
        HandleData g_HandleData;
        Account[] accounts = new Account[GlobalVariables.MAX_PLAYERS];

        public void Loop(NetServer g_Server)
        {
            g_HandleData = new HandleData();
            InitArrays();
            Logging.WriteMessageLog("Listening for connections...");
            while (true)
            {
                Title = "Scorched Server - CPS: " + CalculateCyclesPerSecond();
                g_HandleData.HandleDataMessage(g_Server, accounts);
                Sleep(10);
            }
        }

        private void InitArrays()
        {
            for (int i = 0; i < GlobalVariables.MAX_PLAYERS; i++)
            {
                accounts[i] = new Account();
            }
        }

        static int CalculateCyclesPerSecond()
        {
            if (TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }
    }

    public static class GlobalVariables
    {
        public const byte NO = 0;
        public const byte YES = 1;
        public const int MAX_PLAYERS = 10;
        public const int MAX_CHARACTER_SLOTS = 5;
    }
}

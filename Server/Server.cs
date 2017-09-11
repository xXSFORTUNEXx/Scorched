using System;
using Lidgren.Network;
using static System.Console;
using static System.Environment;
using static System.Threading.Thread;

namespace Server
{
    class Program
    {
        static NetServer g_Server;
        static NetPeerConfiguration g_Config;

        static void Main(string[] args)
        {
            Title = "Scorched Server";
            WriteLine("Loading Please Wait...");
            g_Config = new NetPeerConfiguration("scorched") { Port = 14242 };
            g_Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            g_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            g_Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            g_Config.UseMessageRecycling = true;
            g_Config.MaximumConnections = GlobalVariables.MAX_PLAYERS;
            g_Config.EnableUPnP = false;
            g_Config.ConnectionTimeout = 25.0f;
            g_Server = new NetServer(g_Config);
            g_Server.Start();
            Server s_Server = new Server();
            s_Server.Loop(g_Server);
        }
    }

    class Server
    {
        static int lastTick;
        static int lastFrameRate;
        static int frameRate;
        HandleData g_HandleData;

        public void Loop(NetServer g_Server)
        {
            g_HandleData = new HandleData();

            WriteLine("Listening for connections...");
            while (true)
            {
                Title = "Scorched - CPS: " + CalculateCyclesPerSecond();
                g_HandleData.HandleDataMessage(g_Server);
                Sleep(10);
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
        public const int MAX_PLAYERS = 10;
    }
}

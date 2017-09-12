using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;
using static System.Environment;
using static System.Console;

namespace Client
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Client())
                game.Run();
        }
    }
#endif

    public class Client : Game
    {
        NetClient g_Client;
        NetPeerConfiguration g_Config;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        HandleData g_HandleData;

        static int d_Tick;

        public Client()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            g_Config = new NetPeerConfiguration("scorched");
            g_Config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            g_Config.EnableMessageType(NetIncomingMessageType.ConnectionLatencyUpdated);
            g_Config.UseMessageRecycling = true;
            g_Config.MaximumConnections = 1;
            g_Client = new NetClient(g_Config);
            g_Client.Start();
            g_HandleData = new HandleData();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            g_Client.Disconnect("logout");
            Exit();
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            if (g_Client.ServerConnection == null) { CheckForConnection(g_Client); }
            g_HandleData.HandleDataMessage(g_Client);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        private void CheckForConnection(NetClient g_Client)
        {
            if (TickCount - d_Tick >= 6500)
            {
                WriteLine("Connecting to server...");
                g_Client.DiscoverLocalPeers(14242);
                d_Tick = TickCount;
            }
        }
    }

    public static class GlobalVariables
    {
        public const int MAX_PLAYERS = 10;
    }
}

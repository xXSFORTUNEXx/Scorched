using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using Lidgren.Network;
using Gwen.Control;
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
        UserInterface g_UserInterface;

        private Gwen.Renderer.MonoGame.Input.MonoGame m_Input;
        private Gwen.Renderer.MonoGame.MonoGame m_Renderer;
        private Gwen.Skin.SkinBase m_Skin;
        private Gwen.Control.Canvas m_Canvas;

        static int d_Tick;

        public Client()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1360;
            graphics.PreferredBackBufferHeight = 768;
            Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2), (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));
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
            g_UserInterface = new UserInterface();
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Gwen.Platform.Platform.Init(new Gwen.Platform.MonoGame.MonoGamePlatform());
            Gwen.Loader.LoaderBase.Init(new Gwen.Loader.MonoGame.MonoGameAssetLoader(Content));

            m_Renderer = new Gwen.Renderer.MonoGame.MonoGame(GraphicsDevice, Content, Content.Load<Effect>("GwenEffect"));
            m_Renderer.Resize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            m_Skin = new Gwen.Skin.TexturedBase(m_Renderer, "Skins/DefaultSkin");
            m_Skin.DefaultFont = new Gwen.Font(m_Renderer, "Arial", 9);
            m_Canvas = new Canvas(m_Skin);
            m_Input = new Gwen.Renderer.MonoGame.Input.MonoGame(this);
            m_Input.Initialize(m_Canvas);

            m_Canvas.SetSize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            m_Canvas.ShouldDrawBackground = false;
            //m_Canvas.BackgroundColor = new Gwen.Color(255, 150, 170, 170);

            g_UserInterface.InitMainMenu(m_Canvas);
        }

        protected override void UnloadContent()
        {
            if (m_Canvas != null)
            {
                m_Canvas.Dispose();
                m_Canvas = null;
            }
            if (m_Skin != null)
            {
                m_Skin.Dispose();
                m_Skin = null;
            }
            if (m_Renderer != null)
            {
                m_Renderer.Dispose();
                m_Renderer = null;
            }
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

            m_Input.ProcessMouseState();
            m_Input.ProcessKeyboardState();
            m_Input.ProcessTouchState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            m_Canvas.RenderCanvas();

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

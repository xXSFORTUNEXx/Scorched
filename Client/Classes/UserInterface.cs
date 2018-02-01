using System;
using Gwen.Control;
using Gwen.Control.Layout;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Client.Classes
{
    public class UserInterface
    {
        private Window g_MainMenu;
        private Button g_LoginMenuButton;
        private Button g_RegisterMenuButton;

        private Window g_LoginMenu;
        private Label g_LoginLabel;
        private Label g_PassLabel;
        private TextBox g_LoginTextBox;
        private TextBoxPassword g_PassTextBox;
        private LabeledCheckBox g_RememberCheck;
        private Button g_LoginButton;
        private Button g_BackLoginButton;

        private Window g_RegisterMenu;
        private Label g_RegisterLabel;
        private Label g_RegisterPassLabelA;
        private Label g_RegisterPassLabelB;
        private TextBox g_RegisterUserTextBox;
        private TextBoxPassword g_RegisterPassBoxA;
        private TextBoxPassword g_RegisterPassBoxB;
        private Button g_RegisterButton;
        private Button g_BackRegisterButton;

        private Window g_CharacterWindow;
        private Label g_CharLabel;
        private ListBox g_Characters;
        private Button g_EnterWorld;
        private Button g_LogOut;

        private NetClient g_Client;
        private Canvas m_Canvas;

        public UserInterface(NetClient client, Canvas canvas)
        {
            g_Client = client;
            m_Canvas = canvas;
        }

        public void CharactersMenu()
        {
            g_CharacterWindow = new Window(m_Canvas)
            {
                Title = "Character Selection",
                Size = new Gwen.Size(200, 395),
                StartPosition = StartPosition.CenterCanvas,
                IsClosable = false
            };

            VerticalLayout layout = new VerticalLayout(g_CharacterWindow)
            {
                HorizontalAlignment = Gwen.HorizontalAlignment.Center,
                VerticalAlignment = Gwen.VerticalAlignment.Center,
                Padding = Gwen.Padding.One
            };

            g_CharLabel = new Label(layout)
            {
                Margin = Gwen.Margin.Three,
                Size = new Gwen.Size(125, 25),
                Text = "- Select a character -",
                Alignment = Gwen.Alignment.Center
            };

            g_Characters = new ListBox(layout);
            g_Characters.Size = new Gwen.Size(175, 250);
            g_Characters.VerticalAlignment = Gwen.VerticalAlignment.Center;
            g_Characters.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            g_Characters.Margin = Gwen.Margin.Three;
            g_Characters.ColumnCount = 3;

            g_EnterWorld = new Button(layout)
            {
                Margin = Gwen.Margin.Three,
                Size = new Gwen.Size(160, 25),
                Text = "Enter World",
                VerticalAlignment = Gwen.VerticalAlignment.Center,
                HorizontalAlignment = Gwen.HorizontalAlignment.Center
            };

            g_LogOut = new Button(layout)
            {
                Margin = Gwen.Margin.Three,
                Size = new Gwen.Size(125, 25),
                Text = "Back to Main Menu",
                VerticalAlignment = Gwen.VerticalAlignment.Center,
                HorizontalAlignment = Gwen.HorizontalAlignment.Center
            };
        }

        public void AddCharacterToList(string name, int x, int y)
        {
            TableRow row = g_Characters.AddRow(name);
            row.SetCellText(1, "X: " + x);
            row.SetCellText(2, "Y: " + y);
        }

        public void InitMainMenu()
        {
            g_MainMenu = new Window(m_Canvas);
            g_MainMenu.Title = "Main Menu";
            g_MainMenu.Size = new Gwen.Size(150, 150);
            g_MainMenu.StartPosition = StartPosition.CenterCanvas;
            g_MainMenu.IsClosable = false;

            VerticalLayout layout = new VerticalLayout(g_MainMenu);
            layout.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            layout.Padding = Gwen.Padding.Five;

            g_LoginMenuButton = new Button(layout);
            g_LoginMenuButton.Margin = Gwen.Margin.Six;
            g_LoginMenuButton.Size = new Gwen.Size(100, 25);
            g_LoginMenuButton.Text = "Login";
            g_LoginMenuButton.Clicked += G_Login_Clicked;

            g_RegisterMenuButton = new Button(layout);
            g_RegisterMenuButton.Margin = Gwen.Margin.Six;
            g_RegisterMenuButton.Size = new Gwen.Size(100, 25);
            g_RegisterMenuButton.Text = "Register";
            g_RegisterMenuButton.Clicked += G_Register_Clicked;
        }

        private void G_Register_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            if (g_Client.ServerConnection == null) { return; }
            g_MainMenu.Close();
            RegisterMenu(sender);
            g_RegisterMenu.MakeModal();
        }

        private void G_Login_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            if (g_Client.ServerConnection == null) { return; }
            g_MainMenu.Close();
            LoginMenu(sender);
            g_LoginMenu.MakeModal();
        }

        private void LoginMenu(ControlBase control)
        {
            g_LoginMenu = new Window(control.GetCanvas());
            g_LoginMenu.Title = "Login";
            g_LoginMenu.Size = new Gwen.Size(200, 275);
            g_LoginMenu.StartPosition = StartPosition.CenterCanvas;
            g_LoginMenu.IsClosable = false;

            VerticalLayout layout = new VerticalLayout(g_LoginMenu);
            layout.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            layout.Padding = Gwen.Padding.Five;

            g_LoginLabel = new Label(layout);
            g_LoginLabel.Margin = Gwen.Margin.Three;
            g_LoginLabel.Size = new Gwen.Size(150, 15);
            g_LoginLabel.Text = "Username:";
            g_LoginLabel.Alignment = Gwen.Alignment.Left;

            g_LoginTextBox = new TextBox(layout);
            g_LoginTextBox.Margin = Gwen.Margin.Six;
            g_LoginTextBox.SetText("sfortune");
            g_LoginTextBox.Size = new Gwen.Size(150, 25);

            g_PassLabel = new Label(layout);
            g_PassLabel.Margin = Gwen.Margin.Three;
            g_PassLabel.Size = new Gwen.Size(150, 15);
            g_PassLabel.Text = "Password:";
            g_PassLabel.Alignment = Gwen.Alignment.Left;

            g_PassTextBox = new TextBoxPassword(layout);
            g_PassTextBox.Margin = Gwen.Margin.Six;
            g_PassTextBox.SetText("fortune");
            g_PassTextBox.Size = new Gwen.Size(150, 25);

            g_RememberCheck = new LabeledCheckBox(layout);
            g_RememberCheck.Text = "Remember me?";
            g_RememberCheck.Margin = Gwen.Margin.Three;
            g_RememberCheck.Size = new Gwen.Size(150, 25);

            g_LoginButton = new Button(layout);
            g_LoginButton.Margin = Gwen.Margin.Six;
            g_LoginButton.Size = new Gwen.Size(100, 25);
            g_LoginButton.Clicked += G_LoginButton_Clicked;
            g_LoginButton.Text = "Login";

            g_BackLoginButton = new Button(layout);
            g_BackLoginButton.Margin = Gwen.Margin.Six;
            g_BackLoginButton.Size = new Gwen.Size(100, 25);
            g_BackLoginButton.Clicked += G_ExitU_Clicked;
            g_BackLoginButton.Text = "Back";
        }

        private void G_LoginButton_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            SendLoginRequest();
            g_LoginMenu.Close();
        }

        private void G_ExitU_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            g_LoginMenu.Close();
            g_MainMenu.Show();
        }

        private void RegisterMenu(ControlBase control)
        {
            g_RegisterMenu = new Window(control.GetCanvas());
            g_RegisterMenu.Title = "Register";
            g_RegisterMenu.Size = new Gwen.Size(200, 300);
            g_RegisterMenu.StartPosition = StartPosition.CenterCanvas;
            g_RegisterMenu.IsClosable = false;

            VerticalLayout layout = new VerticalLayout(g_RegisterMenu);
            layout.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            layout.Padding = Gwen.Padding.Five;

            g_RegisterLabel = new Label(layout);
            g_RegisterLabel.Margin = Gwen.Margin.Three;
            g_RegisterLabel.Size = new Gwen.Size(150, 15);
            g_RegisterLabel.Text = "Username:";
            g_RegisterLabel.Alignment = Gwen.Alignment.Left;

            g_RegisterUserTextBox = new TextBox(layout);
            g_RegisterUserTextBox.Margin = Gwen.Margin.Six;
            g_RegisterUserTextBox.Size = new Gwen.Size(150, 25);

            g_RegisterPassLabelA = new Label(layout);
            g_RegisterPassLabelA.Margin = Gwen.Margin.Three;
            g_RegisterPassLabelA.Size = new Gwen.Size(150, 15);
            g_RegisterPassLabelA.Text = "Password:";
            g_RegisterPassLabelA.Alignment = Gwen.Alignment.Left;

            g_RegisterPassBoxA = new TextBoxPassword(layout);
            g_RegisterPassBoxA.Margin = Gwen.Margin.Six;
            g_RegisterPassBoxA.Size = new Gwen.Size(150, 25);

            g_RegisterPassLabelB = new Label(layout);
            g_RegisterPassLabelB.Margin = Gwen.Margin.Three;
            g_RegisterPassLabelB.Size = new Gwen.Size(150, 15);
            g_RegisterPassLabelB.Text = "Retype Password:";
            g_RegisterPassLabelB.Alignment = Gwen.Alignment.Left;

            g_RegisterPassBoxB = new TextBoxPassword(layout);
            g_RegisterPassBoxB.Margin = Gwen.Margin.Six;
            g_RegisterPassBoxB.Size = new Gwen.Size(150, 25);

            g_RegisterButton = new Button(layout);
            g_RegisterButton.Margin = Gwen.Margin.Six;
            g_RegisterButton.Size = new Gwen.Size(100, 25);
            g_RegisterButton.Clicked += G_RegisterButton_Clicked;
            g_RegisterButton.Text = "Register";

            g_BackRegisterButton = new Button(layout);
            g_BackRegisterButton.Margin = Gwen.Margin.Six;
            g_BackRegisterButton.Size = new Gwen.Size(100, 25);
            g_BackRegisterButton.Clicked += G_ExitR_Clicked;
            g_BackRegisterButton.Text = "Back";
        }

        private void G_RegisterButton_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            SendRegistrationRequest();
            g_RegisterMenu.Close();
            g_MainMenu.Show();
        }

        private void G_ExitR_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            g_RegisterMenu.Close();
            g_MainMenu.Show();
        }

        private void SendRegistrationRequest()
        {
            if (g_Client == null || g_Client.ServerConnection == null) { return; }

            string name = g_RegisterUserTextBox.Text;
            string pass = g_RegisterPassBoxA.Text;
            string r_pass = g_RegisterPassBoxB.Text;

            if (name != null && name.Length > 3)
            {
                if (pass != null && r_pass != null && pass.Length > 3 && r_pass.Length > 3)
                {
                    if (pass == r_pass)
                    {
                        NetEncryption encryptionKey = new NetXtea(g_Client, "c45450A558B");
                        NetOutgoingMessage outMSG = g_Client.CreateMessage();
                        outMSG.Write((byte)Packet.Register);
                        outMSG.Write(name);
                        outMSG.Write(pass);
                        outMSG.Encrypt(encryptionKey);
                        g_Client.SendMessage(outMSG, g_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
                    }
                }
            }
            Logging.WriteMessageLog("Sending registration request...");
        }

        private void SendLoginRequest()
        {
            if (g_Client == null || g_Client.ServerConnection == null) { return; }

            string name = g_LoginTextBox.Text;
            string pass = g_PassTextBox.Text;

            if (name != null && pass != null && name.Length > 3 && pass.Length > 3)
            {
                NetEncryption encryptionKey = new NetXtea(g_Client, "c45450A558B");
                NetOutgoingMessage outMSG = g_Client.CreateMessage();
                outMSG.Write((byte)Packet.Login);
                outMSG.Write(name);
                outMSG.Write(pass);
                outMSG.Encrypt(encryptionKey);
                g_Client.SendMessage(outMSG, g_Client.ServerConnection, NetDeliveryMethod.ReliableOrdered);
            }
            Logging.WriteMessageLog("Sending login request...");
        }
    }
}

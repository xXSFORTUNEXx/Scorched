using System;
using Gwen.Control;
using Gwen.Control.Layout;

namespace Client
{
    public class UserInterface
    {
        Window g_MainMenu;
        Button g_Login;
        Button g_Register;
        Button g_Exit;

        public UserInterface() {}

        public void InitMainMenu(Canvas m_Canvas)
        {
            g_MainMenu = new Window(m_Canvas);
            g_MainMenu.Title = "Main Menu";
            g_MainMenu.Size = new Gwen.Size(150, 150);
            g_MainMenu.StartPosition = StartPosition.CenterCanvas;

            VerticalLayout layout = new VerticalLayout(g_MainMenu);
            layout.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            layout.Padding = Gwen.Padding.Five;

            g_Login = new Button(layout);
            g_Login.Margin = Gwen.Margin.Six;
            g_Login.Size = new Gwen.Size(100, 25);
            g_Login.Text = "Login";

            g_Register = new Button(layout);
            g_Register.Margin = Gwen.Margin.Six;
            g_Register.Size = new Gwen.Size(100, 25);
            g_Register.Text = "Register";

            g_Exit = new Button(layout);
            g_Exit.Margin = Gwen.Margin.Six;
            g_Exit.Size = new Gwen.Size(100, 25);
            g_Exit.Text = "Exit";
        }
    }
}

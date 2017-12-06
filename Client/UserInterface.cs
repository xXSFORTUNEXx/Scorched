﻿using System;
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

        Window g_LoginMenu;
        Label g_UserL;
        Label g_PassL;
        TextBox g_User;
        TextBox g_Pass;
        CheckBox g_Remem;
        

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
            g_Login.Clicked += G_Login_Clicked;

            g_Register = new Button(layout);
            g_Register.Margin = Gwen.Margin.Six;
            g_Register.Size = new Gwen.Size(100, 25);
            g_Register.Text = "Register";

            g_Exit = new Button(layout);
            g_Exit.Margin = Gwen.Margin.Six;
            g_Exit.Size = new Gwen.Size(100, 25);
            g_Exit.Text = "Exit";
        }

        private void G_Login_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            LoginMenu(sender);
        }

        private void LoginMenu(ControlBase control)
        {
            g_LoginMenu = new Window(control.GetCanvas());
            g_LoginMenu.Title = "Login Menu";
            g_LoginMenu.Size = new Gwen.Size(200, 350);
            g_LoginMenu.StartPosition = StartPosition.CenterCanvas;

            VerticalLayout layout = new VerticalLayout(g_LoginMenu);
            layout.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            layout.Padding = Gwen.Padding.Five;

            g_UserL = new Label(layout);
            g_UserL.Margin = Gwen.Margin.Three;
            g_UserL.Size = new Gwen.Size(150, 15);
            g_UserL.Text = "Username:";
            g_UserL.Alignment = Gwen.Alignment.Left;

            g_User = new TextBox(layout);
            g_User.Margin = Gwen.Margin.Six;
            g_User.Size = new Gwen.Size(150, 25);
        }
    }
}
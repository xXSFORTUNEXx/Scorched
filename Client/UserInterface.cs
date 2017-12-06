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

        Window g_LoginMenu;
        Label g_UserL;
        Label g_PassL;
        TextBox g_User;
        TextBoxPassword g_Pass;
        LabeledCheckBox g_Remem;
        Button g_LoginU;
        Button g_ExitU;

        Window g_RegisterMenu;
        Label g_UserR;
        Label g_PassA;
        Label g_PassB;
        TextBox g_RegUser;
        TextBoxPassword g_RegPassA;
        TextBoxPassword g_RegPassB;
        Button g_Reg;
        Button g_ExitR;

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
            g_Register.Clicked += G_Register_Clicked;

            g_Exit = new Button(layout);
            g_Exit.Margin = Gwen.Margin.Six;
            g_Exit.Size = new Gwen.Size(100, 25);
            g_Exit.Text = "Exit";
        }

        private void G_Register_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            RegisterMenu(sender);
        }

        private void G_Login_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            LoginMenu(sender);
        }

        private void LoginMenu(ControlBase control)
        {
            g_LoginMenu = new Window(control.GetCanvas());
            g_LoginMenu.Title = "Login";
            g_LoginMenu.Size = new Gwen.Size(200, 275);
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

            g_PassL = new Label(layout);
            g_PassL.Margin = Gwen.Margin.Three;
            g_PassL.Size = new Gwen.Size(150, 15);
            g_PassL.Text = "Password:";
            g_PassL.Alignment = Gwen.Alignment.Left;

            g_Pass = new TextBoxPassword(layout);
            g_Pass.Margin = Gwen.Margin.Six;
            g_Pass.Size = new Gwen.Size(150, 25);

            g_Remem = new LabeledCheckBox(layout);
            g_Remem.Text = "Remember me?";
            g_Remem.Margin = Gwen.Margin.Three;
            g_Remem.Size = new Gwen.Size(150, 25);

            g_LoginU = new Button(layout);
            g_LoginU.Margin = Gwen.Margin.Six;
            g_LoginU.Size = new Gwen.Size(100, 25);
            g_LoginU.Text = "Login";

            g_ExitU = new Button(layout);
            g_ExitU.Margin = Gwen.Margin.Six;
            g_ExitU.Size = new Gwen.Size(100, 25);
            g_ExitU.Clicked += G_ExitU_Clicked;
            g_ExitU.Text = "Back";
        }

        private void G_ExitU_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            g_LoginMenu.Close();
        }

        private void RegisterMenu(ControlBase control)
        {
            g_RegisterMenu = new Window(control.GetCanvas());
            g_RegisterMenu.Title = "Register";
            g_RegisterMenu.Size = new Gwen.Size(200, 300);
            g_RegisterMenu.StartPosition = StartPosition.CenterCanvas;

            VerticalLayout layout = new VerticalLayout(g_RegisterMenu);
            layout.HorizontalAlignment = Gwen.HorizontalAlignment.Center;
            layout.Padding = Gwen.Padding.Five;

            g_UserR = new Label(layout);
            g_UserR.Margin = Gwen.Margin.Three;
            g_UserR.Size = new Gwen.Size(150, 15);
            g_UserR.Text = "Username:";
            g_UserR.Alignment = Gwen.Alignment.Left;

            g_RegUser = new TextBox(layout);
            g_RegUser.Margin = Gwen.Margin.Six;
            g_RegUser.Size = new Gwen.Size(150, 25);

            g_PassA = new Label(layout);
            g_PassA.Margin = Gwen.Margin.Three;
            g_PassA.Size = new Gwen.Size(150, 15);
            g_PassA.Text = "Password:";
            g_PassA.Alignment = Gwen.Alignment.Left;

            g_RegPassA = new TextBoxPassword(layout);
            g_RegPassA.Margin = Gwen.Margin.Six;
            g_RegPassA.Size = new Gwen.Size(150, 25);

            g_PassB = new Label(layout);
            g_PassB.Margin = Gwen.Margin.Three;
            g_PassB.Size = new Gwen.Size(150, 15);
            g_PassB.Text = "Password:";
            g_PassB.Alignment = Gwen.Alignment.Left;

            g_RegPassB = new TextBoxPassword(layout);
            g_RegPassB.Margin = Gwen.Margin.Six;
            g_RegPassB.Size = new Gwen.Size(150, 25);

            g_Reg = new Button(layout);
            g_Reg.Margin = Gwen.Margin.Six;
            g_Reg.Size = new Gwen.Size(100, 25);
            g_Reg.Text = "Register";

            g_ExitR = new Button(layout);
            g_ExitR.Margin = Gwen.Margin.Six;
            g_ExitR.Size = new Gwen.Size(100, 25);
            g_ExitR.Clicked += G_ExitR_Clicked;
            g_ExitR.Text = "Back";
        }

        private void G_ExitR_Clicked(ControlBase sender, ClickedEventArgs arguments)
        {
            g_RegisterMenu.Close();
        }
    }
}

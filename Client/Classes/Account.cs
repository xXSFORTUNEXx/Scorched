using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Client.Classes
{
    public class Account
    {
        public int Id;
        public string Name;
        public string Password;
        public string Last_Login;
        public NetConnection Server_Connection;
        public int[] Character_Id = new int[GlobalVariables.MAX_CHARACTER_SLOTS];

        public Character[] character = new Character[GlobalVariables.MAX_CHARACTER_SLOTS];

        public Account()
        {
            for (int i = 0; i < 5; i++)
            {
                Character_Id[i] = -1;
                character[i] = new Character(-1, "None", 0);
            }
        }

        public Account(int id, string name, string password, string last_logged)
        {
            id = Id;
            Name = name;
            Password = password;
            Last_Login = last_logged;

            for (int i = 0; i < 5; i++)
            {
                Character_Id[i] = -1;
                character[i] = new Character(-1, "None", 0);
            }
        }
    }
}

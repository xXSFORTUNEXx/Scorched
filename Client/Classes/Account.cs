using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    public class Account
    {
        public int Id;
        public string Name;
        public string Password;
        public string Last_Logged;

        public Account() { }

        public Account(int id, string name, string password, string last_logged)
        {
            id = Id;
            Name = name;
            Password = password;
            Last_Logged = last_logged;
        }
    }
}

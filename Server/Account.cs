using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using Lidgren.Network;

namespace Server
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

        public void CreateAccountInDatabase()
        {
            var dbCon = Database.Instance();
            dbCon.DatabaseName = "scorched";
            if (dbCon.IsConnect())
            {
                string query = "INSERT INTO accounts (NAME,PASSWORD,last_logged) VALUES ('" + Name + "','" + Password + "', '0/0/0 00:00:00')";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Name = reader.GetString(0);
                    Password = reader.GetString(0);
                    Last_Logged = reader.GetString(0);
                }
                dbCon.Close();
            }
        }

        public void LoadAccountFromDatabase()
        {
            var dbCon = Database.Instance();
            dbCon.DatabaseName = "scorched";
            if (dbCon.IsConnect())
            {
                string query = "SELECT * FROM accounts WHERE name='sfortune'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Name = reader.GetString(0);
                    Password = reader.GetString(0);
                    Last_Logged = reader.GetString(0);
                }
                dbCon.Close();
            }
        }

        public void UpdatePassword(string new_password)
        {
            Password = new_password;
            var dbCon = Database.Instance();
            dbCon.DatabaseName = "scorched";
            if (dbCon.IsConnect())
            {
                string query = "UPDATE accounts SET PASSWORD='" + new_password + "' WHERE NAME='" + Name + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Name = reader.GetString(0);
                    Password = reader.GetString(0);
                    Last_Logged = reader.GetString(0);
                }
                dbCon.Close();
            }
        }
    }
}

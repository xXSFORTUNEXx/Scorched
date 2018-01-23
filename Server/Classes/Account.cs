using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using Lidgren.Network;

namespace Server.Classes
{
    public class Account
    {
        public int Id;
        public string Name;
        public string Password;
        public string Last_Logged;
        public NetConnection Server_Address;
        public int[] Character_Ids = new int[GlobalVariables.MAX_CHARACTER_SLOTS];

        public Character[] character = new Character[GlobalVariables.MAX_CHARACTER_SLOTS];

        public Account() { }

        public Account(int id, string name, NetConnection server)
        {
            Id = id;
            Name = name;
            Server_Address = server;

            for (int i = 0; i < 5; i++)
            {
                Character_Ids[i] = -1;
            }
        }

        public Account(int id, string name, string password, NetConnection server)
        {
            Id = id;
            Name = name;
            Password = password;
            Server_Address = server;

            for (int i = 0; i < 5; i++)
            {
                Character_Ids[i] = -1;
            }
        }

        public Account(int id, string name, string password, string last_logged, NetConnection server)
        {
            id = Id;
            Name = name;
            Password = password;
            Last_Logged = last_logged;
            Server_Address = server;

            for (int i = 0; i < 5; i++)
            {
                Character_Ids[i] = -1;
            }
        }

        public void CreateAccountInDatabase()
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "INSERT INTO accounts (NAME,PASSWORD,last_logged) VALUES ('" + Name + "','" + Password + "', '" + Last_Logged + "')";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public void LoadAccountFromDatabase(int id)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM accounts WHERE id='" + id + "';";
                Logging.WriteMessageLog("[DB Query] : " + query);
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Id = reader.GetInt32(0);
                            Name = reader.GetString(1);
                            Password = reader.GetString(2);
                            Last_Logged = reader.GetString(3);
                            int n = 4;
                            for (int i = 0; i < 5; i++)
                            {
                                Character_Ids[i] = reader.GetInt32(n);
                                n += 1;
                            }
                        }
                    }
                }
            }
        }

        public void UpdateCharacterIdsInDatabase()
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE accounts";
                query += "SET characterid_1 = '" + Character_Ids[0] + "', characterid_2 = '" + Character_Ids[1] + "', characterid_3 = '" + Character_Ids[2] + "', characterid_4 = '" + Character_Ids[3] + "', characterid_5 = '" + Character_Ids[4] + "'";
                query += "WHERE id = '" + Id + "';";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public void UpdateAccountInDatabase()
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE accounts SET NAME='" + Name + "',PASSWORD='" + Password + "',last_logged='" + Last_Logged + "' WHERE id='" + Id + "';";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public void UpdatePassword(string new_password)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE accounts SET PASSWORD='" + new_password + "' WHERE NAME='" + Name + "'";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public int GetIdFromDatabase(string name)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM accounts WHERE name='" + name + "';";
                Logging.WriteMessageLog("[DB Query] : " + query);
                int id = 0;
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }
                }
                return id;
            }
        }
    }
}

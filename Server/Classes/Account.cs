using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Lidgren.Network;

namespace Server.Classes
{
    public class Account
    {
        public int Id;
        public string Name;
        public string Password;
        public string Last_Login;
        public NetConnection Server_Address;
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

        public Account(int id, string name, NetConnection server)
        {
            Id = id;
            Name = name;
            Server_Address = server;

            for (int i = 0; i < 5; i++)
            {
                Character_Id[i] = -1;
                character[i] = new Character(-1, "None", 0);
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
                Character_Id[i] = -1;
            }
        }

        public Account(int id, string name, string password, string last_logged, NetConnection server)
        {
            id = Id;
            Name = name;
            Password = password;
            Last_Login = last_logged;
            Server_Address = server;

            for (int i = 0; i < 5; i++)
            {
                Character_Id[i] = -1;
            }
        }

        public void CreateAccountInDatabase()
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "INSERT INTO ACCOUNTS (NAME,PASSWORD,LAST_LOGIN,CHAR_1,CHAR_2,CHAR_3,CHAR_4,CHAR_5) VALUES ('" + Name + "','" + Password + "',(SELECT FORMAT(CURRENT_TIMESTAMP, 'yyyy-dd-MM HH:mm:ss.fff', 'en-US')),-1,-1,-1,-1,-1)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Insert] : " + query);
            }
        }

        public void LoadAccountFromDatabase(int id)
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM ACCOUNTS WHERE id='" + id + "';";
                Logging.WriteMessageLog("[DB Query] : " + query);
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Id = reader.GetInt32(0);
                            Name = reader.GetString(1);
                            Password = reader.GetString(2);
                            Last_Login = reader.GetString(3);
                            int n = 4;
                            for (int i = 0; i < 5; i++)
                            {
                                Character_Id[i] = reader.GetInt32(n);
                                n += 1;
                            }
                        }
                    }
                }
            }
        }

        public void UpdateCharacterIdsInDatabase()
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE ACCOUNTS";
                query += "SET CHAR_1 = '" + Character_Id[0] + "', CHAR_2 = '" + Character_Id[1] + "', CHAR_3 = '" + Character_Id[2] + "', CHAR_4 = '" + Character_Id[3] + "', CHAR_5 = '" + Character_Id[4] + "'";
                query += "WHERE id = '" + Id + "';";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Update] : " + query);
            }
        }

        public void UpdateAccountInDatabase()
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE ACCOUNTS SET NAME='" + Name + "',PASSWORD='" + Password + "',LAST_LOGIN=(SELECT FORMAT(CURRENT_TIMESTAMP, 'yyyy-dd-MM HH:mm:ss.fff', 'en-US')) WHERE id='" + Id + "';";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public void UpdatePassword(string new_password)
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE ACCOUNTS SET PASSWORD='" + new_password + "' WHERE NAME='" + Name + "'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public int GetIdFromDatabase(string name)
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM ACCOUNTS WHERE name='" + name + "'";
                Logging.WriteMessageLog("[DB Query] : " + query);
                int id = 0;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
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

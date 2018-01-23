using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using Lidgren.Network;
using static System.Console;

namespace Server.Classes
{
    public static class SQLDatabase
    {
        public static void DatabaseExists()
        {
            string connection = "Server=localhost; UID=sfortune; Pwd=Fortune123*;";
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM information_schema.schemata WHERE SCHEMA_NAME='scorched';";
                Logging.WriteMessageLog("[DB Query] : " + query);
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                }
            }

            if (result == 0) { CreateDatabase(); return; }
            else { Logging.WriteMessageLog("Database found!"); }
        }

        private static void CreateDatabase()
        {
            WriteLine("Attempting to create database...");
            string connection = "Server=localhost; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "CREATE DATABASE IF NOT EXISTS `scorched`;";
                Logging.WriteMessageLog("[DB Query] : " + query);
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            WriteLine("Database created!");
        }
    }
}

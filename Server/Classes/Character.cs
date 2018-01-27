﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using Lidgren.Network;


namespace Server.Classes
{
    public class Character
    {
        public int id;
        public string Name;
        public int X;
        public int Y;
        public int Z;
        public int Direction;
        public int Step;
        public int Aim_Direction;
        public int Sprite;

        public Character() { }

        public void CreateCharacterInDatabase(int userId)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "INSERT INTO characters";
                query += "(NAME,X,Y,z,direction,step,aim_direction,sprite)";
                query += "VALUES";
                query += "('" + Name + "','" + X + "','" + Y + "','" + Z + "','" + Direction + "','" + Step + "','" + Aim_Direction + "','" + Sprite + "');";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public void LoadCharacterFromDatabase(int id)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT * FROM characters WHERE id='" + id + "';";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Name = reader.GetString(1);
                            X = reader.GetInt32(2);
                            Y = reader.GetInt32(3);
                            Z = reader.GetInt32(4);
                            Direction = reader.GetInt32(5);
                            Step = reader.GetInt32(6);
                            Aim_Direction = reader.GetInt32(7);
                            Sprite = reader.GetInt32(8);
                        }
                    } 
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }

        public void UpdateCharacterInDatabase(int id)
        {
            string connection = "Server=localhost; Database=scorched; UID=sfortune; Pwd=Fortune123*;";
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                conn.Open();
                string query = "UPDATE characters";
                query += "SET NAME='" + Name + "',X='" + X + "',Y='" + Y + "',z='" + Z + "',direction='" + Direction + "',step='" + Sprite + "',aim_direction='" + Aim_Direction + "',sprite='" + Sprite + "'";
                query += "WHERE id='" + id + "';";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }
    }

    public enum Directions : byte
    {
        Up,
        Down,
        Left,
        Right,
        Up_Left,
        Up_Right,
        Down_Left,
        Down_Right
    }
}

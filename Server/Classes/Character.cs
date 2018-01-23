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
        public int Class;
        public int Race;

        public int Level;
        public int Experience;
        public int Health;
        public int Max_Health;
        public int Resource;
        public int Max_Resource;

        public int Critical_Hit_Chance;
        public int Block_Chance;
        public int Armor;
        public int Haste;
        public int Dodge_Chance;
        public int Parry;
        public int Mastery;
        public int Versitility;
        public int Life_Leech;

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
                query += "(NAME,X,Y,z,class,race,LEVEL,experience,health,max_health,resource,max_resource,direction,step,aim_direction,sprite)";
                query += "VALUES";
                query += "('" + Name + "','" + X + "','" + Y + "','" + Z + "','" + Class + "','" + Race + "','" + Level + "','" +  Experience + "','" + Health + "','" + Max_Health + "','" + Resource + "','" + Max_Resource + "','" + Direction + "','" + Step + "','" + Aim_Direction + "','" + Sprite + "');";
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
                            Class = reader.GetInt32(5);
                            Race = reader.GetInt32(6);
                            Level = reader.GetInt32(7);
                            Experience = reader.GetInt32(8);
                            Health = reader.GetInt32(9);
                            Max_Health = reader.GetInt32(10);
                            Resource = reader.GetInt32(11);
                            Max_Resource = reader.GetInt32(12);
                            Direction = reader.GetInt32(13);
                            Step = reader.GetInt32(14);
                            Aim_Direction = reader.GetInt32(15);
                            Sprite = reader.GetInt32(16);
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
                query += "SET NAME='" + Name + "',X='" + X + "',Y='" + Y + "',z='" + Z + "',class='" + Class + "',race='" + Race + "',LEVEL='" + Level + "',experience='" + Experience + "',health='" + Health + "',max_health='" + Max_Health + "',resource='" + Resource + "',";
                query += "max_resource='" + Max_Resource + "',direction='" + Direction + "',step='" + Sprite + "',aim_direction='" + Aim_Direction + "',sprite='" + Sprite + "'";
                query += "WHERE id='" + id + "';";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteMessageLog("[DB Query] : " + query);
            }
        }
    }

    public enum Classes : byte
    {
        Warrior,
        Mage,
        Ranger,
        Priest
    }

    public enum Stats : byte
    {
        Strength,
        Intelligence,
        Agility,
        Spirit
    }

    public enum Resource_Types : byte
    {
        Rage,
        Mana,
        Speed
    }

    public enum Equipment_Slots : byte
    {
        Head,
        Neck,
        Shoulder,
        Back,
        Chest,
        Wrist,
        Hand,
        Waist,
        Legs,
        Feet,
        Ring,
        Main_Hand,
        Off_Hand
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

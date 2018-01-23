using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
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

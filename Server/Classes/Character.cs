using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Strength;
        public int Intelligence;
        public int Agility;
        public int WillPower;
        public int Critical_Hit_Chance;
        public int Block_Chance;

        public int Direction;
        public int Aim_Direction;

        public Character() { }
    }

    public enum Classes : byte
    {
        Warrior,
        Mage,
        Ranger,
        Priest
    }

    public enum Resource_Types : int
    {
        Rage,
        Mana,
        Speed
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

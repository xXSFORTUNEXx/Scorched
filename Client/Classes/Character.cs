using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    public class Character
    {
        public int Id;
        public string Name;
        public int X;
        public int Y;
        public int Z;
        public int Direction;
        public int Step;
        public int Aim_Direction;
        public int Sprite;

        public Character() { }

        public Character(int id, string name, int sprite)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
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

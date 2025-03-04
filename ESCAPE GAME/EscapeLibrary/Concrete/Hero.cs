using System;
using System.Drawing;
using EscapeLibrary.Abstract;

namespace EscapeLibrary.Concrete
{
    internal class Hero : Obj
    {
        //Fixed to match two constructors (obj and below)
        public Hero(int yukseklik, Size moveAreaSize) : base (moveAreaSize)
        {
            
            Left = 25; //Starting position of the created character.
            Top = 25;
            //I wanted hero to move 150 units at a time.
            HMoveDistance = 150;
            

        }
    }
}

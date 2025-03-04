using EscapeLibrary.Enum;
using EscapeLibrary.Interface;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace EscapeLibrary.Abstract
{
    //All objects derived from the Obj class have the Picturebox property.
    //B211200052 Gülsemin ÖZGÜR
    internal abstract class Obj : PictureBox, IMove 
    {
        public Size MoveAreaSize { get; }

        public int HMoveDistance { get; protected set; }
        public int EMoveDistance { get; protected set; }
        public new int Right 
        { get => base.Right;
           set => Left = value - Width; //We created a Right with Set property.
        }
        public new int Bottom //A Bottom with a Set property has been created.
        {
          get => base.Bottom;        
          set => Top = value - Height;
        }

        public int Center //We created horizontal movement control of the Midpoint in landscape position.
        { 
            get => Left + (Width/2);
            set => Left = value - (Width/2);
        }

        public int Middle //We created the horizontal movement control of the Midpoint in portrait position
        { 
            get => Top + (Height/2);
            set => Top = value - (Height/2);
        }

        protected Obj(Size moveAreaSize)
        {
            Image = Image.FromFile($@"img\{GetType().Name}.png"); //Whatever name we create a class with, we will create an object with the picture of the same name belonging to that object (the renewed class).
            MoveAreaSize = moveAreaSize; //We determined the movement area using Constructor.
            SizeMode = PictureBoxSizeMode.StretchImage;           
        }
        public bool DoMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.left:
                    return MoveLeft();
                case Direction.right:
                    return MoveRight();
                case Direction.up:
                    return MoveUp();
                case Direction.down:
                    return MoveDown();
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction),direction,null);
            }
        }

        private bool MoveDown()
        {
            if (Bottom == MoveAreaSize.Width -25) return true;

            var newBottom = Bottom + HMoveDistance; //Moves the distance set for the hero
            var isOver = newBottom > MoveAreaSize.Height - 25;

            Bottom = isOver ? MoveAreaSize.Height -25: newBottom; //We use Right, which we give the Set property to.

            return Bottom == MoveAreaSize.Height -25;
        }

        private bool MoveUp() 
        {
            if (Top == 25) return true; //If the hero hits, the Bool variable will be true.
            var newTop = Top - HMoveDistance;
            var isOver = newTop < 25;
            Top = isOver ? 25 : newTop; //If the movement area is less than the movement distance, it will move as much as the movement area.

            return Top == 25;
        }

        private bool MoveRight()
        {
            if (Right == MoveAreaSize.Width-25) return true;

            var newRight = Right + HMoveDistance; //Moves the distance set for the hero
            var isOver = newRight > MoveAreaSize.Width-25;

            Right = isOver ? MoveAreaSize.Width-25 : newRight ;

            return Right == MoveAreaSize.Width-25;
        }

        private bool MoveLeft()
        {
            if (Left == 25) return true; //If the hero hits, the Bool variable will be true.
            var newLeft = Left - HMoveDistance; //Moves the distance set for the hero
            var isOver = newLeft < 25;
            Left = isOver ? 25 : newLeft;

            return Left == 25;            
        }
    }
}

using EscapeLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeLibrary.Interface
{
    internal interface IMove
    {
        //Sets are not written to prevent the movement area from being changed from outside.
        Size MoveAreaSize { get; }

        int HMoveDistance { get; } //To determine how much to move at a time.
        int EMoveDistance { get; }

        /// <summary>
        /// Moves the object in the desired direction
        /// </summary>
        /// <param name="direction">Which direction to move</param>
        /// <returns>If the object hits the wall, it returns true.</returns>
        bool DoMove (Direction direction); 
    }
}

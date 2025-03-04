using EscapeLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscapeLibrary.Concrete
{
    internal class Minion : Obj
    {
        //I created a class named Minion, and since it inherited from the Obj class, it assigned minion.png, which is a png file with the same class name, as the image value of the picturebox.
        //A random object was created from the Random class. Random numbers were assigned to x and y variables with the next property of this object.
        //Randomly assigned x and y variables represent rows and columns in blocks. When multiplied by 150, they create a minion worth block, which is the cell where the row and column intersect.
        //Since the name, size, sizemode, location properties of the Minion object will be the same for each object, it is defined in its own class.
        public Minion(Size moveAreaSize) : base(moveAreaSize)
        {
            Random random = new Random();
            EMoveDistance = 150;
            int x = random.Next(6, 9) * 150; //Locations where minions will spawn randomly.
            int y = random.Next(1, 5) * 150 ;
            Name = "Minion";
            Size = new Size(150, 150);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Location = new Point(x, y);       
        }
    }
}

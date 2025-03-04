using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EscapeLibrary.Concrete;
using EscapeLibrary.Enum;

namespace EscapeGame
{
    public partial class Level1 : Form
    {
        //B221200305 Gülsemin ÖZGÜR.
        //Since the Game.cs class is in a different project, it is first defined in Level1.cs.
        //After all the components are created, the size of the form is defined.
        //The _game object is the constructor of the Game.cs class. Thanks to this constructor method, I was able to connect the fields we defined in Game.cs in Level1.cs.
        //In Game.cs, there is a timer named _elapsedTimer derived from the TIMER class and an object named ElapsedTime with the TIMESPAN property. ElapsedTime increments the elapsed time by one for each second.
        //Thanks to the Constructor, I associated the value in ElapsedTime with an event (Game_ElapsedTimeChanged) that is triggered every second with the help of EventHandler and printed it in the text property of the LabelTime in the Form.
        //There is a method called Move in the interface called Game. This method takes one of the values of the enum in the Directory class as a parameter.
        //There are methods in the Game.cs class for each value in the Enum.
        //With the e.Keycode expression, we obtain the numerical equivalents of the keys pressed from the keyboard. We can control whatever we want from these keys (switch-case) and do whatever we want.
        //The this.keyPreview statement is written in the load event of the form. That is, as soon as the form is loaded, it will listen to the keys pressed from the keyboard thanks to the value true.

        private readonly Game _game;
        public Level1()
        {
            InitializeComponent();
            this.Size = new Size(1050,450);
            labelTime.BringToFront();
            _game = new Game(panelMain,panelInfo, levelLabel, labelHealt, labelScore, labelPlayer, textPlayer, menuPanel, isCont);
            _game.ElapsedTimeChanged += Game_ElapsedTimeChanged;           
        }

        private void Level1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _game.Start();
                    break;
                case Keys.Right:
                    _game.Move(Direction.right);                   
                    break;
                case Keys.Left:
                    _game.Move(Direction.left);
                    break;
                case Keys.Up:
                    _game.Move(Direction.up);
                    break;
                case Keys.Down:
                    _game.Move(Direction.down);
                    break;
                case Keys.P:
                    _game.Pause();
                    break;
            }
        }    
        private void Game_ElapsedTimeChanged(object sender, EventArgs e)
        {
            labelTime.Text = _game.ElapsedTime.ToString(@"m\:ss");
        }
        private void Level1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;       
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void levelLabel_Click(object sender, EventArgs e)
        {

        }

        private void labelTime_Click(object sender, EventArgs e)
        {

        }

        private void labelPlayer_Click(object sender, EventArgs e)
        {

        }
    }
}

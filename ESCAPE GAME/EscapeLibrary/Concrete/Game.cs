using EscapeLibrary.Enum;
using EscapeLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace EscapeLibrary.Concrete
{
    public class Game : IGame
    {
        // B211200052 Gülsemin ÖZGÜR

        // An interface called IGame was created to determine the draft of the game software. There are various functions within this class. These functions are implemented in Game.cs and the methods are automatically included in our class.
        // One of these features is isContinue. It is of Bool data type and forms the basis of the logic to be used in the game to start the game by pressing Enter and to pause it by pressing the 'P' key.

        // In order to count the elapsed time, a timer element named _elapsedTimer was created from the Timer class and set to run every 1 second by giving the value Interval = 1000.
        // Tick Event of _elapsedTimer runs during the specified Interval time. This event timer is used to determine what will happen during the specified Interval period, that is, each time it is triggered.
        // TimeSpan tipinde ElapsedTime isimli bir property ve _elapsedTime isimli bir field tanımlanmıştır. _elapsedTime'ın değeri her değiştiğinde ElapsedTime'in Set özelliği tetiklenir ve değeri değişir.
        // EventHandler has been defined to continuously trigger the event.

        // Pause: When we press the 'P' key, the isContinue variable turns true into false, and false into true. When the value is true, the Start property of the timers is called, and when it is false, the Stop property of the timers is called.
        // By using the params keyword in the StartTimers method, we can use as many Timer type variables as we want as parameters.
        // In the Pause method, it controls the level with the Switch Case so that we can stop the timers we want depending on the level we are at.

        // The method named Game is a constructor, that is, the method that will be run when the program first runs. Variables corresponding to the fields we created were created as parameters to this method.
        // In this way, we can use the fields we created in the Form's own class.

        // The Start method works when the game is opened and started with ENTER. The Start method runs the CreateBlock, createHero, createTrap, CreateChest methods respectively.
        // CreateBlock: An object named Block, of array type that can take 50 values, has been created. It creates a block from top to bottom, from point 0.0 to the last point of the form.
        // Block class is inherited from Obj class. The Obj class inherits from the pictureBox class. In other words, the Block class also has pictureBox properties.

        // createHero: An object of type pictureBox named Hero has been created. The properties of this element are included in the Hero class. (Image, Size ...)

        // createTrap: Created for the trap feature in the game. It randomly sets the name property of 10 of the created blocks (except Finish and Start Blocks) to "Trap" and the Image Property to one of 3 randomly different trap images.
        // checkTrap: This method is executed every time our object named Hero moves. Using the "Intersectwith" method, it works when the hero intersects with any block containing a Trap in the Name property and reduces the defined _healt value by 1.

        // createBomb: Created for the bomb attack feature in the game. It sets the name property of 10 random blocks (except Finish and Start Blocks) to "Bomb".
        // Thanks to the _createBombTimer timer, it creates 10 Bombs in random areas every 3 seconds.
        // Thanks to the _restoreBlockTimer timer, it restores the changed bomb areas every 6 seconds and the createBomb method runs again, thus creating an infinite loop bomb.
        // checkBomb: This method occurs every time our object named Hero moves. Using the "Intersectwith" method, it works when the hero intersects with any block containing Bomb in the Name property and reduces the defined _healt value by 1.
        // The difference between checkBomb and checkTrap: The bombs are constantly changing places. Therefore, if a bomb occurs where the Hero is while he is not moving, the _health value decreases by 1.

        // createMinion: An object named Minion, type picturebox, has been created. The properties of this element are defined within the Minion class.
        // Thanks to the _createMinionTimer timer, minions are created on random blocks every 2 seconds.
        // moveMinionTimer: Each minion object created moves left one block every second and disappears in the last block.
        // checkMinion: This method is executed every time our object named Hero moves. Using the "Intersectwith" method, it works when the hero intersects with any block containing a Minion in the Name property and reduces the defined _healt value by 1.


        // createChest: Changes the name property of one random block (except Finish and Start) to "Chest" and replaces the image property with "Chest.png" in the directory.
        // With the Intersectwith command, the CheckChest method runs when there is an intersection between our character and the Chest.
        // CheckChest: When this method runs, a random double number between 1 and 2 is generated from the Random class. If this random number is less than 1.8, the chest's name is changed to GChest and the _health value is increased by 1.
        // If the randomly generated value is greater than 0.8, its name becomes BChest and it reduces the _health value by 1. Thus, there is an 80% chance of increasing health and a 20% chance of decreasing health.

        // Constructor method: Game (param1, param2.....). This constructor method was established to make associations between the fields created in Game.cs and the form.
        // When the parameters given to the constructor method are used in Level1.cs, they become parameters by writing the names of the components that physically exist in the form.
        // For example, in the Constructor method, the field named _panelMain is used, and in the method in Level1.cs, the physically existing PanelMain element is used as a parameter.

        // Prop(property) was used to make the _health, _score, _level, _elapsedTime properties dynamic.
        // For example, the _health value is initially set to 3. Thanks to the get, set feature of a variable named healt, when there is a change in the _healt variable, it is updated by the operation of the Set feature.
        // The same situations apply to _score and _level.

        private Random random = new Random();
        #region Timers

        private readonly Timer _elapsedTimer = new Timer { Interval = 1000 };

        private readonly Timer _createBombTimer = new Timer { Interval = 3000 };
        private readonly Timer _restoreBlockTimer = new Timer { Interval = 6000 };

        private readonly Timer _createMinionTimer = new Timer { Interval = 2000 };
        private readonly Timer _moveMinionTimer = new Timer { Interval = 1000 };

        #endregion

        #region Properties

        public bool IsContinue { get; private set; }

        public int _level = 1;
        public int _healt = 3;
        public int _score = 0;
        public int healt
        { //When the health value is reduced in the methods, the set property of this property will work and a dynamic health change will be made.
            get => _healt;
            set { _healt = value; }
        }

        public int score 
        {   get => _score;
            set
            {
                _score = value;
            }
        }

        #endregion

        #region Fields

        private readonly Panel _panelMain;
        private readonly Panel _panelInfo;
        private readonly Panel _panelMenu;

        private Label _levelLabel;
        private Label _healtLabel;
        private Label _scoreLabel;
        private Label _playerLabel;
        private Label _contLabel;

        private TextBox _playerText;

        private Hero _hero;

        private Block[] _blocks = new Block[50];
        private readonly List<Minion> _minions = new List<Minion>();
        #endregion

        #region EventHandlers
        public event EventHandler BombTimeChanged;
        public event EventHandler MinionTimeChanged;

        public event EventHandler RestoreMinionChanged;
        public event EventHandler RestoreBlockChanged;
 
        public event EventHandler ElapsedTimeChanged;
        #endregion

        #region TimeSpans

        private TimeSpan _elapsedTime;      
        public TimeSpan ElapsedTime 
        {   get => _elapsedTime;
            private set
            {
                _elapsedTime = value;
                ElapsedTimeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
        public Game(Panel panelmain, Panel panelinfo, Label levelLabel, Label healtLabel, Label scoreLabel, Label playerLabel, TextBox playerText, Panel panelMenu, Label cont) //A constructor parameter was given to create objects inside the panel.
        { 
            _panelMain = panelmain;
            _panelInfo = panelinfo;
            _levelLabel = levelLabel;
            _healtLabel = healtLabel;
            _scoreLabel = scoreLabel;
            _playerLabel = playerLabel;
            _playerText = playerText;
            _panelMenu = panelMenu;
            _contLabel = cont;

            _elapsedTimer.Tick += ElapsedTimer_Tick;   

            _createBombTimer.Tick += CreateBombTimer_Tick;
            _createMinionTimer.Tick += CreateMinionTimer_Tick;

            _moveMinionTimer.Tick += MoveMinionTimer_Tick;
            _restoreBlockTimer.Tick += RestoreBlockTimer_Tick; //When the location of the bombs is changed, the timer to restore the blocks that have not been bombed. 
        }

        private void MoveMinionTimer_Tick(object sender, EventArgs e)
        {
            MoveMinion();
        }

        private void CreateMinionTimer_Tick(object sender, EventArgs e)
        {
            CreateMinion();
        }

        private void RestoreBlockTimer_Tick(object sender, EventArgs e)
        {
            RestartBlock();
        }

        private void ElapsedTimer_Tick(object sender, EventArgs e)
        {
            ElapsedTime += TimeSpan.FromSeconds(1);
            _score = _healt * 500 + (1000 - ElapsedTime.Seconds);
            _scoreLabel.Text = Convert.ToString(_score);
        }

        private void CreateBombTimer_Tick(object sender, EventArgs e)
        {
           CreateBomb();
        }

        public void Move(Direction direction)
        {
            if (!IsContinue) return;
            _hero.DoMove(direction);           
            checkTrap();
            checkBomb();
            checkMinion();
            checkChest();
            checkFinish();
        }
        public void Pause()
        {
            //When the P key is pressed, it stops the timer if it is running, and starts it if it is stopped.
            IsContinue = !IsContinue;
            if (IsContinue)
            {
                //_elapsedTimer.Start();
                switch (_level)
                {
                    case 1 :
                        _elapsedTimer.Start();
                        break;
                    case 2:
                        //lvl2TimerStart();
                        startTimers(_createBombTimer, _restoreBlockTimer);
                        break;
                    case 3:
                        //lvl3TimerStart();
                        startTimers(_createMinionTimer, _moveMinionTimer);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                StopTimers();
            }
        }
        public void Start()
        {
            IsContinue = true;
            PlayerMenu();
            _elapsedTimer.Start();
            createHero();
            createBlock();
            createChest();
            createTrap();
        }

        private void PlayerMenu()
        {
            _playerLabel.Text = Convert.ToString(_playerText.Text);
            _panelMenu.Visible = false;
        }

        private void createHero()
        {
            _hero = new Hero(_panelMain.Height, _panelMain.Size)
            {

            };
            _hero.Size = new Size(100, 100);
            _hero.MaximumSize = new Size(100, 100);
            _hero.MinimumSize = new Size(100, 100);
            _hero.SizeMode = PictureBoxSizeMode.StretchImage;
            _hero.BackColor = Color.Transparent;
            _panelMain.Controls.Add(_hero);
            _hero.BringToFront();
        }     
        protected void createBlock()
        {
            int startX = 0;
            int startY = 0;

            int width = 150;
            int height = 150;

            int chest = random.Next(2, 50);
            double randomValue = random.NextDouble();

            //80% chance of producing 1, 20% chance of producing 2
            int result = (randomValue < 0.8) ? 1 : 2;        

            for (int i = 0; i < 50; i++) //Number of blocks to be created
            {                      
                _blocks[i] = new Block(_panelMain.Height, _panelMain.Size)
                {
                    Name = "Block" + (i + 1),
                    Size = new Size(150, 150),
                    Location = new Point(startX, startY)
                };
                _blocks[i].SendToBack();
                _blocks[i].Click += Block_click;              

                if (i == 0)
                {
                    _blocks[i].Name = "StartBlock";
                    _blocks[i].Size = new Size(150, 150);
                    _blocks[i].Location = new Point(startX, startY);
                    _blocks[i].Click += Block_click;
                }

                if (i == 47)
                {
                    _blocks[i].Name = "Finish";
                    _blocks[i].Size = new Size(150, 150);
                    _blocks[i].Location = new Point(startX, startY);
                    _blocks[i].Click += Block_click;
                    _blocks[i].Image = Image.FromFile(@"img\Finish.png");
                }
                _panelMain.Controls.Add(_blocks[i]);

                startY += height; 
                if (startY + height > _panelMain.Height)
                {
                    startY = 0;
                    startX += width;
                }
            }
        }       
        private void createChest()
        {
            while (true)
            {
                int number = random.Next(2, 50);
                if (number != 47 && _blocks[number].Name.StartsWith("Block"))
                {
                    _blocks[number].Name = "Chest";
                    _blocks[number].Image = Image.FromFile(@"img\Chest.png");
                }
                break;
            }          
        }
        Random rnd = new Random();
        public List<int> availableNumbers = Enumerable.Range(1, 50).ToList();
        public void createTrap() //It randomly selects the created blocks and changes their names to Trap.
        {
            var randomBlocks = _blocks.OrderBy(x => random.Next()).Take(10).ToList();
            foreach (var block in randomBlocks)
            {
                if (block.Name != "Finish" && block.Name != "StartBlock" && block.Name.Contains("Block"))
                {
                    //Updates the name of block
                    int blockIndex = Array.IndexOf(_blocks, block);
                    block.Name = $"Trap{blockIndex:D2}";

                }
            }
            /*int i = 0;   //Alternative Trap insertion method.
            for (i = 0; i < 10; i++)
            {
                int index = rnd.Next(0, availableNumbers.Count);
                int randomNumber = availableNumbers[index];
                if (randomNumber != 47 && randomNumber != 1)
                {
                    _blocks[randomNumber].Name = "Trap" + randomNumber;
                    checkTrap(); //If there is a Trap in the starting location of the character, hero loses life.
                }
                availableNumbers.RemoveAt(index);
            }*/
        }
        protected void CreateBomb()
        {         
            RestartBlock();
            var randomBlocks = _blocks.OrderBy(x => random.Next()).Take(10).ToList();
            //Sets the background image of the selected blocks as "Bomb.png"
            foreach (var block in randomBlocks)
            {
                if (block.Name != "Finish" && block.Name != "StartBlock" && block.Name != "Chest")
                {
                    //Updates the name of the block
                    int blockIndex = Array.IndexOf(_blocks, block);
                    block.Name = $"Bomb{blockIndex:D2}";
                    block.Image = Image.FromFile(@"img\Bomb.png");                   
                }                
            }
            checkBomb(); //If a bomb occurs in the area where the character is, hero loses life.
        }
        private void CreateMinion()
        {
            var minion = new Minion(_panelMain.Size);
            _minions.Add(minion);
            _panelMain.Controls.Add(minion);
            minion.BringToFront();
            minion.Click += Minion_click;           
        }
        
        protected void checkTrap()
        {
            foreach (Control control in _panelMain.Controls)
            {
                if (control is Block && control.Name.Contains("Trap") && _hero.Bounds.IntersectsWith(control.Bounds)) //If "Trap" is mentioned in the name of the block where the Hero steps, it is a trap.
                {                    
                    string pcName = control.Name;
                    string lastTwo = pcName.Substring(4);
                    int a = Convert.ToInt32(lastTwo);
                    Random TrapNumber = new Random();
                    int b = TrapNumber.Next(1, 4);

                    //It holds a random number between 1 and 3, and the appearance of the traps changes depending on the random number generated.
                    switch (b)
                    {
                        case 1:
                            _blocks[a].Image = Image.FromFile(@"img\Fire.png");
                            break;
                        case 2:
                            _blocks[a].Image = Image.FromFile(@"img\Trap.png");
                            break;
                        case 3:
                            _blocks[a].Image = Image.FromFile(@"img\Skeleton.png");
                            break;
                        default:
                            break;
                    }
                    LostHealt();
                }
            }
        }
        protected void checkBomb()
        {
            foreach (Control control in _panelMain.Controls)
            {
                if (control is Block && control.Name.Contains("Bomb") && _hero.Bounds.IntersectsWith(control.Bounds)) // Bomb Control
                {
                    LostHealt();
                }
            }
        }
        protected void checkMinion()
        {
            foreach (Control control in _panelMain.Controls)
            {
                if (control is Minion && control.Name.Contains("Minion") && _hero.Bounds.IntersectsWith(control.Bounds)) // Minion Control
                {
                    _hero.BringToFront();
                    LostHealt();
                    break;
                }
            }
        }
        private void checkChest()
        {
            foreach (Control control in _panelMain.Controls)
            {
                if (control is Block && control.Name.Contains("Chest") && _hero.Bounds.IntersectsWith(control.Bounds)) // Minion Control
                {
                    double state = random.NextDouble();
                    int result = (state < 0.8) ? 0 : 1;
                    switch (result)
                    {
                        case 0:
                            control.Name = "GoodChest";
                            UpHealt();
                            control.Name = "GoodC";
                            break;
                        case 1:
                            control.Name = "BadChest";
                            LostHealt();
                            control.Name = "BadC";
                            break;
                    }
                }
            }
        }
        protected void checkFinish()
        {
            foreach (Control control in _panelMain.Controls)
            {
                if (control is Block && control.Name.Contains("Finish") && _hero.Bounds.IntersectsWith(control.Bounds)) //Finish Control
                {
                    if (_level == 3)
                    {
                        Wp();
                    }                    
                    LevelUp();
                }
            }
        }

        private void RestartBlock()
        {
            foreach (var block in _blocks)
            {
                if (block.Name.StartsWith("Bomb"))
                {
                    block.Image = Image.FromFile(@"img\Block.png");
                    block.Name = "Block" + (Array.IndexOf(_blocks, block) + 1);
                }
            }
        }     
        private void MoveMinion()
        {          
            for (var i = _minions.Count - 1; i >= 0; i--)
            {
                var minion = _minions[i];
                if (minion.Location.X >= 150) //If this value is set to 300, the minion will not reach the first column.
                {
                    minion.Left -= 150;
                }
                else
                {
                    _panelMain.Controls.Remove(minion);
                }
                
            }
            checkMinion();
        }
        private void LostHealt()
        {
            _healt--; //If this line is closed, GOD MODE will be opened.
            if (_healt > 0)
            {          
                _healtLabel.Text = Convert.ToString(healt);
            }
            else if (_healt == 0)
            {
                _healtLabel.Text = Convert.ToString(healt);
                //writeScore();
                StopTimers();
                MessageBox.Show("GAME OVER !");             
                Application.Exit();
            }
        }
        private void UpHealt()
        {
            _healt++;
            _healtLabel.Text = Convert.ToString(healt);
        }
        protected void LevelUp()
        {
            Destroy();
            _level++;
            writeScore();
            UpHealt();
            switch (_level)
            {
                case 2:
                    createBlock();
                    createHero();
                    createChest();
                    CreateBomb();
                    _createBombTimer.Start();
                    _restoreBlockTimer.Start();
                    _levelLabel.Text = Convert.ToString(_level);
                    _healtLabel.Text = Convert.ToString(healt);
                    break;
                case 3:
                    _createBombTimer.Stop();
                    _restoreBlockTimer.Stop();
                    createBlock();
                    createHero();
                    createChest();
                    CreateMinion();                   
                    _moveMinionTimer.Start();
                    _createMinionTimer.Start();
                    _levelLabel.Text = Convert.ToString(_level);
                    _healtLabel.Text = Convert.ToString(healt);
                    break;
            } 
        }
        private void writeScore()
        {
            _score = _healt * 500 + (1000 - ElapsedTime.Seconds);
            string s = _scoreLabel.Text = Convert.ToString(_score);          
            using (StreamWriter writer = new StreamWriter("Scores.txt",true))
            {
                writer.WriteLine($"Oyuncu -> {_playerLabel.Text} Skor ->{_score}");
            }
        }
        private void Destroy()
        {
            _panelMain.Controls.Remove(_hero);
            BlockRemove();
        }       
        private void BlockRemove()
        {
            for (int i = 0; i < 50; i++)
            {             
                if (_blocks[i].Name.Contains("Trap"))
                {
                    _panelMain.Controls.Remove(_blocks[i]);
                }
                _panelMain.Controls.Remove(_blocks[i]);
            }
        }
        private void Block_click(object sender, EventArgs e)
        {
            Block clicked = (Block)sender;
            MessageBox.Show(clicked.Name + " Clicked!");
        }
        private void Minion_click(object sender, EventArgs e)
        {
            Minion clicked = (Minion)sender;
            MessageBox.Show(clicked.Name + " Clicked!"); //Find out the name of the clicked pictureBox.
        }
        private void Wp()
        {
            writeScore();
            StopTimers();
            MessageBox.Show("OYUN BİTTİ!", "TEBRİKLER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);           
            Application.Exit(); //Go to the score display.
        }
        private void StopTimers()
        {
            _elapsedTimer.Stop();
            _createBombTimer.Stop();
            _restoreBlockTimer.Stop();
            _createMinionTimer.Stop();
            _moveMinionTimer.Stop();
        }
        protected void startTimers(params Timer[] _timer  )
        {
            _elapsedTimer.Start();
            foreach ( Timer timer in _timer )
            {
                timer.Start();
            }
        }
        private void lvl2TimerStart()
        {
            _elapsedTimer.Start();
            _createBombTimer.Start();
            _restoreBlockTimer.Start();          
        }
        private void lvl3TimerStart()
        {
            _createMinionTimer.Start();
            _moveMinionTimer.Start();
        }
    }
}

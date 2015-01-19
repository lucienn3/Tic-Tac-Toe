using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class Game : Form
    {
        // Drawing
        private System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black, 4);
        private System.Drawing.Pen player = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
        private System.Drawing.Pen ki = new System.Drawing.Pen(System.Drawing.Color.Blue, 5);
        System.Drawing.Graphics formGraphics;
        System.Drawing.Rectangle rectangle;

        // Settings
        public bool fun = false;
        private int step = 0;
        public bool easy = false;

        // Gamefield
        int[,] gamefield = {
                           {0,0,0}, //0 
                           {0,0,0}, //1
                           {0,0,0}  //2
                           };

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Shown(object sender, EventArgs e)
        {
            formGraphics = this.CreateGraphics();
            drawField();
            step = 0;
        }

        private void drawField()
        {
            formGraphics.DrawLine(myPen, 200, 0, 200, 600);
            formGraphics.DrawLine(myPen, 400, 0, 400, 600);
            formGraphics.DrawLine(myPen, 0, 200, 600, 200);
            formGraphics.DrawLine(myPen, 0, 400, 600, 400);
        }

        private int getBox(int x, int y)
        {
            // First row
            if ((x >= 0 && 200 >= x) && (0 <= y && y <= 200)) 
                return 1;
            else if ((205 <= x && x <= 400) && (0 <= y && y <= 200)) 
                return 2;
            else if ((405 <= x && x <= 630) && (0 <= y && y <= 200))
                return 3;

            // Second row
            if ((0 <= x && x <= 200) && (205 <= y && y <= 400))
                return 4;
            else if ((205 <= x && x <= 400) && (205 <= y && y <= 400))
                return 5;
            else if ((405 <= x && x <= 630) && (205 <= y && y <= 400))
                return 6;

            // Third row
            if ((0 <= x && x <= 200) && (405 <= y && y <= 630))
                return 7;
            else if ((205 <= x && x <= 400) && (405 <= y && y <= 630))
                return 8;
            else if ((405 <= x && x <= 630) && (405 <= y && y <= 630))
                return 9;
            return -1;
        }

        private bool NotFull(int box)
        {
            if (box < 4)
            {
                if (gamefield[0, getarraybox(box)] == 0)
                    return true;
            }
            else if (box > 3 && box < 7)
            {
                if (gamefield[1, getarraybox(box)] == 0)
                    return true;
            }
            else if (box > 6 && box < 10)
            {
                if (gamefield[2, getarraybox(box)] == 0)
                    return true;
            }
            return false;
        }

        private bool filled(int box, int who)
        {
            if (box < 4)
            {
                if (gamefield[0, getarraybox(box)] == who)
                    return true;
            }
            else if (box > 3 && box < 7)
            {
                if (gamefield[1, getarraybox(box)] == who)
                    return true;
            }
            else if (box > 6 && box < 10)
            {
                if (gamefield[2, getarraybox(box)] == who)
                    return true;
            }
            return false;
        }

        private int getarraybox(int box)
        {
            if (box % 3 == 0)
                return 2;
            else
                return (box % 3) - 1;
        }

        private void Fill(int box, int who)
        {
            if (box < 4)
            {
                gamefield[0, getarraybox(box)] = who;
            }
            else if (box > 3 && box < 7)
            {
                gamefield[1, getarraybox(box)] = who;
 
            }
            else if (box > 6 && box < 10)
            {
                gamefield[2, getarraybox(box)] = who;
            }
        }

        private void handleMouse (int x, int y)
        {
            
            int box = getBox(x, y);
            if (box != -1)
            {
                if(NotFull(box)) 
                {
                    drawBox(box,1);
                    step++;
                    runki();
                    checkWon();
                }
                else
                {
                    MessageBox.Show("Dieses Feld wurde schon ausgewählt.");
                }      
            }
            else
            {
                MessageBox.Show("Keine Ahnung wo du hin klickst, aber hier ist kein Feld.");
            }
        }

        private void runki()
        {
            if(fun == true)
            {
                MessageBox.Show("Haha... 42 The meaning of life... But you will not win this game!");
                MessageBox.Show("Computer won");
                this.Visible = false;
            }
            if (easy || step < 3 || (!checkHor(2, 1) && !checkVert(2, 1) && !checkhrightleft(2) && !checkhleftright(2)))
            {
                int old_step = step;
                Random rand = new Random();
                while (old_step == step && checknotfull())
                {
                    int box = rand.Next(10);
                    if (NotFull(box) && box != 0)
                    {
                        drawBox(box, 2);
                        step++;
                    }
                }
            }

        }

        private void checkWon()
        {
            if (checkHor(3,1) || checkVert(3,1))
            {
                MessageBox.Show("Good Game! You won!");
                this.Visible = false;
            } 
            else if (!checknotfull())
            {
                MessageBox.Show("No one won :( Try again");
                this.Visible = false;
            }

        }

        private bool checkHor(int c, int who)
        {
            int count = 0;
            bool won = false;
            count = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (!won)
                {
                    count = 0;
                    for (int o = 0; o <= 2; o++)
                    {
                        if (gamefield[o, i] == who)
                        {
                            count++;
                            if (count == c)
                            {
                                i = 3;
                                o = 3;
                                won = true;
                            }
                        }
                    }
                }
            }
            return won;
        }

        private bool checkVert(int c,int who)
        {
            int count = 0;
            bool won = false;
            count = 0;
            for (int i = 0; i <= 2; i++)
            {
                if (!won)
                {
                    count = 0;
                    for (int o = 0; o <= 2; o++)
                    {
                        if (gamefield[i, o] == who)
                        {
                            count++;
                            if (count == c)
                            {
                                i = 3;
                                o = 3;
                                won = true;
                            }
                        }
                    }
                }
            }
            return won;
        }

        private bool checknotfull()
        {
            for (int i = 0; i <= 2; i++)
            {
                  for (int o = 0; o <= 2; o++)
                  {
                        if (gamefield[i, o] == 0)
                        {
                            return true;
                        }
                    }
            }
            return false;
        }

        private int getHor(int c)
        {
            int count = 0;
            int won = 0;
            for (int i = 0; i < 3; i++)
            {
                if (!(won == i))
                {
                    count = 0;
                    for (int o = 0; o < 3; o++)
                    {
                        if (gamefield[o, i] == 1)
                        {
                            count++;
                            if (count == c)
                            {
                                won = i;
                                i = 3;
                                o = 3;
                            }
                        }
                    }
                }
            }
            return won;
        }

        private int getVert(int c)
        {
            int count = 0;
            int won = 0;
            for (int i = 0; i < 3; i++)
            {
                if (!(won == i))
                {
                    count = 0;
                    for (int o = 0; o < 3; o++)
                    {
                        if (gamefield[i, o] == 1)
                        {
                            count++;
                            if (count == c)
                            {
                                won = i;
                                i = 3;
                                o = 3;
                            }
                        }
                    }
                }
            }
            return won;
        }

        private bool checkhleftright(int c)
        {
            int count = 0;
            bool won = false;

            if (filled(1,1)) count++;
            if (filled(5, 1)) count++;
            if (filled(9, 1)) count++;

            if (count == c)
                won = true;
            return won;
        }

        private bool checkhrightleft(int c)
        {
            int count = 0;
            bool won = false;

            if (filled(3, 1)) count++;
            if (filled(5, 1)) count++;
            if (filled(7, 1)) count++;

            if (count == c)
                won = true;
            return won;
        }

        private int getKIchoice()
        {
            int row = 0;
            int col = 0;
            bool hor = checkHor(2,1);
            bool vert = checkVert(2,1);
            bool toplbottomr = checkhleftright(2);
            bool toprbottoml = checkhrightleft(2);

            if(hor)
            {
                col = getHor(2);
                MessageBox.Show("Found at:" + col);
            } 
            else if(vert)
            {

            }
            else if(toplbottomr)
            {

            }
            else if(toprbottoml)
            {

            }



            return row*3+col;
        }

        private void drawBox(int box, int who)
        {
            if (who == 1)
            {
                switch (box)
                {
                    case 1:
                        formGraphics.DrawLine(player, 0, 0, 200, 200);
                        formGraphics.DrawLine(player, 0, 200, 200, 0);
                        drawField();
                        Fill(box, who);
                        break;
                    case 2:
                        formGraphics.DrawLine(player, 200, 0, 400, 200);
                        formGraphics.DrawLine(player, 200, 200, 400, 0);
                        drawField();
                        Fill(box, who);
                        break;
                    case 3:
                        formGraphics.DrawLine(player, 400, 0, 600, 200);
                        formGraphics.DrawLine(player, 400, 200, 600, 0);
                        drawField();
                        Fill(box, who);
                        break;
                    case 4:
                        formGraphics.DrawLine(player, 0, 200, 200, 400);
                        formGraphics.DrawLine(player, 0, 400, 200, 200);
                        drawField();
                        Fill(box, who);
                        break;
                    case 5:
                        formGraphics.DrawLine(player, 200, 200, 400, 400);
                        formGraphics.DrawLine(player, 200, 400, 400, 200);
                        drawField();
                        Fill(box, who);
                        break;
                    case 6:
                        formGraphics.DrawLine(player, 400, 200, 600, 400);
                        formGraphics.DrawLine(player, 400, 400, 600, 200);
                        drawField();
                        Fill(box, who);
                        break;
                    case 7:
                        formGraphics.DrawLine(player, 0, 400, 200, 600);
                        formGraphics.DrawLine(player, 0, 600, 200, 400);
                        drawField();
                        Fill(box, who);
                        break;
                    case 8:
                        formGraphics.DrawLine(player, 200, 400, 400, 600);
                        formGraphics.DrawLine(player, 200, 600, 400, 400);
                        drawField();
                        Fill(box, who);
                        break;
                    case 9:
                        formGraphics.DrawLine(player, 400, 400, 600, 600);
                        formGraphics.DrawLine(player, 400, 600, 600, 400);
                        drawField();
                        Fill(box, who);
                        break;
                }
            }
            else
            {
               switch (box)
                {

                    case 1:
                        rectangle = new System.Drawing.Rectangle(2, 2, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 2:
                        rectangle = new System.Drawing.Rectangle(204, 1, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 3:
                        rectangle = new System.Drawing.Rectangle(404, 1, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 4:
                         rectangle = new System.Drawing.Rectangle(2, 202, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 5:
                       rectangle = new System.Drawing.Rectangle(204, 201, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 6:
                        rectangle = new System.Drawing.Rectangle(404, 201, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 7:
                        rectangle = new System.Drawing.Rectangle(2, 402, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 8:
                        rectangle = new System.Drawing.Rectangle(204, 401, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                    case 9:
                        rectangle = new System.Drawing.Rectangle(404, 401, 194, 194);
                        formGraphics.DrawEllipse(ki, rectangle);
                        drawField();
                        Fill(box, who);
                        break;
                }
            }
        }

        private void Game_MouseClick(object sender, MouseEventArgs e)
        {
            handleMouse(e.Location.X, e.Location.Y);
        }
    }
}

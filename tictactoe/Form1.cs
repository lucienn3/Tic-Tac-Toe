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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            game.fun = true;
            game.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            game.fun = false;
            game.Visible = true;
        }
    }
}
